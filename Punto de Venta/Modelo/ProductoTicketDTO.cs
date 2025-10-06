using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Modelo
{
    public class ProductoTicketDTO
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
    }

}
