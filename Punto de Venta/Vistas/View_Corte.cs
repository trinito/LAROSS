using Punto_de_Venta.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Venta(dtp_time.Value.ToString("dd/MM/yyyy"));
        }

        private void Venta(string fecha)
        {
            Productos(fecha);
            Efectivo(fecha);
            Tarjeta(fecha);
            TotalDia(fecha);
        }

        private void dtp_time_CloseUp(object sender, EventArgs e)
        {
            Venta(dtp_time.Value.ToString("dd/MM/yyyy"));
        }


        private void Productos(string fecha)
        {
            DetalleVentaController detalleVentacontroler = new DetalleVentaController(new Modelo.chinahousedbEntities());
        }

        private void Efectivo(string fecha)
        {

        }

        private void Tarjeta(string fecha)
        {

        }

        private void TotalDia(string fecha)
        {

        }
    }
}
