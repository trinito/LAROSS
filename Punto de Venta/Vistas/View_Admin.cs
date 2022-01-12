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
    public partial class View_Admin : Form
    {
        List<Menu> productos;
        public Menu productoSelect;
        public View_Admin()
        {
            InitializeComponent();
            productos = new List<Menu>();
        }

        private void View_Admin_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            LlenarProductos();
            LlenarCategorias();
        }


        private void LlenarProductos()
        {
            ProductosController productosController = new ProductosController(new chinahousedbEntities());
            productos = productosController.GetProductos().ToList();
            dgv_productos.DataSource = productos;
        }

        private void LlenarCategorias()
        {
            CategoriaController categoriaController = new CategoriaController(new chinahousedbEntities());
            combo_categoria.DataSource = categoriaController.GetCategorias();
        }

        private void combo_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            var x = combo_categoria.SelectedValue.ToString();
        }

        private void button_buscar_Click(object sender, EventArgs e)
        {
            if (dgv_productos.CurrentRow != null)
            {
                productoSelect = (Menu)dgv_productos.CurrentRow.DataBoundItem;



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_productos.CurrentRow != null)
            {
                string message = "¿Estás seguro de eliminar este producto?";
                string title = "¡ALERTA!";

                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Menu producto = (Menu)dgv_productos.CurrentRow.DataBoundItem;
                    dgv_productos.DataSource = null;
                    productos.Remove(producto);
                    dgv_productos.DataSource = productos;
                }
            }
        }
    }
}
