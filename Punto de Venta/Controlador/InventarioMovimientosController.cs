using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class InventarioMovimientosController
    {
        public async Task<List<MovimientoDTO>> ObtenerMovimientosAsync(DateTime fechaSeleccionada)
        {
            using (var context = new la_ross_dbEntities())
            {
                // Definimos rango de inicio y fin del día
                DateTime fechaInicio = fechaSeleccionada.Date;
                DateTime fechaFin = fechaSeleccionada.Date.AddDays(1).AddTicks(-1);

                return await context.InventarioMovimientos
                .Where(m => m.fecha >= fechaInicio && m.fecha <= fechaFin) // filtro primero
                .Join(context.Usuarios,
                      m => m.id_usuario,
                      u => u.id,
                      (m, u) => new MovimientoDTO
                      {
                          IdMovimiento = m.id_movimiento,
                          CodigoBarras = m.codigo_barras,
                          NombreProducto = m.nombre_producto,
                          Cantidad = m.cantidad,
                          StockAntes = m.stock_antes,
                          StockDespues = m.stock_despues,
                          Usuario = u.nombre + " " + u.apellido,
                          Motivo = m.motivo,
                          Fecha = m.fecha
                      })
                .OrderBy(m => m.Fecha)
                .ToListAsync();

            }
        }

        public class MovimientoDTO
        {
            public int IdMovimiento { get; set; }
            public string CodigoBarras { get; set; }
            public string NombreProducto { get; set; }
            public int Cantidad { get; set; }
            public int StockAntes { get; set; }
            public int StockDespues { get; set; }
            public string Usuario { get; set; }
            public string Motivo { get; set; }
            public DateTime Fecha { get; set; }
        }

    }
}
