using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Punto_de_Venta.Controlador
{
    public class VentaController
    {
        public int CrearVenta(DateTime fecha, string hora, int cantidad_productos, decimal total, bool estatus, string forma_pago)
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    Venta venta = new Venta
                    {
                        fecha = fecha,
                        hora = hora,
                        cantidad_productos = cantidad_productos,
                        total = total,
                        estatus = estatus,
                        forma_pago = forma_pago
                    };

                    context.Venta.Add(venta);
                    int x = context.SaveChanges();

                    return x > 0 ? venta.id_venta : 0;
                }
            }
            catch
            {
                throw new Exception("Error en la base de datos, no se pudo registrar la venta.");
            }
        }

        public decimal[] TotalesCorte(DateTime fecha)
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    decimal efectivo = context.Venta
                        .Where(x => x.fecha == fecha && x.forma_pago == "EFECTIVO")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    decimal tarjeta = context.Venta
                        .Where(x => x.fecha == fecha && x.forma_pago == "TARJETA")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    return new decimal[] { efectivo, tarjeta, efectivo + tarjeta };
                }
            }
            catch
            {
                return null;
            }
        }

        public decimal[] TotalesMes(DateTime fecha)
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    int mes = fecha.Month;

                    decimal efectivo = context.Venta
                        .Where(x => x.fecha.Month == mes && x.forma_pago == "EFECTIVO")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    decimal tarjeta = context.Venta
                        .Where(x => x.fecha.Month == mes && x.forma_pago == "TARJETA")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    return new decimal[] { efectivo, tarjeta, efectivo + tarjeta };
                }
            }
            catch
            {
                return null;
            }
        }

        public int NumTicket()
        {
            try
            {
                using (var context = new chinahousedbEntities())
                {
                    return context.Venta.Any() ? context.Venta.Max(x => x.id_venta) : 0;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
