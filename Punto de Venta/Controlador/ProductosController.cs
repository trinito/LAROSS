using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class ProductosController
    {
        public async Task<IEnumerable<Menu>> GetProductosAsync()
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    var productos = await context.Menu
                        .Where(p => p.estatus)
                        .ToListAsync();

                    productos.ForEach(p => p.precio = Math.Round(p.precio, 2));
                    return productos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message, ex);
            }
        }

        public Menu GetProducto(string codigo)
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    var producto = context.Menu.FirstOrDefault(p => p.estatus && p.codigo == codigo);

                    if (producto != null)
                        producto.precio = Math.Round(producto.precio, 2);

                    return producto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto: " + ex.Message, ex);
            }
        }

        public bool UpdateProducto(Menu producto)
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    var existente = context.Menu.SingleOrDefault(p => p.id_menu == producto.id_menu);

                    if (existente == null) return false;

                    existente.codigo = producto.codigo;
                    existente.id_categoria = producto.id_categoria;
                    existente.medida = producto.medida;
                    existente.nombre = producto.nombre;
                    existente.precio = producto.precio;

                    context.Entry(existente).State = System.Data.Entity.EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto: " + ex.Message, ex);
            }
        }

        public int InsertProducto(Menu producto)
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    context.Menu.Add(producto);
                    context.SaveChanges();
                    return producto.id_menu;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("updating the entries"))
                {
                    throw new InvalidOperationException("El producto con ese código ya estuvo registrado pero se dio de baja.", ex);
                }
                throw new Exception("Error al insertar producto: " + ex.Message, ex);
            }
        }

        public bool DeleteProducto(string codigo)
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    var producto = context.Menu.SingleOrDefault(p => p.codigo == codigo);

                    if (producto == null) return false;

                    producto.estatus = false;
                    context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto: " + ex.Message, ex);
            }
        }
    }
}
