using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Punto_de_Venta.Controlador
{
    public class CategoriaController
    {
        public IEnumerable<Categoria> GetCategorias()
        {
            using (var context = new la_ross_dbEntities())
            {
                return context.Categoria
                              .Where(t => t.estatus == true)
                              .ToList();
            }
        }
    }
}
