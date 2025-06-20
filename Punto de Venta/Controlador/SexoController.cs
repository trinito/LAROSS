using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class SexoController
    {
        public async Task<List<Sexos>> ObtenerTodosLosSexosAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.Sexos
                    .Where(s => s.estatus == true)
                    .OrderBy(s => s.nombre)
                    .ToListAsync();
            }
        }

        public async Task<int> InsertarSexoAsync(string nombreSexo)
        {
            using (var context = new la_ross_dbEntities())
            {
                var existente = await context.Sexos
                    .FirstOrDefaultAsync(s => s.nombre == nombreSexo);

                if (existente != null)
                {
                    if (!existente.estatus)
                    {
                        // Reactivar si estaba eliminado
                        existente.estatus = true;
                        await context.SaveChangesAsync();
                        return existente.id_sexo;
                    }

                    // Ya existe activo
                    throw new Exception("Ya existe un género con ese nombre.");
                }

                // Crear nuevo
                var nuevo = new Sexos
                {
                    nombre = nombreSexo,
                    estatus = true
                };

                context.Sexos.Add(nuevo);
                await context.SaveChangesAsync();
                return nuevo.id_sexo;
            }
        }

        public async Task ModificarSexoAsync(int idSexo, string nuevoNombre)
        {
            using (var context = new la_ross_dbEntities())
            {
                var sexo = await context.Sexos.FindAsync(idSexo);
                if (sexo != null)
                {
                    sexo.nombre = nuevoNombre;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task EliminarSexoAsync(int idSexo)
        {
            using (var context = new la_ross_dbEntities())
            {
                var sexo = await context.Sexos.FindAsync(idSexo);
                if (sexo != null)
                {
                    sexo.estatus = false; // Eliminación lógica
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
