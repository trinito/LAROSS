using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Modelo
{
    public class ProductoInventarioFisicoDTO
    {
        public int IdDetalle { get; set; }          // Para poder registrar el conteo después
        public string CodigoBarras { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public string Talla { get; set; }
        public string Sexo { get; set; }
        public string Categoria { get; set; }
        public int CantidadContada { get; set; }
        public int StockSistema { get; set; }
        public int Diferencia { get; set; }
    }
}
