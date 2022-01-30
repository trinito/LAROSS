using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Punto_de_Venta.Modelo.Menu;

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
            try
            {
                List<Menu> menu = Context.Menu.Where(t => t.estatus == true).ToList();
                if (menu != null)
                {
                    foreach (var x in menu)
                    {
                        decimal decimal_aux = x.precio;
                        x.precio = Math.Round(decimal_aux, 2);
                    }
                }
                return menu;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos, no se pudo obtener el producto.");
            }

          
        }

        public Menu GetProducto(string codigo)
        {
            try
            {
                Menu menu = Context.Menu.Where(x => x.estatus == true && x.codigo == codigo).FirstOrDefault();
                if (menu != null)
                {
                    decimal decimal_aux = menu.precio;
                    menu.precio = Math.Round(decimal_aux, 2);
                }

                return menu;

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos, no se pudieron obtener los productos.");
            }
         
                
        }

        public int UpdateProducto(Menu menu)
        {
            try
            {
                Menu result = Context.Menu.SingleOrDefault(x => x.id_menu == menu.id_menu);
                result.codigo = menu.codigo;
                result.id_categoria = menu.id_categoria;
                result.medida = menu.medida;
                result.nombre = menu.nombre;
                result.precio = menu.precio;
                Context.Entry(result).State = System.Data.Entity.EntityState.Modified;

                if (Context.SaveChanges() > 0)
                    return 1;

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos, no se pudo actualizar el producto.");
            }
      
        }

        public int InsertProducto(Menu menu)
        {
            try
            {
                Context.Menu.Add(menu);
                if (Context.SaveChanges() > 0)
                    return menu.id_menu;

                return 0;
            }
            catch (Exception ex)
            {
                if(ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    string message = "El producto con esa código ya estuvo registrado pero se dió de baja.";
                    string title = "ERROR";
                    MessageBox.Show(message, title);
                }
                return 0;
            }
        }
        public int DeleteProducto(Menu menu)
        {
            try
            {
                Menu result = Context.Menu.SingleOrDefault(x => x.codigo == menu.codigo);
                result.estatus = false;
                Context.Entry(result).State = System.Data.Entity.EntityState.Modified;
                if (Context.SaveChanges() > 0)
                    return 1;

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos, no se pudo eliminar el producto.");
            }

         
        }

    }
}
