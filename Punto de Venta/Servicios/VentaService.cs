using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Punto_de_Venta.Controlador
{
    public class VentaService
    {
        public bool RealizarVenta(DateTime fecha, string hora, List<ProductoVentaDTO> productos, string formaPago, int idUsuario = 0)
        {
            using (var context = new la_ross_dbEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Crear venta
                        var venta = new Venta
                        {
                            fecha = fecha,
                            hora = hora,
                            cantidad_productos = productos.Sum(p => p.Cantidad),
                            total = productos.Sum(p => p.PrecioVenta * p.Cantidad),
                            estatus = true,
                            forma_pago = formaPago,
                            id_usuario_editado = idUsuario,
                            fecha_editado = DateTime.Now
                        };
                        context.Venta.Add(venta);
                        context.SaveChanges();

                        // 2. Crear detalle venta
                        foreach (var p in productos)
                        {
                            var articulo = context.Articulos.FirstOrDefault(a => a.codigo_barras == p.CodigoBarras || a.codigo_barras_original == p.CodigoBarras);
                            if (articulo == null)
                                throw new Exception($"Producto con código {p.CodigoBarras} no encontrado.");

                            if (articulo.stock < p.Cantidad)
                                throw new Exception($"Producto {articulo.nombre} sin stock suficiente.");

                            var detalle = new DetalleVenta
                            {
                                id_venta = venta.id_venta,
                                id_producto = articulo.id_producto,
                                cantidad = p.Cantidad,
                                precio_unitario = p.PrecioVenta,
                                subtotal = p.PrecioVenta * p.Cantidad,
                                fecha_editado = DateTime.Now,
                                id_usuario_editado = idUsuario
                            };
                            context.DetalleVenta.Add(detalle);

                            // 3. Actualizar stock
                            articulo.stock -= p.Cantidad;
                            context.Entry(articulo).State = EntityState.Modified;
                        }

                        context.SaveChanges();

                        // 4. Commit transacción
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al realizar la venta: " + ex.Message);
                    }
                }
            }
        }
    }
}
