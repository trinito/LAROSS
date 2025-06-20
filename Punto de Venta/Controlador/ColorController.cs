using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class ColorController
    {
        public async Task<List<Colores>> ObtenerTodosLosColoresAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.Colores
                    .Where(c => c.estatus == true)
                    .OrderBy(c => c.nombre)
                    .ToListAsync();
            }
        }

        public async Task<int> InsertarColorAsync(string nombreColor)
        {
            using (var context = new la_ross_dbEntities())
            {
                var existente = await context.Colores
                    .FirstOrDefaultAsync(c => c.nombre == nombreColor);

                if (existente != null)
                {
                    if (!existente.estatus)
                    {
                        // Reactivar si estaba eliminado
                        existente.estatus = true;
                        await context.SaveChangesAsync();
                        return existente.id_color;
                    }

                    // Ya existe activo
                    throw new Exception("Ya existe un color con ese nombre.");
                }

                // Crear nuevo
                var nuevo = new Colores
                {
                    nombre = nombreColor,
                    estatus = true
                };

                context.Colores.Add(nuevo);
                await context.SaveChangesAsync();
                return nuevo.id_color;
            }
        }

        public async Task ModificarColorAsync(int idColor, string nuevoNombre)
        {
            using (var context = new la_ross_dbEntities())
            {
                var color = await context.Colores.FindAsync(idColor);
                if (color != null)
                {
                    color.nombre = nuevoNombre;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task EliminarColorAsync(int idColor)
        {
            using (var context = new la_ross_dbEntities())
            {
                var color = await context.Colores.FindAsync(idColor);
                if (color != null)
                {
                    color.estatus = false; // Eliminación lógica
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
