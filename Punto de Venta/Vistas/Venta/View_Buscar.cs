using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas
{
    public partial class View_Buscar : Form
    {
        public ProductoVentaDTO productoSelect;
        private List<ProductoVentaDTO> productos;
        private readonly ProductosController productosController;
        private readonly BindingSource bindingSource;
        private LoadingControl loadingOverlay;
        string rutaBaseImagenes = @"C:\LaRoss\imagenes_productos\";

        public View_Buscar()
        {
            InitializeComponent();

            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();

            productos = new List<ProductoVentaDTO>();
            productosController = new ProductosController();
            bindingSource = new BindingSource();

            dgv_productos.AutoGenerateColumns = false;
            dgv_productos.DataSource = bindingSource;

            // Crear y agregar columnas manualmente
            dgv_productos.Columns.Clear();

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodigoBarras",
                HeaderText = "Código",
                Width = 120
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 180
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Marca",
                HeaderText = "Marca",
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Color",
                HeaderText = "Color",
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Talla",
                HeaderText = "Talla",
                Width = 80
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Sexo",
                HeaderText = "Sexo",
                Width = 80
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Categoria",
                HeaderText = "Categoría",
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrecioVenta",
                HeaderText = "Precio",
                DefaultCellStyle = { Format = "C2" },
                Width = 80
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Stock",
                HeaderText = "Stock",
                Width = 60
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Total",
                HeaderText = "Total",
                DefaultCellStyle = { Format = "C2" },
                Width = 100
            });

            dgv_productos.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_productos.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);

            dgv_productos.DoubleBuffered(true);
        }


        private async void View_Buscar_Load(object sender, EventArgs e)
        {
            dgv_productos.Enabled = false;

            try
            {
                loadingOverlay.ShowOverlay();
                productos = await productosController.ObtenerProductosParaVentaAsync();
                bindingSource.DataSource = productos;

                AjustarColumnas();

                if (productos.Count == 0)
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("No se encontraron productos.");
                }
                   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgv_productos.Enabled = true;
                loadingOverlay.HideOverlay();
            }
        }

        private void txt_producto_TextChanged(object sender, EventArgs e)
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


        private void btn_seleccionar_Click(object sender, EventArgs e) => Seleccionar();
        private void btn_salir_Click(object sender, EventArgs e) => Salir();

        private void View_Buscar_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter: Seleccionar(); break;
                case Keys.Escape: Salir(); break;
            }
        }

        private void Seleccionar()
        {
            if (dgv_productos.CurrentRow != null)
                productoSelect = (ProductoVentaDTO)dgv_productos.CurrentRow.DataBoundItem;

            Close();
        }

        private void Salir() => Close();

        private void AjustarColumnas()
        {
            foreach (DataGridViewColumn col in dgv_productos.Columns)
            {
                switch (col.Name)
                {
                    case "Código":
                        col.Width = 103;
                        break;
                    case "Nombre":
                        col.Width = 388;
                        break;
                    case "Medida":
                        col.Width = 259;
                        break;
                    case "Precio":
                        col.Width = 129;
                        break;
                    default:
                        break;
                }
            }

            if (dgv_productos.Columns.Contains("codigo"))
                dgv_productos.Columns["codigo"].Frozen = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgv_productos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_productos.CurrentRow != null)
            {
                // tu lógica aquí: por ejemplo
                var seleccionado = (ProductoVentaDTO)dgv_productos.CurrentRow.DataBoundItem;
                pb_foto.ImageLocation = rutaBaseImagenes+seleccionado.Foto;
            }
        }
    }

    // Extensión para activar DoubleBuffer en tiempo de ejecución
    public static class DataGridViewExtensions
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgv, new object[] { setting });
        }
    }
}