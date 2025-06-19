using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class MarcasController
    {
        public async Task<List<Marcas>> ObtenerTodasLasMarcasAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                // Solo marcas activas (estatus = true)
                return await context.Marcas
                    .Where(m => m.estatus == true)
                    .OrderBy(m => m.nombre)
                    .ToListAsync();
            }
        }

        public async Task<int> InsertarMarcaAsync(string nombreMarca)
        {
            using (var context = new la_ross_dbEntities())
            {
                var existente = await context.Marcas
                    .FirstOrDefaultAsync(m => m.nombre == nombreMarca);

                if (existente != null)
                {
                    if (!existente.estatus)
                    {
                        // Reactivar si estaba eliminada (estatus = false)
                        existente.estatus = true;
                        await context.SaveChangesAsync();
                        return existente.id_marca;
                    }

                    // Ya existe activa, lanzar excepción o notificar
                    throw new Exception("Ya existe una marca con ese nombre.");
                }

                // No existe, la creamos
                var nueva = new Marcas
                {
                    nombre = nombreMarca,
                    estatus = true
                };

                context.Marcas.Add(nueva);
                await context.SaveChangesAsync();
                return nueva.id_marca;
            }
        }

        public async Task ModificarMarcaAsync(int idMarca, string nuevoNombre)
        {
            using (var context = new la_ross_dbEntities())
            {
                // Verificar que no exista otra marca activa con el mismo nombre
                var existente = await context.Marcas
                    .FirstOrDefaultAsync(m => m.nombre == nuevoNombre && m.estatus && m.id_marca != idMarca);

                if (existente != null)
                    throw new Exception("Ya existe otra marca activa con ese nombre.");

                var marca = await context.Marcas.FindAsync(idMarca);
                if (marca != null)
                {
                    marca.nombre = nuevoNombre;
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Marca no encontrada.");
                }
            }
        }

        public async Task EliminarMarcaAsync(int idMarca)
        {
            using (var context = new la_ross_dbEntities())
            {
                var marca = await context.Marcas.FindAsync(idMarca);
                if (marca != null)
                {
                    // Eliminación lógica: estatus = false
                    marca.estatus = false;
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Marca no encontrada.");
                }
            }
        }
    }
}
