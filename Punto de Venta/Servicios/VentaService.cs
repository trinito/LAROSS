using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class VentaService
    {
        public async Task<bool> RealizarVentaAsync(DateTime fecha, string hora, List<ProductoVentaDTO> productos, string formaPago, decimal pagoVenta, decimal cambioVenta, int idUsuario = 0)
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
                            fecha_editado = DateTime.Now,
                            pago = pagoVenta,
                            cambio = cambioVenta
                        };
                        context.Venta.Add(venta);
                        await context.SaveChangesAsync();

                        // 2. Crear detalle venta
                        foreach (var p in productos)
                        {
                            var articulo = await context.Articulos
                                .FirstOrDefaultAsync(a => a.codigo_barras == p.CodigoBarras || a.codigo_barras_original == p.CodigoBarras);

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

                            // Marca temporal para que el trigger lo ignore
                            context.Database.ExecuteSqlCommand(
                                "SET CONTEXT_INFO @origenVenta",
                                new SqlParameter("@origenVenta", System.Text.Encoding.UTF8.GetBytes("venta"))
                            );

                            context.Entry(articulo).State = EntityState.Modified;

                        }

                        await context.SaveChangesAsync();

                        // 4. Commit transacción
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al realizar la venta: " + ex.Message, ex);
                    }
                }
            }
        }
    }
}
