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
                using (var context = new chinahousedbEntities())
                {
                    foreach (var x in productos)
                    {
                        var detalle = new DetalleVenta
                        {
                            id_venta = id_venta,
                            id_menu = x.id_menu,
                            nombre = x.nombre,
                            medida = x.medida,
                            cantidad = x.cantidad,
                            precio = x.precio
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
