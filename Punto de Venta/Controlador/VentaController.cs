using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class VentaController
    {
        public int CrearVenta(DateTime fecha, string hora, int cantidad_productos, decimal total, bool estatus, string forma_pago, DateTime? fecha_editado = null, int? id_usuario_editado = null)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    Venta venta = new Venta
                    {
                        fecha = fecha,
                        hora = hora,
                        cantidad_productos = cantidad_productos,
                        total = total,
                        estatus = estatus,
                        forma_pago = forma_pago,
                        fecha_editado = fecha_editado,
                        id_usuario_editado = id_usuario_editado
                    };

                    context.Venta.Add(venta);
                    int x = context.SaveChanges();

                    return x > 0 ? venta.id_venta : 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos, no se pudo registrar la venta. " + ex.Message);
            }
        }

        public decimal[] TotalesCorte(DateTime fecha)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    decimal efectivo = context.Venta
                        .Where(x => x.fecha == fecha && x.forma_pago == "EFECTIVO")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    decimal tarjeta = context.Venta
                        .Where(x => x.fecha == fecha && x.forma_pago == "TARJETA")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    decimal transferencia = context.Venta
                        .Where(x => x.fecha == fecha && x.forma_pago == "TRANSFERENCIA")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    return new decimal[] { efectivo, tarjeta, transferencia, efectivo + tarjeta + transferencia };
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
                using (var context = new la_ross_dbEntities())
                {
                    int mes = fecha.Month;

                    decimal efectivo = context.Venta
                        .Where(x => x.fecha.Month == mes && x.forma_pago == "EFECTIVO")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    decimal tarjeta = context.Venta
                        .Where(x => x.fecha.Month == mes && x.forma_pago == "TARJETA")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    decimal transferencia = context.Venta
                        .Where(x => x.fecha.Month == mes && x.forma_pago == "TRANSFERENCIA")
                        .Sum(x => (decimal?)x.total) ?? 0;

                    return new decimal[] { efectivo, tarjeta, transferencia, efectivo + tarjeta + transferencia };
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> NumTicketAsync()
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    if (await context.Venta.AnyAsync())
                    {
                        return await context.Venta.MaxAsync(x => x.id_venta);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
