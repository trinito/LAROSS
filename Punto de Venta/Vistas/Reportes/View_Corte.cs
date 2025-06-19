using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Corte : Form
    {
        private bool canPrint = false;

        public View_Corte()
        {
            InitializeComponent();
        }

        private void View_Corte_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            LoadVentasForDate(dtp_time.Value.Date);
        }

        private void LoadVentasForDate(DateTime fecha)
        {
            if (LoadProductos(fecha))
            {
                LoadTotalesDelDia(fecha);
            }
            else
            {
                MessageBox.Show("No se encontraron ventas de ese día, favor de ingresar otra fecha...", "¡Mensaje!");
                dgv_productos.DataSource = null;
                lbl_efectivo.Text = lbl_tarjeta.Text = lbl_total.Text = "$0.00";
                canPrint = false;
            }
        }

        private void dtp_time_CloseUp(object sender, EventArgs e)
        {
            LoadVentasForDate(dtp_time.Value.Date);
        }

        private bool LoadProductos(DateTime fecha)
        {
            try
            {
                var vistasController = new VistasController();
                var resultado = vistasController.CorteProductos(fecha);
                if (resultado?.Any() == true)
                {
                    dgv_productos.DataSource = resultado;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error");
            }
            return false;
        }

        private void LoadTotalesDelDia(DateTime fecha)
        {
            try
            {
                var ventaController = new VentaController();
                decimal[] totales = ventaController.TotalesCorte(fecha);

                lbl_efectivo.Text = totales[0].ToString("C", CultureInfo.CurrentCulture);
                lbl_tarjeta.Text = totales[1].ToString("C", CultureInfo.CurrentCulture);
                lbl_total.Text = totales[2].ToString("C", CultureInfo.CurrentCulture);

                canPrint = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar totales del día: {ex.Message}", "Error");
                canPrint = false;
            }
        }

        private void button_imprimir_Click(object sender, EventArgs e)
        {
            if (!canPrint)
                return;

            if (!(dgv_productos.DataSource is List<ViewCorte> productos))
                return;

            try
            {
                var ticket = new ImprimirTickets();

                ticket.TextoCentro("VENTAS DEL DÍA");
                ticket.TextoExtremos(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("hh:mm tt"));
                ticket.TextoIzquierda(" ");
                ticket.EncabezadoCorte();
                ticket.lineasGuio();

                foreach (var item in productos)
                {
                    ticket.AgregaArticulo2(item.Nombre, item.Cantidad, item.Total);
                }

                ticket.lineasGuio();

                // Parseando los valores eliminando símbolos y respetando la cultura
                decimal efectivo = Decimal.Parse(lbl_efectivo.Text, NumberStyles.Currency, CultureInfo.CurrentCulture);
                decimal tarjeta = Decimal.Parse(lbl_tarjeta.Text, NumberStyles.Currency, CultureInfo.CurrentCulture);
                decimal total = Decimal.Parse(lbl_total.Text, NumberStyles.Currency, CultureInfo.CurrentCulture);

                ticket.AgregarTotales("            EFECTIVO:  ", efectivo);
                ticket.AgregarTotales("             TARJETA:  ", tarjeta);
                ticket.AgregarTotales("         TOTAL VENTA: ", total);

                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda(" ");
                ticket.CortaTicket();
                ticket.ImprimirTicket("ZJ-58");

                canPrint = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir el ticket: {ex.Message}", "Error");
            }
        }
    }
}
