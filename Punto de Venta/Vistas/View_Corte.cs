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
using System.Windows.Forms.DataVisualization.Charting;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Corte : Form
    {
        public View_Corte()
        {
            InitializeComponent();
        }

        private void View_Corte_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            Venta(dtp_time.Value.ToString("dd/MM/yyyy"));
        }

        private void Venta(string fecha)
        {
            if(Productos(fecha))
            {
                TotalDia(fecha);
            }
        }

        private void dtp_time_CloseUp(object sender, EventArgs e)
        {
            Venta(dtp_time.Value.ToString("dd/MM/yyyy"));
        }


        private bool Productos(string fecha)
        {
            VistasController vistasController = new VistasController(new Modelo.chinahousedbEntities());
            List<ViewCorte> result = vistasController.CorteProductos(fecha);
            if(result != null)
            {
                dgv_productos.DataSource = result;
                return true;
            }

            return false;
        }

        private void TotalDia(string fecha)
        {
            VentaController ventaController = new VentaController(new chinahousedbEntities());
            decimal[] resul = ventaController.TotalesCorte(fecha);
            lbl_efectivo.Text = resul[0].ToString("C", CultureInfo.CurrentCulture);
            lbl_tarjeta.Text = resul[1].ToString("C", CultureInfo.CurrentCulture);
            lbl_total.Text = resul[2].ToString("C", CultureInfo.CurrentCulture);
        }

        private void button_imprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
