using Punto_de_Venta.Controlador;
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

namespace Punto_de_Venta.Vistas.Venta.Caja
{
    public partial class View_DepositoCaja: Form
    {
        public View_DepositoCaja()
        {
            InitializeComponent();
        }

        private async void btn_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar monto
                if (!decimal.TryParse(txt_monto.Text, out decimal monto) || monto <= 0)
                {
                    MessageBox.Show("Ingrese un monto válido mayor a 0.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_monto.Focus();
                    return;
                }

                // Validar descripción
                if (cb_descripcion.SelectedItem == null || string.IsNullOrWhiteSpace(cb_descripcion.Text))
                {
                    MessageBox.Show("Seleccione o escriba una descripción.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cb_descripcion.Focus();
                    return;
                }

                string descripcion = cb_descripcion.SelectedItem?.ToString() ?? cb_descripcion.Text;

                // Llamada al controlador
                var controller = new CajaMovimientosController();
                bool exito = await controller.RegistrarIngresoAsync(monto, descripcion, SesionUsuario.UsuarioActual.id);

                if (exito)
                {
                    MessageBox.Show("Ingreso registrado correctamente.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos
                    txt_monto.Clear();
                    cb_descripcion.SelectedIndex = -1;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el ingreso.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}",
                                "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
