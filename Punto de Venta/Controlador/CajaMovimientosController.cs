using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Punto_de_Venta.Modelo;

namespace Punto_de_Venta.Controlador
{
    public class CajaMovimientosController
    {
        /// <summary>
        /// Verifica si ya existe un movimiento inicial de caja para hoy.
        /// </summary>
        public async Task<bool> VerificarInicioCajaAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                DateTime hoy = DateTime.Today;
                return await context.CajaMovimientos
                    .AnyAsync(m => m.tipo_movimiento == "INICIAL" && DbFunctions.TruncateTime(m.fecha) == hoy);
            }
        }

        /// <summary>
        /// Registra el monto inicial de la caja.
        /// </summary>
        public async Task<bool> RegistrarMovimientoInicialAsync(decimal monto, int idUsuario, bool esAutomatico = false)
        {
            using (var context = new la_ross_dbEntities())
            {
                var movimiento = new CajaMovimientos
                {
                    tipo_movimiento = "INICIAL",
                    monto = monto,
                    descripcion = esAutomatico ? "SALDO AUTOMÁTICO DE AYER" : "APERTURA DE CAJA",
                    id_usuario = idUsuario,
                    fecha = DateTime.Now
                };

                context.CajaMovimientos.Add(movimiento);
                return await context.SaveChangesAsync() > 0;
            }
        }


        /// <summary>
        /// Registra una venta en caja (solo para efectivo).
        /// </summary>
        public async Task<bool> RegistrarVentaEnCajaAsync(decimal monto, int idUsuario)
        {
            using (var context = new la_ross_dbEntities())
            {
                var movimiento = new CajaMovimientos
                {
                    tipo_movimiento = "VENTA",
                    monto = monto,
                    descripcion = "VENTA EFECTIVO",
                    id_usuario = idUsuario,
                    fecha = DateTime.Now
                };

                context.CajaMovimientos.Add(movimiento);
                return await context.SaveChangesAsync() > 0;
            }
        }

        /// <summary>
        /// Registra un retiro de caja.
        /// </summary>
        public async Task<bool> RegistrarRetiroAsync(decimal monto, string descripcion, int idUsuario)
        {
            using (var context = new la_ross_dbEntities())
            {
                string tipo_mov = "RETIRO";

                if (descripcion == "CIERRE DE CAJA")
                    tipo_mov = "CIERRE";

                var movimiento = new CajaMovimientos
                {
                    tipo_movimiento = tipo_mov,
                    monto = monto,
                    descripcion = descripcion ?? "RETIRO DE EFECTIVO",
                    id_usuario = idUsuario,
                    fecha = DateTime.Now
                };

                context.CajaMovimientos.Add(movimiento);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<decimal> ObtenerSaldoActualAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                DateTime hoy = DateTime.Today;

                var movimientos = await context.CajaMovimientos
      .Where(m => DbFunctions.TruncateTime(m.fecha) == hoy)
      .ToListAsync();

                decimal ingresos = movimientos
                    .Where(m => m.tipo_movimiento == "INICIAL"
                             || m.tipo_movimiento == "VENTA"
                             || m.tipo_movimiento == "INGRESO") // 👈 agregar ingreso manual
                    .Sum(m => (decimal?)m.monto) ?? 0m;


                decimal retiros = movimientos
                    .Where(m => m.tipo_movimiento == "RETIRO"
                             || m.tipo_movimiento == "CIERRE")
                    .Sum(m => (decimal?)m.monto) ?? 0m;

                return ingresos - retiros;
            }

        }

        public async Task<decimal> ObtenerSaldoInicialAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                DateTime hoy = DateTime.Today;

                var movimientos = await context.CajaMovimientos
      .Where(m => DbFunctions.TruncateTime(m.fecha) == hoy)
      .ToListAsync();

                decimal saldoInicial = movimientos
                    .Where(m => m.tipo_movimiento == "INICIAL")
                    .Sum(m => (decimal?)m.monto) ?? 0m;


                return saldoInicial;
            }

        }


        /// <summary>
        /// Registra un ingreso manual de efectivo a la caja.
        /// </summary>
        public async Task<bool> RegistrarIngresoAsync(decimal monto, string descripcion, int idUsuario)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto debe ser mayor a 0.", nameof(monto));

            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("Debe ingresar una descripción válida.", nameof(descripcion));

            using (var context = new la_ross_dbEntities())
            {
                var movimiento = new CajaMovimientos
                {
                    tipo_movimiento = "INGRESO",
                    monto = monto,
                    descripcion = descripcion,
                    id_usuario = idUsuario,
                    fecha = DateTime.Now
                };

                context.CajaMovimientos.Add(movimiento);
                return await context.SaveChangesAsync() > 0;
            }
        }

        /// <summary>
        /// Obtiene el saldo final de un día específico.
        /// </summary>
        public async Task<decimal> ObtenerSaldoPorDiaAsync(DateTime fecha)
        {
            using (var context = new la_ross_dbEntities())
            {
                var movimientos = await context.CajaMovimientos
                    .Where(m => DbFunctions.TruncateTime(m.fecha) == fecha.Date)
                    .ToListAsync();

                decimal ingresos = movimientos
                    .Where(m => m.tipo_movimiento == "INICIAL"
                             || m.tipo_movimiento == "VENTA"
                             || m.tipo_movimiento == "INGRESO")
                    .Sum(m => (decimal?)m.monto) ?? 0m;

                decimal retiros = movimientos
                    .Where(m => m.tipo_movimiento == "RETIRO"
                             || m.tipo_movimiento == "CIERRE")
                    .Sum(m => (decimal?)m.monto) ?? 0m;

                return ingresos - retiros;
            }
        }

    }
}
