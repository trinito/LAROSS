using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class DetalleVentaController
    {
        private readonly chinahousedbEntities Context;

        public DetalleVentaController(chinahousedbEntities context)
        {
            Context = context;
        }

        public bool CrearDetalleVenta(int id_venta, List<ProductoCaja> productos)
        {
            bool result = false;

            try
            {
                foreach(var x in productos)
                {
                    DetalleVenta detalle = new DetalleVenta { id_venta = id_venta,id_menu=x.id_menu, nombre = x.nombre, medida = x.medida, cantidad = x.cantidad, precio=x.precio };
                    Context.DetalleVenta.Add(detalle);
                    int resultado = Context.SaveChanges();
                    if (resultado > 0)
                        result = true;
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos, no se pudo registrar el detalle de venta.");
            }

            return result;
        }
    }
}
