using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;

namespace Punto_de_Venta.Controlador
{
    public class DetalleVentaController
    {
        public bool CrearDetalleVenta(int id_venta, List<ProductoCaja> productos)
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
                            id_producto = x.id_producto,
                            cantidad = x.cantidad,
                            precio_unitario = x.precio
                        };

                        context.DetalleVenta.Add(detalle);
                    }

                    // Guardar todos los cambios de una sola vez
                    int resultado = context.SaveChanges();
                    return resultado > 0;
                }
            }
            catch
            {
                return false;
                // Si quieres lanzar excepción personalizada, descomenta la línea de abajo
                // throw new Exception("Error en la base de datos, no se pudo registrar el detalle de venta.");
            }
        }
    }
}
