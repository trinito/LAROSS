using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Productos
{
    public partial class View_Sexos : Form
    {
        private SexoController sexoController;
        private Timer animTimer;
        private int animPaso = 0;
        private Size originalSize;
        private Point originalLocation;
        private Button currentButton;
        private const int scale = 10;

        private int idSexoSeleccionado = -1;

        public View_Sexos()
        {
            InitializeComponent();
            sexoController = new SexoController();

            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += AnimacionClic;

            ConfigurarBoton(btn_agregar_sexo, Properties.Resources.icons8_add_25, Properties.Resources.icons8_add_25_2);
            ConfigurarBoton(btn_modificar_sexo, Properties.Resources.icons8_edit_25_2, Properties.Resources.icons8_edit_25);
            ConfigurarBoton(btn_eliminar_sexo, Properties.Resources.icons8_delete_25, Properties.Resources.icons8_delete_25_2);
        }

        private async void View_Sexos_Load(object sender, EventArgs e)
        {
            await CargarSexosEnGridAsync();
            CambiarEstadoBotones(false);
        }

        private void ConfigurarBoton(Button boton, Image imgNormal, Image imgHover)
        {
            boton.MouseEnter += (s, e) => boton.Image = imgHover;
            boton.MouseLeave += (s, e) => boton.Image = imgNormal;
        }

        private void AnimacionClic(object sender, EventArgs e)
        {
            if (currentButton == null) return;

            if (animPaso == 0)
            {
                currentButton.Size = new Size(originalSize.Width - scale, originalSize.Height - scale);
                currentButton.Location = new Point(originalLocation.X + scale / 2, originalLocation.Y + scale / 2);
                animPaso++;
            }
            else
            {
                currentButton.Size = originalSize;
                currentButton.Location = originalLocation;
                animTimer.Stop();
                currentButton = null;
            }
        }

        private void AnimarBoton(Button boton)
        {
            if (animTimer.Enabled) return;

            currentButton = boton;
            originalSize = boton.Size;
            originalLocation = boton.Location;
            animPaso = 0;
            animTimer.Start();
        }

        private async Task CargarSexosEnGridAsync()
        {
            var lista = await sexoController.ObtenerTodosLosSexosAsync();
            dgv_sexos.DataSource = null;
            dgv_sexos.DataSource = lista;

            if (dgv_sexos.Columns.Contains("id_sexo"))
                dgv_sexos.Columns["id_sexo"].Visible = false;

            if (dgv_sexos.Columns.Contains("estatus"))
                dgv_sexos.Columns["estatus"].Visible = false;

            dgv_sexos.Columns["nombre"].HeaderText = "Nombre";

            dgv_sexos.ReadOnly = true;
            dgv_sexos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_sexos.MultiSelect = false;
            dgv_sexos.AllowUserToAddRows = false;
            dgv_sexos.AllowUserToDeleteRows = false;
            dgv_sexos.AllowUserToResizeRows = false;
            dgv_sexos.AllowUserToResizeColumns = false;
            dgv_sexos.RowHeadersVisible = false;

            dgv_sexos.EnableHeadersVisualStyles = false;
            dgv_sexos.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_sexos.ColumnHeadersDefaultCellStyle.BackColor;
            dgv_sexos.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv_sexos.ColumnHeadersDefaultCellStyle.ForeColor;

            dgv_sexos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_sexos.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_sexos.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
        }

        private void dgv_sexos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            CambiarEstadoBotones(true);

            var fila = dgv_sexos.Rows[e.RowIndex];
            idSexoSeleccionado = Convert.ToInt32(fila.Cells["id_sexo"].Value);
            txt_nombre_sexo.Text = fila.Cells["nombre"].Value.ToString();
            txt_nombre_sexo.Focus();
        }

        private async void btn_agregar_sexo_Click(object sender, EventArgs e)
        {
            await Agregar();
        }
        public async Task Agregar()
        {
            AnimarBoton(btn_agregar_sexo);

            string nombre = txt_nombre_sexo.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor ingresa un nombre de género.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await sexoController.InsertarSexoAsync(nombre);
                MessageBox.Show("Género agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarSexosEnGridAsync();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_modificar_sexo_Click(object sender, EventArgs e)
        {
            await Modificar();
        }

        public async Task Modificar()
        {
            AnimarBoton(btn_modificar_sexo);

            if (idSexoSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un género para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txt_nombre_sexo.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await sexoController.ModificarSexoAsync(idSexoSeleccionado, nombre);
                MessageBox.Show("Género modificado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarSexosEnGridAsync();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_eliminar_sexo_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_eliminar_sexo);

            if (idSexoSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un género para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar este género?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await sexoController.EliminarSexoAsync(idSexoSeleccionado);
                    MessageBox.Show("Género eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarSexosEnGridAsync();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimpiarFormulario()
        {
            txt_nombre_sexo.Clear();
            idSexoSeleccionado = -1;
            CambiarEstadoBotones(false);
            txt_nombre_sexo.Focus();
        }

        private void CambiarEstadoBotones(bool edicion)
        {
            btn_modificar_sexo.Visible = edicion;
            btn_eliminar_sexo.Visible = edicion;
            btn_agregar_sexo.Visible = !edicion;

            btn_cancelar.Visible = edicion;
            btn_salir.Visible = !edicion;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void View_Sexos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (btn_cancelar.Visible)
                    LimpiarFormulario();
                else
                    this.Close();
            }
        }

        private async void txt_nombre_sexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter
            {
                if (btn_agregar_sexo.Visible)
                    await Agregar();
                else
                    await Modificar();

                e.Handled = true;
            }
        }
    }
}
