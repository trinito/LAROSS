using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Punto_de_Venta.Controlador
{
    public class DetalleVentaController
    {
        public bool CrearDetalleVenta(int id_venta, List<ProductoVentaDTO> productos, DateTime? fecha_editado = null, int? id_usuario_editado = null)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    foreach (var x in productos)
                    {
                        var detalle = new DetalleVenta
                        {
                            id_venta = id_venta,
                            // Aquí debes obtener el id_producto a partir del código de barras
                            id_producto = ObtenerIdProductoPorCodigo(x.CodigoBarras, context),
                            cantidad = x.Cantidad,
                            precio_unitario = x.PrecioVenta,
                            subtotal = x.Total,
                            fecha_editado = fecha_editado,
                            id_usuario_editado = id_usuario_editado
                        };

                        context.DetalleVenta.Add(detalle);
                    }

                    int resultado = context.SaveChanges();
                    return resultado > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar detalle de venta: " + ex.Message);
            }
        }

        private int ObtenerIdProductoPorCodigo(string codigoBarras, la_ross_dbEntities context)
        {
            var producto = context.Articulos.FirstOrDefault(p => p.codigo_barras == codigoBarras || p.codigo_barras_original == codigoBarras);
            if (producto == null)
                throw new Exception($"No se encontró el producto con código: {codigoBarras}");
            return producto.id_producto;
        }
    }
}
