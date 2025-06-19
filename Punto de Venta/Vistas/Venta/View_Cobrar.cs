using System;
using System.Globalization;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Cobrar : Form
    {
        public decimal total = 0;
        public decimal cambio = 0;
        public decimal pago = 0;

        public bool compra = false;
        public string forma_pago = "";
        public View_Cobrar()
        {
            InitializeComponent();
        }

        private void View_Cobrar_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Cobrar();
                    break;
                case Keys.Escape:
                    Cancelar();
                    break;

                default:
                    break;
            }
        }

        private void View_Cobrar_Load(object sender, System.EventArgs e)
        {
            txt_total.Text = total.ToString("C", CultureInfo.CurrentCulture);
        }

        private void txt_pago_TextChanged(object sender, System.EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_pago.Text))
               return;

            pago = Convert.ToDecimal(((TextBox)sender).Text);
            if (pago > total)
            {
                cambio = pago - total;
                txt_cambio.Text = cambio.ToString("C", CultureInfo.CurrentCulture);
            }
            else
                txt_cambio.Text = "";
        }

        private void txt_pago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void radio_tarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if(radio_tarjeta.Checked)
            {
                txt_pago.Enabled = false;
                txt_pago.ReadOnly = true;
                txt_pago.Text = "";
                txt_cambio.Text = "";
            }
        }

        private void radio_efectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_efectivo.Checked)
            {
                txt_pago.Enabled = true;
                txt_pago.ReadOnly = false;
                txt_pago.Focus();
            }
        }

        private void btn_cobrar_Click(object sender, EventArgs e)
        {
            Cobrar();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cobrar()
        {
            if(radio_tarjeta.Checked)
            {
                string message = "La venta se realizará con TARJETA ¿Estás seguro?";
                string title = "¡Mensaje!";
                
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    forma_pago = "TARJETA";
                    compra = true;
                    pago = total;
                    this.Close();
                }

                    return;
            }

            if(string.IsNullOrEmpty(txt_pago.Text))
            {
                string message = "Error al cobrar, favor de ingresar el pago";
                string title = "¡Mensaje!";
                MessageBox.Show(message, title);
                txt_pago.Focus();
            }
            else if (pago < total)
            {
                string message = "Error al cobrar, el pago es menor que el total";
                string title = "¡Alerta!";
                MessageBox.Show(message, title);
                txt_pago.Focus();
            }
            else
            {
                forma_pago = "EFECTIVO";
                compra = true;
                this.Close();
            }
        }

        private void Cancelar()
        {
            this.Close();
        }
    }
}
