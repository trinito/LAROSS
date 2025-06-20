using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Productos
{
    public partial class View_Colores : Form
    {
        private ColorController colorController;
        private Timer animTimer;
        private int animPaso = 0;
        private Size originalSize;
        private Point originalLocation;
        private Button currentButton;
        private const int scale = 10;

        private int idColorSeleccionado = -1;

        public View_Colores()
        {
            InitializeComponent();
            colorController = new ColorController();

            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += AnimacionClic;

            ConfigurarBoton(btn_agregar_color, Properties.Resources.icons8_add_25, Properties.Resources.icons8_add_25_2);
            ConfigurarBoton(btn_modificar_color, Properties.Resources.icons8_edit_25_2, Properties.Resources.icons8_edit_25);
            ConfigurarBoton(btn_eliminar_color, Properties.Resources.icons8_delete_25, Properties.Resources.icons8_delete_25_2);
        }

        private async void View_Colores_Load(object sender, EventArgs e)
        {
            await CargarColoresEnGridAsync();
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

        private async Task CargarColoresEnGridAsync()
        {
            var lista = await colorController.ObtenerTodosLosColoresAsync();
            dgv_colores.DataSource = null;
            dgv_colores.DataSource = lista;

            if (dgv_colores.Columns.Contains("id_color"))
                dgv_colores.Columns["id_color"].Visible = false;

            if (dgv_colores.Columns.Contains("estatus"))
                dgv_colores.Columns["estatus"].Visible = false;

            dgv_colores.Columns["nombre"].HeaderText = "Nombre";

            dgv_colores.ReadOnly = true;
            dgv_colores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_colores.MultiSelect = false;
            dgv_colores.AllowUserToAddRows = false;
            dgv_colores.AllowUserToDeleteRows = false;
            dgv_colores.AllowUserToResizeRows = false;
            dgv_colores.AllowUserToResizeColumns = false;
            dgv_colores.RowHeadersVisible = false;

            dgv_colores.EnableHeadersVisualStyles = false;
            dgv_colores.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_colores.ColumnHeadersDefaultCellStyle.BackColor;
            dgv_colores.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv_colores.ColumnHeadersDefaultCellStyle.ForeColor;

            dgv_colores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_colores.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_colores.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
        }

        private void dgv_colores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            CambiarEstadoBotones(true);

            var fila = dgv_colores.Rows[e.RowIndex];
            idColorSeleccionado = Convert.ToInt32(fila.Cells["id_color"].Value);
            txt_nombre_color.Text = fila.Cells["nombre"].Value.ToString();
            txt_nombre_color.Focus();
        }

        private async void btn_agregar_color_Click(object sender, EventArgs e)
        {
            await Agregar();
        }
        public async Task Agregar()
        {
            AnimarBoton(btn_agregar_color);

            string nombre = txt_nombre_color.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor ingresa un nombre de color.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await colorController.InsertarColorAsync(nombre);
                MessageBox.Show("Color agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarColoresEnGridAsync();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_modificar_color_Click(object sender, EventArgs e)
        {
            await Modificar();
        }

        public async Task Modificar()
        {
            AnimarBoton(btn_modificar_color);

            if (idColorSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un color para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txt_nombre_color.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await colorController.ModificarColorAsync(idColorSeleccionado, nombre);
                MessageBox.Show("Color modificado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarColoresEnGridAsync();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_eliminar_color_Click(object sender, EventArgs e)
        {
            AnimarBoton(btn_eliminar_color);

            if (idColorSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un color para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar este color?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await colorController.EliminarColorAsync(idColorSeleccionado);
                    MessageBox.Show("Color eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarColoresEnGridAsync();
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
            txt_nombre_color.Clear();
            idColorSeleccionado = -1;
            CambiarEstadoBotones(false);
            txt_nombre_color.Focus();
        }

        private void CambiarEstadoBotones(bool edicion)
        {
            btn_modificar_color.Visible = edicion;
            btn_eliminar_color.Visible = edicion;
            btn_agregar_color.Visible = !edicion;

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

        private void View_Colores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (btn_cancelar.Visible)
                    LimpiarFormulario();
                else
                    this.Close();
            }
        }

        private async void txt_nombre_color_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter
            {
                if (btn_agregar_color.Visible)
                    await Agregar();
                else
                    await Modificar();

                e.Handled = true;
            }
        }
    }
}
