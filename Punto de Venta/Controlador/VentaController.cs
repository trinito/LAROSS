using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class VentaController
    {
        private readonly chinahousedbEntities Context;

        public VentaController(chinahousedbEntities context)
        {
            Context = context;
        }

        public int CrearVenta(DateTime fecha, string hora, int cantidad_productos, decimal total, bool estatus, string forma_pago)
        {
            int result = 0;
            try
            {
                Venta venta = new Venta { fecha = fecha, hora = hora, cantidad_productos = cantidad_productos, total = total, estatus = estatus, forma_pago = forma_pago };
                Context.Venta.Add(venta);
                int x = Context.SaveChanges();
                if (x > 0)
                {
                    result = venta.id_venta;
                }
                else
                    return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos, no se pudo registrar la venta.");
            }

            return result;
        }

        public decimal[] TotalesCorte(DateTime fecha)
        {
            decimal [] result = new decimal[3];
            result[0] = 0;
            result[1] = 0;
            try
            {
                if (Context.Venta.Any(x=> x.fecha== fecha && x.forma_pago == "EFECTIVO"))
                    result[0] = Context.Venta.Where(x => x.fecha == fecha && x.forma_pago == "EFECTIVO").Sum(x => x.total);

                if (Context.Venta.Any(x => x.fecha == fecha && x.forma_pago == "TARJETA"))
                    result[1] = Context.Venta.Where(x => x.fecha == fecha && x.forma_pago == "TARJETA").Sum(x => x.total);
                result[2] = result[0] + result[1];

                return result;
            }
            catch(Exception e)
            {
                
            }
            return null;
        }

        public decimal[] TotalesMes(DateTime fecha)
        {
            decimal[] result = new decimal[3];
            result[0] = 0;
            result[1] = 0;
            try
            {
                if (Context.Venta.Any(x => x.fecha.Month == fecha.Month && x.forma_pago == "EFECTIVO"))
                    result[0] = Context.Venta.Where(x => x.fecha.Month == fecha.Month && x.forma_pago == "EFECTIVO").Sum(x => x.total);

                if (Context.Venta.Any(x => x.fecha.Month == fecha.Month && x.forma_pago == "TARJETA"))
                    result[1] = Context.Venta.Where(x => x.fecha.Month == fecha.Month && x.forma_pago == "TARJETA").Sum(x => x.total);
                result[2] = result[0] + result[1];

                return result;
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public int NumTicket()
        {
            try
            {
                return Context.Venta.Max(x => x.id_venta);
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
    }
}
