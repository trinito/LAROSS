using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Corte : Form
    {
        private bool isImprimir = false;
        public View_Corte()
        {
            InitializeComponent();
        }

        private void View_Corte_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            Venta(dtp_time.Value.Date);
        }

        private void Venta(DateTime fecha)
        {
            if(Productos(fecha))
            {
                TotalDia(fecha);
            }
            else
            {
                string message = "No se encontraron ventas de ese día, favor de ingresar otra fecha...";
                string title = "¡Mensaje!";
                MessageBox.Show(message, title);
                dgv_productos.DataSource = null;
                lbl_efectivo.Text = "$0";
                lbl_tarjeta.Text = "$0";
                lbl_total.Text = "$0";
                isImprimir = false;
            }
        }

        private void dtp_time_CloseUp(object sender, EventArgs e)
        {
            Venta(dtp_time.Value.Date);
        }


        private bool Productos(DateTime fecha)
        {
            try
            {
                VistasController vistasController = new VistasController(new Modelo.chinahousedbEntities());
                List<ViewCorte> result = vistasController.CorteProductos(fecha);
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

        private void TotalDia(DateTime fecha)
        {
            try
            {
                VentaController ventaController = new VentaController(new chinahousedbEntities());
                decimal[] resul = ventaController.TotalesCorte(fecha);
                lbl_efectivo.Text = resul[0].ToString("C", CultureInfo.CurrentCulture);
                lbl_tarjeta.Text = resul[1].ToString("C", CultureInfo.CurrentCulture);
                lbl_total.Text = resul[2].ToString("C", CultureInfo.CurrentCulture);
                isImprimir = true;
            }
            catch (Exception ex)
            {

            }
          
        }

        private void button_imprimir_Click(object sender, EventArgs e)
        {
            if (!isImprimir)
                return;

            List<ViewCorte> result;
            ImprimirTickets ticket = new ImprimirTickets();
            try
            {
                if (dgv_productos.DataSource != null)
                    result = (List<ViewCorte>)dgv_productos.DataSource;
                else
                    return;
                string efectivo = lbl_efectivo.Text;
                efectivo = efectivo.Replace("$", string.Empty);
                string tarjeta = lbl_tarjeta.Text;
                tarjeta = tarjeta.Replace("$", string.Empty);
                string total = lbl_total.Text;
                total = total.Replace("$", string.Empty);
                ticket.TextoCentro("VENTAS DEL DIA");
                ticket.TextoExtremos(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("hh:mm tt"));
                ticket.TextoIzquierda(" ");
                ticket.EncabezadoCorte();
                ticket.lineasGuio();
                foreach (var x in result)
                {
                    ticket.AgregaArticulo2(x.Nombre, x.Cantidad, x.Total);
                }
                ticket.lineasGuio();
                ticket.AgregarTotales("            EFECTIVO:  ", Convert.ToDecimal(efectivo));
                ticket.AgregarTotales("             TARJETA:  ", Convert.ToDecimal(tarjeta));
                ticket.AgregarTotales("         TOTAL VENTA: ", Convert.ToDecimal(total));
                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda(" ");
                ticket.CortaTicket();
                ticket.ImprimirTicket("ZJ-5890");
                isImprimir = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
