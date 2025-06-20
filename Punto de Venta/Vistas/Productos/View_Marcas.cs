using Punto_de_Venta.Controlador;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Productos
{
    public partial class View_Marcas : Form
    {
        private MarcasController marcasController;
        private Timer animTimer;
        private int animPaso = 0;
        private Size originalSize;
        private Point originalLocation;
        private Button currentButton;
        private const int scale = 10;

        private int idMarcaSeleccionada = -1; // Guardar el id seleccionado

        public View_Marcas()
        {
            InitializeComponent();
            marcasController = new MarcasController();

            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += AnimacionClic;

            // Configura botones con animación e iconos
            ConfigurarBoton(btn_agregar_marca, Properties.Resources.icons8_add_25, Properties.Resources.icons8_add_25_2);
            ConfigurarBoton(btn_modificar_marca, Properties.Resources.icons8_edit_25_2, Properties.Resources.icons8_edit_25);
            ConfigurarBoton(btn_eliminar_marca, Properties.Resources.icons8_delete_25, Properties.Resources.icons8_delete_25_2);
        }

        private async void View_Marcas_Load(object sender, EventArgs e)
        {
            await CargarMarcasEnGridAsync();
            CambiarEstadoBotones(false);
        }

        private void ConfigurarBoton(Button boton, Image imgNormal, Image imgHover)
        {
            boton.MouseEnter += (s, e) => { boton.Image = imgHover; };
            boton.MouseLeave += (s, e) => { boton.Image = imgNormal; };
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

        private async Task CargarMarcasEnGridAsync()
        {
            var listaMarcas = await marcasController.ObtenerTodasLasMarcasAsync();
            dgv_marcas.DataSource = null;
            dgv_marcas.DataSource = listaMarcas;

            if (dgv_marcas.Columns.Contains("id_marca"))
                dgv_marcas.Columns["id_marca"].Visible = false;

            if (dgv_marcas.Columns.Contains("estatus"))
                dgv_marcas.Columns["estatus"].Visible = false;

            dgv_marcas.Columns["nombre"].HeaderText = "Nombre";

            dgv_marcas.ReadOnly = true;
            dgv_marcas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_marcas.MultiSelect = false;
            dgv_marcas.AllowUserToAddRows = false;
            dgv_marcas.AllowUserToDeleteRows = false;
            dgv_marcas.AllowUserToResizeRows = false;
            dgv_marcas.AllowUserToResizeColumns = false;
            dgv_marcas.RowHeadersVisible = false;

            dgv_marcas.EnableHeadersVisualStyles = false;
            dgv_marcas.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_marcas.ColumnHeadersDefaultCellStyle.BackColor;
            dgv_marcas.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv_marcas.ColumnHeadersDefaultCellStyle.ForeColor;

            dgv_marcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_marcas.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_marcas.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
        }

        private void dgv_marcas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            CambiarEstadoBotones(true);

            var fila = dgv_marcas.Rows[e.RowIndex];
            idMarcaSeleccionada = Convert.ToInt32(fila.Cells["id_marca"].Value);
            txt_nombre_marca.Text = fila.Cells["nombre"].Value.ToString();
            txt_nombre_marca.Focus();
        }

        private async void btn_agregar_marca_Click(object sender, EventArgs e)
        {
            await Agregar();
        }

        public async Task Agregar()
        {
            AnimarBoton(btn_agregar_marca);

            string nombreMarca = txt_nombre_marca.Text.Trim();

            if (string.IsNullOrEmpty(nombreMarca))
            {
                MessageBox.Show("Por favor ingresa un nombre para la marca.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int nuevoId = await marcasController.InsertarMarcaAsync(nombreMarca);

                MessageBox.Show($"Marca '{nombreMarca}' agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarMarcasEnGridAsync();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_modificar_marca_Click(object sender, EventArgs e)
        {
            await Modificar();
        }
        public async Task Modificar()
        {
            AnimarBoton(btn_modificar_marca);

            if (idMarcaSeleccionada == -1)
            {
                MessageBox.Show("No hay ninguna marca seleccionada para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_nombre_marca.Text))
            {
                MessageBox.Show("El nombre de la marca no puede estar vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await marcasController.ModificarMarcaAsync(idMarcaSeleccionada, txt_nombre_marca.Text.Trim());

                MessageBox.Show("Marca modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await CargarMarcasEnGridAsync();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_eliminar_marca_Click(object sender, EventArgs e)
        {
            await Eliminar();
        }
        
        public async Task Eliminar()
        {
            AnimarBoton(btn_eliminar_marca);

            if (idMarcaSeleccionada == -1)
            {
                MessageBox.Show("No hay ninguna marca seleccionada para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("¿Estás seguro que deseas eliminar esta marca?",
                                                "Confirmar eliminación",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    await marcasController.EliminarMarcaAsync(idMarcaSeleccionada);

                    MessageBox.Show("Marca eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await CargarMarcasEnGridAsync();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void CambiarEstadoBotones(bool editarEliminarVisible)
        {
            btn_modificar_marca.Visible = editarEliminarVisible;
            btn_eliminar_marca.Visible = editarEliminarVisible;
            btn_cancelar.Visible = editarEliminarVisible;

            btn_agregar_marca.Visible = !editarEliminarVisible;
            btn_salir.Visible = !editarEliminarVisible;
        }

        private void LimpiarFormulario()
        {
            txt_nombre_marca.Clear();
            idMarcaSeleccionada = -1;
            CambiarEstadoBotones(false);
            txt_nombre_marca.Focus();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void View_Marcas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: 
                    if (btn_cancelar.Visible)
                    {
                        LimpiarFormulario();
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
            }
        }

   
        private async void txt_nombre_marca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (btn_agregar_marca.Visible)
                    await Agregar();
                else
                    await Modificar();

                e.Handled = true;  // Evita que se "suene" el beep al presionar Enter
            }
        }
    }
}
