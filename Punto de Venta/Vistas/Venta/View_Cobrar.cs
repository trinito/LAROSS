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

        private void txt_pago_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_pago.Text))
            {
                txt_cambio.Text = "";
                return;
            }

            if (decimal.TryParse(txt_pago.Text, out decimal pagoIngresado))
            {
                pago = pagoIngresado;
                if (pago >= total)
                {
                    cambio = pago - total;
                    txt_cambio.Text = cambio.ToString("C", CultureInfo.CurrentCulture);
                }
                else
                {
                    cambio = 0;
                    txt_cambio.Text = "";
                }
            }
            else
            {
                txt_cambio.Text = "";
            }
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
            if (radio_tarjeta.Checked || radio_transferencia.Checked)
            {
                string metodo = radio_tarjeta.Checked ? "TARJETA" : "TRANSFERENCIA";
                string message = $"¿Deseas confirmar el pago con {metodo}?";
                string title = "Confirmar forma de pago";

                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    forma_pago = metodo;
                    compra = true;
                    pago = total;
                    cambio = 0;
                    this.Close();
                }

                return;
            }

            if (string.IsNullOrWhiteSpace(txt_pago.Text))
            {
                MessageBox.Show("Por favor, ingresa el monto con el que se está pagando.", "Pago requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_pago.Focus();
                return;
            }

            if (pago < total)
            {
                MessageBox.Show("El monto ingresado es menor al total de la venta. Verifica el pago por favor.", "Pago insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_pago.Focus();
                return;
            }

            forma_pago = "EFECTIVO";
            compra = true;
            cambio = pago - total;
            this.Close();
        }


        private void Cancelar()
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_transferencia.Checked)
            {
                txt_pago.Enabled = false;
                txt_pago.ReadOnly = true;
                txt_pago.Text = "";
                txt_cambio.Text = "";
            }
        }
    }
}
