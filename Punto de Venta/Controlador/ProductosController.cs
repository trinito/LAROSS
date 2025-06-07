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
        public async Task<IEnumerable<Articulos>> GetProductosAsync()
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var productos = await context.Articulos
                        .Where(p => p.estatus)
                        .ToListAsync();

                    productos.ForEach(p => p.precio_venta = Math.Round(p.precio_venta, 2));
                    return productos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message, ex);
            }
        }

        public Articulos GetProducto(string codigo)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var producto = context.Articulos.FirstOrDefault(p => p.estatus && p.codigo_barras == codigo);

                    if (producto != null)
                        producto.precio_venta = Math.Round(producto.precio_venta, 2);

                    return producto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto: " + ex.Message, ex);
            }
        }

        public bool UpdateProducto(Articulos producto)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var existente = context.Articulos.SingleOrDefault(p => p.id_producto == producto.id_producto);

                    if (existente == null) return false;

                    existente.codigo_barras = producto.codigo_barras;
                    existente.id_categoria = producto.id_categoria;
                    existente.id_marca = producto.id_marca;
                    existente.nombre = producto.nombre;
                    existente.precio_costo = producto.precio_costo;

                    context.Entry(existente).State = System.Data.Entity.EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto: " + ex.Message, ex);
            }
        }

        public int InsertProducto(Articulos producto)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    context.Articulos.Add(producto);
                    context.SaveChanges();
                    return producto.id_producto;
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
                using (var context = new la_ross_dbEntities())
                {
                    var producto = context.Articulos.SingleOrDefault(p => p.codigo_barras == codigo);

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
