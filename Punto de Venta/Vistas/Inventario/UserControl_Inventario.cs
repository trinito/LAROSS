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
using Punto_de_Venta.Vistas.Inventario;
using Punto_de_Venta.Controles;

namespace Punto_de_Venta.Vistas
{
    public partial class UserControl_Inventario : UserControl
    {
        ProductosController productosController;
        private readonly BindingSource bindingSource;
        private List<ProductoVentaDTO> productos;
        private ProductoVentaDTO productoSelect;
        private LoadingControl loadingOverlay;

        public UserControl_Inventario()
        {
            InitializeComponent();

            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();

            productosController = new ProductosController();
            bindingSource = new BindingSource();
            productos = new List<ProductoVentaDTO>();
            productoSelect = new ProductoVentaDTO();

            GridViewHelper();

        }

        private void button_quitar_Click(object sender, EventArgs e)
        {

        }

        private async void UserControl_Inventario_Load(object sender, EventArgs e)
        {
            try
            {
                txt_codigo.Focus();
                await CargarProductosEnDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public async Task CargarProductosEnDataGridView()
        {
            productos = await productosController.ObtenerProductosParaVentaAsync();
            bindingSource.DataSource = productos;
            int totalStock = await productosController.ObtenerTotalStockAsync();
            lbl_stock.Text = totalStock.ToString();
        }

        private void GridViewHelper()
        {
            dgv_productos.AutoGenerateColumns = false;
            dgv_productos.DataSource = bindingSource;

            // Crear y agregar columnas manualmente
            dgv_productos.Columns.Clear();

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoBarras",          // <-- Name agregado
                DataPropertyName = "CodigoBarras",
                HeaderText = "Código",
                Width = 80
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 180
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Marca",
                DataPropertyName = "Marca",
                HeaderText = "Marca",
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Color",
                DataPropertyName = "Color",
                HeaderText = "Color",
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Talla",
                DataPropertyName = "Talla",
                HeaderText = "Talla",
                Width = 60
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sexo",
                DataPropertyName = "Sexo",
                HeaderText = "Sexo",
                Width = 80
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categoria",
                DataPropertyName = "Categoria",
                HeaderText = "Categoría",
                Width = 100
            });


            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                DataPropertyName = "Stock",
                HeaderText = "Stock",
                Width = 60
            });


            dgv_productos.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_productos.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);

            if (dgv_productos.Columns.Contains("CodigoBarras"))
                dgv_productos.Columns["CodigoBarras"].Frozen = true;

            dgv_productos.DoubleBuffered(true);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void lbl_stock_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filtro = txt_producto.Text.Trim().ToUpper();

            var filtrados = string.IsNullOrEmpty(filtro)
                ? productos
                : productos.Where(p =>
                      p.Nombre.ToUpper().Contains(filtro) ||
                      p.CodigoBarras.ToUpper().Contains(filtro)
                  ).ToList();

            bindingSource.DataSource = filtrados;
        }

        private async void txt_codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && !string.IsNullOrEmpty(txt_codigo.Text))
            {
                try
                {
                    string filtro = txt_codigo.Text.Trim().ToUpper();


                    var filtrados = string.IsNullOrEmpty(filtro)
                        ? productos
                        : productos.Where(p => p.CodigoBarras.ToUpper() == filtro).ToList();

                    bindingSource.DataSource = filtrados;
                    if (filtrados.Count > 0)
                    {
                        dgv_productos.CurrentCell = dgv_productos.Rows[0].Cells[0]; // Seleccionar primera fila
                        await Seleccionar();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún producto con ese código.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
                finally
                {

                }
            }
        }

        private async void btn_agregar_Click(object sender, EventArgs e)
        {
            await Seleccionar();
        }

        private async Task Seleccionar()
        {
            if (dgv_productos.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            productoSelect = dgv_productos.CurrentRow.DataBoundItem as ProductoVentaDTO;

            if (productoSelect == null)
            {
                MessageBox.Show("No se pudo obtener el producto seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var form = new View_Stock(productoSelect.Nombre, productoSelect.CodigoBarras, productoSelect.Stock))
            {
                txt_codigo.Text = string.Empty;
                form.ShowDialog();

                if (form.result)
                {
                    await CargarProductosEnDataGridView();
                }
            }
        }


        private async void dgv_productos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            await Seleccionar();
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                btn_imprimir.Enabled = false;

                if (dgv_productos.CurrentRow != null)
                {
                    var productoSelect = dgv_productos.CurrentRow.DataBoundItem as ProductoVentaDTO;

                    if (productoSelect != null)
                    {
                        if (productoSelect.Stock <= 0)
                        {
                            MessageBox.Show("El producto no tiene stock disponible para imprimir el código de barra.", "Sin stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        using (var form = new View_Tickets(productoSelect.Nombre, productoSelect.CodigoBarras))
                        {
                            form.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo obtener el producto seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un producto para imprimir el ticket.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                btn_imprimir.Enabled = true;
            }
        }

        private async void txt_codigo_original_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && !string.IsNullOrEmpty(txt_codigo_original.Text))
            {
                try
                {
                    string filtro = txt_codigo_original.Text.Trim().ToUpper();

                    var filtrados = string.IsNullOrEmpty(filtro)
                        ? productos
                        : productos.Where(p => p.CodigoBarrasOriginal.ToUpper() == filtro).ToList();


                    bindingSource.DataSource = filtrados;

                    if (filtrados.Count > 0)
                    {
                        dgv_productos.CurrentCell = dgv_productos.Rows[0].Cells[0]; // Seleccionar primera fila
                        await Seleccionar();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún producto con ese código.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
                finally
                {

                }
            }
        }

        private void txt_codigo_original_TextChanged(object sender, EventArgs e)
        {
            string filtro = txt_codigo_original.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                bindingSource.DataSource = productos; // Muestra todos los productos
            }
        }

        private void txt_codigo_TextChanged(object sender, EventArgs e)
        {
            string filtro = txt_codigo.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                bindingSource.DataSource = productos; // Muestra todos los productos
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
