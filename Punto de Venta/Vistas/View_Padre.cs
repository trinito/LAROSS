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
    public partial class View_Padre: Form
    {
        List<Button> botones;
        public bool LogoutSolicitado { get; private set; } = false;
        public View_Padre()
        {
            InitializeComponent();
        }

        private void View_Padre_Load(object sender, EventArgs e)
        {
            lbl_nombre.Text = string.Concat(SesionUsuario.UsuarioActual.nombre, " ", SesionUsuario.UsuarioActual.apellido);
            botones = new List<Button>();
            llenadoListaBotones();
            MostrarUserControl(new UserControl_Ventas());
            limpiarBotones(btn_inicio);

        }


        private void btn_inicio_Click(object sender, EventArgs e)
        {
            MostrarUserControl(new UserControl_Ventas());
            limpiarBotones(btn_inicio);
        }

        private void btn_ventas_Click(object sender, EventArgs e)
        {
            MostrarUserControl(new UserControl_Historial());
            limpiarBotones(btn_ventas);
        }

        private void btn_productos_Click(object sender, EventArgs e)
        {
            MostrarUserControl(new UserControl_Productos());
            limpiarBotones(btn_productos);
        }

        private void btn_inventario_Click(object sender, EventArgs e)
        {
            MostrarUserControl(new UserControl_Inventario());
            limpiarBotones(btn_inventario);
        }

        private void btn_reportes_Click(object sender, EventArgs e)
        {
            MostrarUserControl(new UserControl_Reportes());
            limpiarBotones(btn_reportes);
        }

        private void btn_configuracion_Click(object sender, EventArgs e)
        {
            MostrarUserControl(new UserControl_Configuracion());
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
            foreach(Button x in botones)
            {
                x.BackColor = Color.FromArgb(21, 57, 93);
            }    
            boton.BackColor = Color.FromArgb(24, 44, 61);
        }

        private void MostrarUserControl(UserControl uc)
        {
            this.panel_principal.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            this.panel_principal.Controls.Add(uc);
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
    }
}
