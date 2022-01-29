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

        public List<ViewCorte> CorteProductos(DateTime fecha)
        {
            try
            {
                List<ViewCorte> result = new List<ViewCorte>();
                result = Context.ViewCorte.Where(x => x.fecha.Month == fecha.Month).OrderByDescending(x => x.Total).ToList();
                if (result != null && result.Count > 0)
                    return result;
            }
            catch (Exception e)
            {

            }

         return null;
        }

        public List<ViewCorte> ReporteVentasMes(DateTime fecha)
        {
            List<ViewCorte> result = new List<ViewCorte>();
            result = Context.ViewCorte.Where(x => x.fecha == fecha).OrderByDescending(x => x.Total).ToList();

            // result = Context.ViewCorte.Where(x => x.fecha.Month == fecha.Month).OrderByDescending(x => x.Total).ToList();
            if (result != null && result.Count > 0)
                return result;

            return null;
        }
    }
}
