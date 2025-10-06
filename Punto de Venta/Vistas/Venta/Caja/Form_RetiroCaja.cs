using Punto_de_Venta.Controlador;
using Punto_de_Venta.Servicios;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Venta.Caja
{
    public partial class Form_RetiroCaja : Form
    {
        decimal saldoInicial = 0;
        public Form_RetiroCaja()
        {
            InitializeComponent();
        }

        private async void Form_RetiroCaja_Load(object sender, EventArgs e)
        {
            await CargarSaldoInicialAsync();
        }

        private async Task CargarSaldoInicialAsync()
        {
            try
            {
                var controller = new CajaMovimientosController();
                // Obtener saldos
                decimal saldoInicial = await controller.ObtenerSaldoInicialAsync();

                // Actualizar campos
                await CargarSaldoActualAsync();
                txt_saldo_inicial.Text = $"{saldoInicial:N2}";
                label5.Text = $"{SesionUsuario.UsuarioActual.nombre} {SesionUsuario.UsuarioActual.apellido}";
                cb_descripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el saldo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarSaldoActualAsync()
        {
            try
            {
                var controller = new CajaMovimientosController();
                // Obtener saldos
                decimal saldoActual = await controller.ObtenerSaldoActualAsync();

                // Actualizar campos
                txt_saldo_actual.Text = $"{saldoActual:N2}";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el saldo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar monto
                if (string.IsNullOrWhiteSpace(txt_monto.Text) || !decimal.TryParse(txt_monto.Text, out decimal monto) || monto <= 0)
                {
                    MessageBox.Show("Por favor ingresa un monto válido mayor a cero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar saldo mostrado
                if (!decimal.TryParse(txt_saldo_actual.Text, out decimal saldoActual))
                {
                    MessageBox.Show("Error al leer el saldo actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar descripción
                string descripcion = cb_descripcion.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("Por favor selecciona un motivo para el retiro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar monto contra saldo
                if (monto > saldoActual)
                {
                    MessageBox.Show(
                       $"El monto ingresado ({monto:C2}) excede el saldo disponible ({saldoActual:C2}).",
                       "Advertencia",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning
                    ); 
                    return;
                }

                // Validar monto contra saldo inical
                if (saldoActual - monto < saldoInicial)
                {
                    MessageBox.Show(
                       $"No se puede realizar el retiro. El saldo restante ({saldoActual - monto:C2}) sería menor al saldo inicial permitido ({saldoInicial:C2}).",
                       "Advertencia",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning
                   );
                    return;
                }

              

                // Confirmar con usuario
                DialogResult confirm = MessageBox.Show(
                    $"¿Estás seguro de retirar ${monto:N2} de la caja?\n\nDescripción: {descripcion}",
                    "Confirmar retiro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm != DialogResult.Yes)
                {
                    // Usuario canceló
                    return;
                }

                // ID del usuario (ajústalo según tu sesión)
                int idUsuario = SesionUsuario.UsuarioActual.id;

                // Guardar en base de datos
                var controller = new CajaMovimientosController();
                bool exito = await controller.RegistrarRetiroAsync(monto, descripcion, idUsuario);

                if (exito)
                {
                    MessageBox.Show("Retiro registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el retiro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite solo números, backspace y un solo punto decimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (var form = new View_DepositoCaja())
            {
                form.ShowDialog();
            }
            await CargarSaldoActualAsync();
        }
    }
}
