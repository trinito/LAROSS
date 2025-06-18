using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;
using Punto_de_Venta.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Punto_de_Venta
{
    public partial class form_cuenta : Form
    {
        private LoadingControl loadingOverlay;
        public form_cuenta()
        {
            InitializeComponent();
            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();
        }


        private void txt_usuario_Enter(object sender, EventArgs e)
        {
            if(txt_usuario.Text =="USUARIO")
            {
                txt_usuario.Text = "";
                txt_usuario.ForeColor = Color.Black;
            }
        }

        private void txt_usuario_Leave(object sender, EventArgs e)
        {
            if(txt_usuario.Text=="")
            {
                txt_usuario.Text = "USUARIO";
                txt_usuario.ForeColor = Color.DimGray;
            }
        }

        private void txt_contrasena_Enter(object sender, EventArgs e)
        {
            if(txt_contrasena.Text== "CONTRASEÑA")
            {
                txt_contrasena.Text = "";
                txt_contrasena.ForeColor = Color.Black;
                txt_contrasena.UseSystemPasswordChar = true;
            }

         }

        private void txt_contrasena_Leave(object sender, EventArgs e)
        {
            if(txt_contrasena.Text=="")
            {
                txt_contrasena.Text = "CONTRASEÑA";
                txt_contrasena.ForeColor = Color.DimGray;
               txt_contrasena.UseSystemPasswordChar = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (txt_contrasena.Text == "CONTRASEÑA")
                return;

            if (txt_contrasena.UseSystemPasswordChar == true)
                txt_contrasena.UseSystemPasswordChar = false;
            else
                txt_contrasena.UseSystemPasswordChar = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await Iniciar();
        }

        private async void txt_contrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                await Iniciar();
            }
        }

        private async Task Iniciar()

        {
            if (txt_usuario.Text.Contains("USUARIO") || txt_contrasena.Text.Contains("CONTRASEÑA"))
                return;

            if (string.IsNullOrEmpty(txt_contrasena.Text) && string.IsNullOrEmpty(txt_usuario.Text))
            {
                MessageBox.Show("No puedes dejar el campo vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    loadingOverlay.ShowOverlay();
                   Usuarios user = new Usuarios() { contra = txt_contrasena.Text, nombre = txt_usuario.Text };

                UsuarioController usuarioController = new UsuarioController();
                    SesionUsuario.UsuarioActual = await usuarioController.LoginAsync(user);

                if (SesionUsuario.UsuarioActual == null)
                {
                        loadingOverlay.HideOverlay();
                        MessageBox.Show("USUARIO INCORRECTO", "MENSAJE", MessageBoxButtons.OK);
                }
                else
                {
                        this.Hide();
                        Form form;

                        if (SesionUsuario.UsuarioActual.tipo == "ADMIN")
                            form = new View_Admin();
                        else
                            form = new View_Padre();

                        form.ShowDialog();
                        this.txt_usuario.Text = string.Empty;
                        this.txt_contrasena.Text = string.Empty;
                        this.BeginInvoke((Action)(() => txt_usuario.Focus()));

                        if (form is View_Padre padre && padre.LogoutSolicitado)
                        {
                            this.Show();
                        }
                        else if (form is View_Admin admin && admin.LogoutSolicitado)
                        {
                            this.Show();
                        }
                        else
                        {
                            // Cerró ventana principal, salir de la app
                            Application.Exit();
                        }
                    }

                }
                finally
                {
                    // Ocultar overlay
                     loadingOverlay.HideOverlay();
                }
            }

        }

        private async void txt_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                await Iniciar();
            }
        }
    }
}
