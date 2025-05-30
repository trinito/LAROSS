using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas
{
    public partial class View_VentasMes : Form
    {
        private readonly VentaController _ventaController;
        private readonly VistasController _vistasController;

        public View_VentasMes()
        {
            InitializeComponent();
            _ventaController = new VentaController();
            _vistasController = new VistasController();
        }

        private void View_VentasMes_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            CargarVentasPorMes(dtp_time.Value.Date);
        }

        private void dtp_time_CloseUp(object sender, EventArgs e)
        {
            CargarVentasPorMes(dtp_time.Value.Date);
        }

        private void CargarVentasPorMes(DateTime fecha)
        {
            if (CargarProductos(fecha))
            {
                MostrarTotalesDelMes(fecha);
            }
            else
            {
                MessageBox.Show(
                    "No se encontraron ventas de ese mes, favor de ingresar otra fecha...",
                    "¡Mensaje!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dgv_productos.DataSource = null;
                lbl_efectivo.Text = "$0";
                lbl_tarjeta.Text = "$0";
                lbl_total.Text = "$0";
            }
        }

        private bool CargarProductos(DateTime fecha)
        {
            try
            {
                var resultado = _vistasController.ReporteVentasMes(fecha);
                if (resultado != null && resultado.Count > 0)
                {
                    dgv_productos.DataSource = resultado;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void MostrarTotalesDelMes(DateTime fecha)
        {
            try
            {
                decimal[] totales = _ventaController.TotalesMes(fecha);

                lbl_efectivo.Text = totales[0].ToString("C", CultureInfo.CurrentCulture);
                lbl_tarjeta.Text = totales[1].ToString("C", CultureInfo.CurrentCulture);
                lbl_total.Text = totales[2].ToString("C", CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener totales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lbl_efectivo.Text = "$0";
                lbl_tarjeta.Text = "$0";
                lbl_total.Text = "$0";
            }
        }
    }
}
