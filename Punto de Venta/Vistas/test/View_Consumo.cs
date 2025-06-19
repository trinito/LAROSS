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
    public partial class View_Consumo : Form
    {
        public decimal precio_consumo = 0;
        public View_Consumo()
        {
            InitializeComponent();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            AsignarConsumo();
        }

        private void txt_precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                AsignarConsumo();
            }
        }

        private void AsignarConsumo()
        {
            if (string.IsNullOrEmpty(txt_precio.Text))
            {
                string message = "Favor de ingresar un precio...";
                string title = "Mensaje";
                MessageBox.Show(message, title);
            }
            else
            {
                precio_consumo = Convert.ToDecimal(txt_precio.Text);
                this.Close();
            }
        }
    }
}
