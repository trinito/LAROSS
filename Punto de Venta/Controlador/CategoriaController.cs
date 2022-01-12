using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class CategoriaController
    {
        private readonly chinahousedbEntities Context;

        public CategoriaController(chinahousedbEntities context)
        {
            Context = context;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            return Context.Categoria.Where(t => t.estatus == true).ToList(); ;
        }
    }
}
