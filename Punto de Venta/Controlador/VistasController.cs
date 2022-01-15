using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    
    public class VistasController
    {
        private readonly chinahousedbEntities Context;

        public VistasController(chinahousedbEntities context)
        {
            Context = context;
        }

        public List<ViewCorte> CorteProductos(string fecha)
        {
            List<ViewCorte> result = new List<ViewCorte>();
            result = Context.ViewCorte.Where(x => x.fecha == fecha).OrderByDescending(x=>x.Total).ToList();
            return result;
        }
    }
}
