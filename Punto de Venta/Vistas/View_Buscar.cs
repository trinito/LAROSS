using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Punto_de_Venta.Modelo.Menu;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Buscar : Form
    {
        public Menu productoSelect;
        IEnumerable<Menu> productos;
        public View_Buscar()
        {
            InitializeComponent();
            productos =  Enumerable.Empty<Menu>();
        }

        private void View_Buscar_KeyDown(object sender, KeyEventArgs e)
        {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                    Seleccionar();
                    break;
                    case Keys.Escape:
                    Salir();
                        break;

                    default:
                        break;
                }
        }

        private void View_Buscar_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            ProductosController productosController = new ProductosController(new chinahousedbEntities());
            productos = productosController.GetProductos();
            dgv_productos.DataSource = productos;
        }

        private void txt_producto_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_producto.Text))
            {
                dgv_productos.DataSource = null;
                dgv_productos.DataSource = productos;

            }
            else
            {
                dgv_productos.DataSource = null;
                dgv_productos.DataSource = productos.Where(x => x.nombre.Contains(txt_producto.Text.ToUpper())).ToList();
            }
        }

 

        private void Salir()
        {
            this.Close();
        }

        private void Seleccionar()
        {
            if (dgv_productos.CurrentRow != null)
            {
                productoSelect = (Menu)dgv_productos.CurrentRow.DataBoundItem;
            }
            this.Close();
        }

        private void btn_seleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Salir();
        }
    }
}
