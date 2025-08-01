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

namespace Punto_de_Venta.Vistas.Venta
{
    public partial class View_Inicio_Caja: Form
    {
        public View_Inicio_Caja()
        {
            InitializeComponent();
        }

        private async void btn_registrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_monto.Text))
            {
                MessageBox.Show("Por favor ingresa un monto válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txt_monto.Text.Trim(), out decimal monto) || monto <= 0)
            {
                MessageBox.Show("El monto debe ser un número mayor a cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var controller = new CajaMovimientosController();
                bool resultado = await controller.RegistrarMovimientoInicialAsync(monto, SesionUsuario.UsuarioActual.id);

                if (resultado)
                {
                    MessageBox.Show("Monto inicial registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el monto inicial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
