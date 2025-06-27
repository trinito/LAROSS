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

        public async Task<List<VentaDTO>> ObtenerVentasDelDiaAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                var query = from v in context.Venta
                            where DbFunctions.TruncateTime(v.fecha) == DbFunctions.TruncateTime(fecha)
                            select new VentaDTO
                            {
                                IdVenta = v.id_venta,
                                Fecha = v.fecha,
                                Hora = v.hora,
                                CantidadProductos = v.cantidad_productos,
                                Total = v.total,
                                FormaPago = v.forma_pago,
                                Usuario = v.id_usuario_editado.HasValue
                                    ? context.Usuarios
                                        .Where(u => u.id == v.id_usuario_editado)
                                        .Select(u => u.nombre + " " + u.apellido)
                                        .FirstOrDefault()
                                    : "N/A",
                                Estatus = v.estatus ? "ACTIVA" : "CANCELADA",
                                Modificado = v.fecha_editado
                            };

                return await query.ToListAsync();
            }
        }


        public async Task<decimal> ObtenerTotalVentasDelDiaAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.Venta
                    .Where(v => DbFunctions.TruncateTime(v.fecha) == DbFunctions.TruncateTime(fecha) && v.estatus)
                    .SumAsync(v => (decimal?)v.total) ?? 0m;
            }
        }


        public async Task<bool> CancelarVentaAsync(int idVenta, int idUsuario)
        {
            using (var context = new la_ross_dbEntities())
            {
                var venta = await context.Venta.FindAsync(idVenta);
                if (venta == null) return false;

                venta.estatus = false;
                venta.fecha_editado = DateTime.Now;
                venta.id_usuario_editado = idUsuario;

                // Obtener detalles de la venta
                var detalles = await context.DetalleVenta
                    .Where(d => d.id_venta == idVenta)
                    .ToListAsync();

                // Regresar stock a cada producto
                foreach (var detalle in detalles)
                {
                    var producto = await context.Articulos.FindAsync(detalle.id_producto);
                    if (producto != null)
                    {
                        producto.stock += detalle.cantidad;
                    }
                }

                return await context.SaveChangesAsync() > 0;
            }
        }


    }
}
