using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas
{
    public partial class View_VentasMes : Form
    {
        public View_VentasMes()
        {
            InitializeComponent();
        }

        private void VentasMes_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            Venta(dtp_time.Value.Date);
        }

        private void Venta(DateTime fecha)
        {
            if (Productos(fecha))
            {
                TotalDia(fecha);
            }
            else
            {
                string message = "No se encontraron ventas de ese mes, favor de ingresar otra fecha...";
                string title = "¡Mensaje!";
                MessageBox.Show(message, title);
                dgv_productos.DataSource = null;
                lbl_efectivo.Text = "$0";
                lbl_tarjeta.Text = "$0";
                lbl_total.Text = "$0";
            }
        }

        private void TotalDia(DateTime fecha)
        {
            try
            {
                VentaController ventaController = new VentaController(new chinahousedbEntities());
                decimal[] resul = ventaController.TotalesMes(fecha);
                lbl_efectivo.Text = resul[0].ToString("C", CultureInfo.CurrentCulture);
                lbl_tarjeta.Text = resul[1].ToString("C", CultureInfo.CurrentCulture);
                lbl_total.Text = resul[2].ToString("C", CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {

            }

        }

        private bool Productos(DateTime fecha)
        {
            try
            {
                VistasController vistasController = new VistasController(new Modelo.chinahousedbEntities());
                List<ViewCorte> result = vistasController.ReporteVentasMes(fecha);
                if (result != null)
                {
                    dgv_productos.DataSource = result;
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        private void dtp_time_CloseUp(object sender, EventArgs e)
        {
            Venta(dtp_time.Value.Date);
        }
    }
}
