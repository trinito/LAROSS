using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class TallasController
    {
        public async Task<List<Tallas>> ObtenerTodasLasTallasAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.Tallas
                    .Where(t => t.estatus == true)
                    .OrderBy(t => t.nombre)
                    .ToListAsync();
            }
        }

        public async Task<int> InsertarTallaAsync(string nombreTalla)
        {
            using (var context = new la_ross_dbEntities())
            {
                var existente = await context.Tallas
                    .FirstOrDefaultAsync(t => t.nombre == nombreTalla);

                if (existente != null)
                {
                    if (!existente.estatus)
                    {
                        // Reactivar si estaba eliminada
                        existente.estatus = true;
                        await context.SaveChangesAsync();
                        return existente.id_talla;
                    }

                    // Ya existe activa
                    throw new Exception("Ya existe una talla con ese nombre.");
                }

                // No existe, se crea
                var nueva = new Tallas
                {
                    nombre = nombreTalla,
                    estatus = true
                };

                context.Tallas.Add(nueva);
                await context.SaveChangesAsync();
                return nueva.id_talla;
            }
        }

        public async Task ModificarTallaAsync(int idTalla, string nuevoNombre)
        {
            using (var context = new la_ross_dbEntities())
            {
                var talla = await context.Tallas.FindAsync(idTalla);
                if (talla != null)
                {
                    talla.nombre = nuevoNombre;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task EliminarTallaAsync(int idTalla)
        {
            using (var context = new la_ross_dbEntities())
            {
                var talla = await context.Tallas.FindAsync(idTalla);
                if (talla != null)
                {
                    talla.estatus = false; // Eliminación lógica
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
