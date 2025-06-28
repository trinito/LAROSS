using Punto_de_Venta.Modelo;
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

namespace Punto_de_Venta.Vistas
{
    public partial class View_Padre : Form
    {
        List<Button> botones;
        public bool LogoutSolicitado { get; private set; } = false;
        private UserControl userControlActual = null;

        private UserControl_Ventas ventasControl = new UserControl_Ventas();
        private UserControl_Historial historialControl = new UserControl_Historial();
        private UserControl_Productos productosControl = new UserControl_Productos();
        private UserControl_Inventario inventarioControl = new UserControl_Inventario();
        private UserControl_Reportes reportesControl = new UserControl_Reportes();
        private UserControl_Configuracion configuracionControl = new UserControl_Configuracion();

        public View_Padre()
        {
            InitializeComponent();
        }

        private void View_Padre_Load(object sender, EventArgs e)
        {
            lbl_nombre.Text = string.Concat(SesionUsuario.UsuarioActual.nombre, " ", SesionUsuario.UsuarioActual.apellido);
            botones = new List<Button>();
            llenadoListaBotones();
            MostrarUserControl(ventasControl);
            limpiarBotones(btn_inicio);
            this.KeyDown += View_Padre_KeyDown;

        }


        private void btn_inicio_Click(object sender, EventArgs e)
        {
            MostrarUserControl(ventasControl);
            limpiarBotones(btn_inicio);
        }

        private async void btn_ventas_Click(object sender, EventArgs e)
        {
            MostrarUserControl(historialControl);
            await historialControl.CargarVentasEnDataGridView(); // 👈 Llamada explícita
            limpiarBotones(btn_ventas);
        }

        private async void btn_productos_Click(object sender, EventArgs e)
        {
            MostrarUserControl(productosControl);
            await productosControl.CargarProductosEnDataGridView();
            productosControl.LimpiarFormulario();
            limpiarBotones(btn_productos);
        }

        private async void btn_inventario_Click(object sender, EventArgs e)
        {
            MostrarUserControl(inventarioControl);
            await inventarioControl.CargarProductosEnDataGridView();
            limpiarBotones(btn_inventario);
        }

        private async void btn_reportes_Click(object sender, EventArgs e)
        {
            MostrarUserControl(reportesControl);
            await reportesControl.CargarDatosDashboardAsync();
            limpiarBotones(btn_reportes);
        }

        private void btn_configuracion_Click(object sender, EventArgs e)
        {
            MostrarUserControl(configuracionControl);
            limpiarBotones(btn_configuracion);
        }

        private void llenadoListaBotones()
        {
            botones.Add(btn_configuracion);
            botones.Add(btn_inicio);
            botones.Add(btn_inventario);
            botones.Add(btn_productos);
            botones.Add(btn_reportes);
            botones.Add(btn_ventas);
        }

        private void limpiarBotones(Button boton)
        {
            foreach (Button x in botones)
            {
                x.BackColor = Color.FromArgb(21, 57, 93);
            }
            boton.BackColor = Color.FromArgb(24, 44, 61);
        }

        private void MostrarUserControl(UserControl uc)
        {
            if (userControlActual != uc)
            {
                panel_principal.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                panel_principal.Controls.Add(uc);
                userControlActual = uc;  // Guarda cuál está activo
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show("¿Deseas cerrar sesión?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                LogoutSolicitado = true;
                this.Close(); // Cierra y regresa al login
            }
        }

        private void View_Padre_KeyDown(object sender, KeyEventArgs e)
        {
            if (panel_principal.Controls.Count > 0 && panel_principal.Controls[0] is UserControl_Ventas ventasControl)
            {
                ventasControl.HandleKeyDown(e);
            }
            else if (panel_principal.Controls.Count > 0 && panel_principal.Controls[0] is UserControl_Productos productsControl)
            {
                productsControl.HandleKeyDown(e);
            }
        }
    }
}
