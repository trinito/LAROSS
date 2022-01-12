using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class ProductosController
    {
        private readonly chinahousedbEntities Context;

        public ProductosController(chinahousedbEntities context)
        {
            Context = context;
        }

        public IEnumerable<Menu> GetProductos()
        {   
            List<Menu> menu = Context.Menu.Where(t => t.estatus == true).ToList();
            if(menu!=null)
            {
                foreach (var x in menu)
                {
                    decimal decimal_aux = x.precio;
                    x.precio = Math.Round(decimal_aux, 2);
                }
            }
            return menu;
        }

        public Menu GetProducto(string codigo)
        {
            Menu menu = Context.Menu.Where(x => x.estatus == true && x.codigo == codigo).FirstOrDefault();
            if(menu!=null)
            {
                decimal decimal_aux = menu.precio;
                menu.precio = Math.Round(decimal_aux, 2);
            }

            return menu;
                
        }
    }
}
