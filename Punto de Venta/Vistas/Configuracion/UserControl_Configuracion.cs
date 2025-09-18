using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Controles;

namespace Punto_de_Venta.Vistas
{
    public partial class UserControl_Configuracion : UserControl
    {
        ConfiguracionController configuracionController;
        private readonly BindingSource bindingSource;
        private LoadingControl loadingOverlay;

        public UserControl_Configuracion()
        {
            InitializeComponent();
            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();
            configuracionController = new ConfiguracionController();
            bindingSource = new BindingSource();
            GridViewHelper();
        }


        private void GridViewHelper()
        {
            dgv_usuarios.AutoGenerateColumns = false;
            dgv_usuarios.DataSource = bindingSource;

            // Crear y agregar columnas manualmente
            dgv_usuarios.Columns.Clear();

            dgv_usuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",          // <-- Name agregado
                DataPropertyName = "Id",
                HeaderText = "ID",
                FillWeight = 10
            });

            dgv_usuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombres",
                DataPropertyName = "Nombres",
                HeaderText = "Nombres",
                FillWeight = 20
            });

            dgv_usuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                DataPropertyName = "Usuario",
                HeaderText = "Usuario",
                FillWeight = 20
            });

            dgv_usuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Permisos",
                DataPropertyName = "Permisos",
                HeaderText = "Permisos",
                FillWeight = 40
            });

            dgv_usuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estatus",
                DataPropertyName = "Estatus",
                HeaderText = "Estatus",
                FillWeight = 10
            });

            dgv_usuarios.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_usuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);

            if (dgv_usuarios.Columns.Contains("Id"))
                dgv_usuarios.Columns["Id"].Frozen = true;

            dgv_usuarios.DoubleBuffered(true);
        }

        private async void UserControl_Configuracion_Load(object sender, EventArgs e)
        {
            loadingOverlay.ShowOverlay();
            await CargarUsuariosEnDataGridView();
            loadingOverlay.HideOverlay();
            btn_modificar.Enabled = false;
            btn_eliminar.Enabled = false;
        }

        public async Task CargarUsuariosEnDataGridView()
        {
            var usuarios = await configuracionController.ObtenerTodosLosUsuariosAsync();
            dgv_usuarios.DataSource = usuarios.Where(u => u.Estatus == "Activo").ToList();
            bindingSource.DataSource = usuarios;
        }



        // Método para limpiar el formulario (también lo puedes usar en btn_cancelar_Click)
        private void LimpiarFormulario()
        {
            txt_nombre.Clear();
            txt_apellidos.Clear();
            txt_usuario.Clear();
            txt_contraseña.Clear();
            txt_confirmar_contraseña.Clear();

            check_ventas.Checked = false;
            check_historial.Checked = false;
            check_inventario.Checked = false;
            check_productos.Checked = false;
            check_dashboard.Checked = false;
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            txt_contraseña.UseSystemPasswordChar = !txt_contraseña.UseSystemPasswordChar;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txt_confirmar_contraseña.UseSystemPasswordChar = !txt_confirmar_contraseña.UseSystemPasswordChar;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            btn_agregar.Enabled = true;
            btn_modificar.Enabled = false;
            btn_eliminar.Enabled = false;
            txt_idUsuario.Text = string.Empty;
        }

        // Evento para doble clic en el DataGridView

        private async void btn_agregar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario(out string permisos))
                return;


            try
            {
                var confirmAgregar = MessageBox.Show(
                    "¿Desea agregar este nuevo usuario?",
                    "Confirmar agregación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmAgregar != DialogResult.Yes)
                    return; // sale si el usuario elige "No"

                bool agregado = await configuracionController.AgregarUsuarioAsync(
                    txt_nombre.Text.Trim(),
                    txt_apellidos.Text.Trim(),
                    txt_usuario.Text.Trim(),
                    txt_contraseña.Text.Trim(),
                    permisos
                );

                if (agregado)
                {
                    MessageBox.Show("Usuario agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarUsuariosEnDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btn_modificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_idUsuario.Text))
                return;

            if (!ValidarFormulario(out string permisos))
                return;

            int idUsuario = Convert.ToInt32(txt_idUsuario.Text);

            try
            {

                var confirmModificar = MessageBox.Show(
                    "¿Está seguro de que desea guardar los cambios de este usuario?",
                    "Confirmar modificación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmModificar != DialogResult.Yes)
                    return; // sale si el usuario elige "No"


                bool modificado = await configuracionController.ModificarUsuarioAsync(
                    idUsuario,
                    txt_nombre.Text.Trim(),
                    txt_apellidos.Text.Trim(),
                    txt_usuario.Text.Trim(),
                    txt_contraseña.Text.Trim(),
                    permisos
                );

                if (modificado)
                {
                    MessageBox.Show("Usuario modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    btn_agregar.Enabled = true;
                    btn_modificar.Enabled = false;
                    btn_eliminar.Enabled = false;
                    await CargarUsuariosEnDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_idUsuario.Text))
                return;

            int idUsuario = Convert.ToInt32(txt_idUsuario.Text);

            // Confirmar acción con el usuario
            var confirm = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?",
                                          "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                // Llamar al controller para eliminar (lógica puede ser física o lógica)
                bool eliminado = await configuracionController.EliminarUsuarioAsync(idUsuario);

                if (eliminado)
                {
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar formulario y botones
                    LimpiarFormulario();
                    btn_agregar.Enabled = true;
                    btn_modificar.Enabled = false;
                    btn_eliminar.Enabled = false;


                    // Recargar DataGridView
                    await CargarUsuariosEnDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void dgv_usuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                int idUsuario = Convert.ToInt32(dgv_usuarios.Rows[e.RowIndex].Cells["id"].Value);
                var usuario = await configuracionController.ObtenerUsuarioPorIdAsync(idUsuario);
                if (usuario == null)
                {
                    MessageBox.Show("El usuario está inactivo o no existe.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CargarUsuarioEnFormulario(usuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidarFormulario(out string permisos)
        {
            permisos = null;

            if (string.IsNullOrWhiteSpace(txt_nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_apellidos.Text) ||
                string.IsNullOrWhiteSpace(txt_usuario.Text) ||
                string.IsNullOrWhiteSpace(txt_contraseña.Text))
            {
                MessageBox.Show("Debe llenar todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txt_contraseña.Text != txt_confirmar_contraseña.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var permisosList = new List<string>();
            if (check_ventas.Checked) permisosList.Add("Ventas");
            if (check_historial.Checked) permisosList.Add("Historial");
            if (check_inventario.Checked) permisosList.Add("Inventario");
            if (check_productos.Checked) permisosList.Add("Productos");
            if (check_dashboard.Checked) permisosList.Add("Dashboard");

            if (!permisosList.Any())
            {
                MessageBox.Show("Debe seleccionar al menos un permiso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            permisos = string.Join(", ", permisosList);
            return true;
        }

        private void CargarUsuarioEnFormulario(Usuarios usuario)
        {
            if (usuario == null) return;

            txt_nombre.Text = usuario.nombre;
            txt_apellidos.Text = usuario.apellido;
            txt_usuario.Text = usuario.username;
            txt_contraseña.Text = usuario.contra;
            txt_confirmar_contraseña.Text = usuario.contra;

            // Limpiar y asignar CheckBoxes
            check_ventas.Checked = false;
            check_historial.Checked = false;
            check_inventario.Checked = false;
            check_productos.Checked = false;
            check_dashboard.Checked = false;

            if (!string.IsNullOrEmpty(usuario.permisos))
            {
                var permisos = usuario.permisos.Split(',').Select(p => p.Trim()).ToList();
                check_ventas.Checked = permisos.Contains("Ventas");
                check_historial.Checked = permisos.Contains("Historial");
                check_inventario.Checked = permisos.Contains("Inventario");
                check_productos.Checked = permisos.Contains("Productos");
                check_dashboard.Checked = permisos.Contains("Dashboard");
            }

            txt_idUsuario.Text = usuario.id.ToString();

            btn_agregar.Enabled = false;
            btn_modificar.Enabled = true;
            btn_eliminar.Enabled = true;
        }

        private void btn_modificar_EnabledChanged(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (!btn.Enabled)
            {
                btn.BackColor = Color.LightGray;
                btn.ForeColor = Color.DarkGray;
                btn.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                btn.BackColor = Color.FromArgb(39, 157, 210); // o tu color original
                btn.ForeColor = Color.Black;
                btn.FlatStyle = FlatStyle.Popup;
            }
        }

        private void btn_eliminar_EnabledChanged(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (!btn.Enabled)
            {
                btn.BackColor = Color.LightGray;
                btn.ForeColor = Color.DarkGray;
                btn.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                btn.BackColor = Color.FromArgb(229, 88, 102); // o tu color original
                btn.ForeColor = Color.Black;
                btn.FlatStyle = FlatStyle.Popup;
            }
        }

        private void btn_agregar_EnabledChanged(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (!btn.Enabled)
            {
                btn.BackColor = Color.LightGray;
                btn.ForeColor = Color.DarkGray;
                btn.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                btn.BackColor = Color.FromArgb(53, 189, 129); // o tu color original
                btn.ForeColor = Color.Black;
                btn.FlatStyle = FlatStyle.Popup;
            }
        }
    }
}
