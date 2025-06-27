using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Modelo
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int CantidadProductos { get; set; }
        public decimal Total { get; set; }
        public string FormaPago { get; set; }
        public string Usuario { get; set; } // Opcional, depende de tu relación con tabla Usuarios
        public string Estatus { get; set; } // Texto: "ACTIVA" o "CANCELADA"
        public DateTime? Modificado { get; set; }  // Nullable para la fecha_editado
    }

}
