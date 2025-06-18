using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Productos: Form
    {
        public View_Productos()
        {
            InitializeComponent();
        }

        private void View_Productos_Load(object sender, EventArgs e)
        {
            btn_productos.BackColor = Color.FromArgb(24, 44, 61);
        }
    }
}
