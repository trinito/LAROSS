using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
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

namespace Punto_de_Venta
{
    public partial class form_cuenta : Form
    {
        public form_cuenta()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_usuario.Text.Contains("USUARIO") || txt_contrasena.Text.Contains("CONTRASEÑA"))
                return;

            if (string.IsNullOrEmpty(txt_contrasena.Text) && string.IsNullOrEmpty(txt_usuario.Text))
            {
                MessageBox.Show("No puedes dejar el campo vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Usuarios user = new Usuarios() { contra = txt_contrasena.Text, nombre=txt_usuario.Text };
                UsuarioController usuarioController = new UsuarioController(new chinahousedbEntities());
                string result = usuarioController.Login(user);

                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("USUARIO INCORRECTO", "MENSAJE", MessageBoxButtons.OK);
                }
                else
                {   
                    this.Hide();
                    Form form;
                    if (result =="Admin")
                        form = new View_Reportes();
                    else
                        form = new View_Principal();
                    form.ShowDialog();
                    this.Show();
                }
            }
        }
    }
}
