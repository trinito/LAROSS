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
        IEnumerable<Menu> productos;
        public View_Admin()
        {
            InitializeComponent();
            productos = Enumerable.Empty<Menu>();
        }

        private void View_Admin_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            ProductosController productosController = new ProductosController(new chinahousedbEntities());
            productos = productosController.GetProductos();
            dgv_productos.DataSource = productos;
        }
    }
}
