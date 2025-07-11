using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class DashboardController
    {
        // Obtiene el total de ventas del día especificado
        public async Task<decimal> ObtenerTotalVentasDiaAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.Venta
                    .Where(v => DbFunctions.TruncateTime(v.fecha) == fecha.Date && v.estatus)
                    .SumAsync(v => (decimal?)v.total) ?? 0m;
            }
        }

        // Obtiene la cantidad de tickets (ventas) del día especificado
        public async Task<int> ObtenerCantidadTicketsDiaAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.Venta
                    .CountAsync(v => DbFunctions.TruncateTime(v.fecha) == fecha.Date && v.estatus);
            }
        }

        // Obtiene un diccionario con ventas por forma de pago en el día especificado
        public async Task<Dictionary<string, decimal>> ObtenerVentasPorFormaPagoDiaAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                var resultados = await context.Venta
                    .Where(v => DbFunctions.TruncateTime(v.fecha) == fecha.Date && v.estatus)
                    .GroupBy(v => v.forma_pago)
                    .Select(g => new { FormaPago = g.Key, Total = g.Sum(v => v.total) })
                    .ToListAsync();

                return resultados.ToDictionary(x => x.FormaPago, x => x.Total);
            }
        }

        // Obtiene lista con ventas totales agrupadas por día para el mes especificado
        public async Task<List<(DateTime Fecha, decimal Total)>> ObtenerVentasPorDiaDelMesAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                var primerDia = new DateTime(fecha.Year, fecha.Month, 1);
                var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

                var query = await context.Venta
                    .Where(v => v.fecha >= primerDia && v.fecha <= ultimoDia && v.estatus)
                    .GroupBy(v => DbFunctions.TruncateTime(v.fecha))
                    .Select(g => new
                    {
                        Fecha = g.Key.Value,
                        Total = g.Sum(v => v.total)
                    })
                    .ToListAsync();

                return query.Select(x => (x.Fecha, x.Total)).ToList();
            }
        }

        // Obtiene los productos más vendidos del mes con su cantidad vendida, limitado a topN
        public async Task<List<(string NombreProducto, int CantidadVendida)>> ObtenerProductosMasVendidosDelMesAsync(DateTime fecha, int topN = 5)
        {
            using (var context = new la_ross_dbEntities())
            {
                var primerDia = new DateTime(fecha.Year, fecha.Month, 1);
                var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

                var query = await (from detalle in context.DetalleVenta
                                   join venta in context.Venta on detalle.id_venta equals venta.id_venta
                                   join producto in context.Articulos on detalle.id_producto equals producto.id_producto
                                   where venta.fecha >= primerDia && venta.fecha <= ultimoDia && venta.estatus
                                   group detalle by producto.nombre into grupo
                                   select new
                                   {
                                       NombreProducto = grupo.Key,
                                       Cantidad = grupo.Sum(x => x.cantidad)
                                   })
                                   .OrderByDescending(x => x.Cantidad)
                                   .Take(topN)
                                   .ToListAsync();

                return query.Select(x => (x.NombreProducto, x.Cantidad)).ToList();
            }
        }

        // Obtiene detalle de productos vendidos en el día especificado
        public async Task<List<(string NombreProducto, int CantidadVendida, decimal TotalProducto)>> ObtenerDetalleProductosVendidosDiaAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                var query = await (from detalle in context.DetalleVenta
                                   join venta in context.Venta on detalle.id_venta equals venta.id_venta
                                   join producto in context.Articulos on detalle.id_producto equals producto.id_producto
                                   where DbFunctions.TruncateTime(venta.fecha) == fecha.Date && venta.estatus
                                   group detalle by producto.nombre into grupo
                                   select new
                                   {
                                       NombreProducto = grupo.Key,
                                       CantidadVendida = grupo.Sum(x => x.cantidad),
                                       TotalProducto = grupo.Sum(x => x.subtotal)   
                                   })
                                   .ToListAsync();

                return query.Select(x => (x.NombreProducto, x.CantidadVendida, x.TotalProducto)).ToList();
            }
        }

        // Obtiene resumen de ventas (corte de caja) para un día
        public async Task<(int TotalVentas, decimal MontoTotal, decimal TotalEfectivo, decimal TotalTarjeta, decimal TotalTransferencia)>
            ObtenerResumenVentasDiaAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                var query = await context.Venta
                    .Where(v => DbFunctions.TruncateTime(v.fecha) == fecha.Date && v.estatus)
                    .ToListAsync();

                int totalVentas = query.Count;
                decimal montoTotal = query.Sum(v => v.total);
                decimal totalEfectivo = query.Where(v => v.forma_pago == "Efectivo").Sum(v => v.total);
                decimal totalTarjeta = query.Where(v => v.forma_pago == "Tarjeta").Sum(v => v.total);
                decimal totalTransferencia = query.Where(v => v.forma_pago == "Transferencia").Sum(v => v.total);

                return (totalVentas, montoTotal, totalEfectivo, totalTarjeta, totalTransferencia);
            }
        }



    }
}
