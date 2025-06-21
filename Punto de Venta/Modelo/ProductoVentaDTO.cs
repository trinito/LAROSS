using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Modelo
{
    public class ProductoVentaDTO
    {
        public string CodigoBarras { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public string Talla { get; set; }
        public string Sexo { get; set; }
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Total => Cantidad * PrecioVenta;
        public int Stock { get; set; }
        public string Foto { get; set; }

    }


}
