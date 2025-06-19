using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class CategoriasController
    {
        public async Task<List<Categorias>> ObtenerTodasLasCategoriasAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.Categorias
                    .Where(c => c.estatus == true)
                    .OrderBy(c => c.nombre)
                    .ToListAsync();
            }
        }


        public async Task<int> InsertarCategoriaAsync(string nombreCategoria)
        {
            using (var context = new la_ross_dbEntities())
            {
                var existente = await context.Categorias
                    .FirstOrDefaultAsync(c => c.nombre == nombreCategoria);

                if (existente != null)
                {
                    if (!existente.estatus)
                    {
                        // Reactivar si estaba eliminada
                        existente.estatus = true;
                        await context.SaveChangesAsync();
                        return existente.id_categoria;
                    }

                    // Ya existe activa, lanzar excepción o notificar
                    throw new Exception("Ya existe una categoría con ese nombre.");
                }

                // No existe, la creamos
                var nueva = new Categorias
                {
                    nombre = nombreCategoria,
                    estatus = true
                };

                context.Categorias.Add(nueva);
                await context.SaveChangesAsync();
                return nueva.id_categoria;
            }
        }


        public async Task ModificarCategoriaAsync(int idCategoria, string nuevoNombre)
        {
            using (var context = new la_ross_dbEntities())
            {
                var categoria = await context.Categorias.FindAsync(idCategoria);
                if (categoria != null)
                {
                    categoria.nombre = nuevoNombre;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task EliminarCategoriaAsync(int idCategoria)
        {
            using (var context = new la_ross_dbEntities())
            {
                var categoria = await context.Categorias.FindAsync(idCategoria);
                if (categoria != null)
                {
                    categoria.estatus = false; // eliminación lógica
                    await context.SaveChangesAsync();
                }
            }
        }

    }
}
