using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Modelo
{
    public class ProductoCaja
    {
        public int id_producto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string medida { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
    }
}
