using Punto_de_Venta.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Inventario
{
    public partial class View_Tickets: Form
    {
        string nombreProducto;
        string codigoProducto;
        public View_Tickets(string nombre, string codigo)
        {
            InitializeComponent();
            nombreProducto = nombre;
            codigoProducto = codigo;
        }

        private void View_Tickets_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Imprimir();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;

                default:
                    break;
            }
        }

        private async Task Imprimir()
        {
            await ImprimirCodigosDeBarras.ImprimirCodigoAsync(nombreProducto, codigoProducto, Convert.ToInt32(txt_numero.Text));
            this.Close();
        }

        private void txt_numero_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite solo dígitos y control (como Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_numero.Text))
                MessageBox.Show("Ingresar un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                Imprimir();
        }
    }
}
