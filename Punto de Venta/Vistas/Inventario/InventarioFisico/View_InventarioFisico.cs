using Punto_de_Venta.Controlador;
using Punto_de_Venta.Controles;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Servicios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta.Vistas.Inventario.InventarioFisico
{
    public partial class View_InventarioFisico : Form
    {
        private readonly BindingSource bindingSource;
        InventarioFisicoController inventarioFisicoController;
        private LoadingControl loadingOverlay;
        private int idInventarioActual;

        public View_InventarioFisico()
        {
            InitializeComponent();
            loadingOverlay = new LoadingControl();
            this.Controls.Add(loadingOverlay);
            loadingOverlay.BringToFront();
            bindingSource = new BindingSource();
            inventarioFisicoController = new InventarioFisicoController();
            GridViewHelper();
        }

        private async void View_InventarioFisico_Load(object sender, EventArgs e)
        {
            try
            {
                panel_main.Enabled = false;
                await InicializarInventarioFisicoAsync();
            }
            finally
            {
                loadingOverlay.HideOverlay();
                panel_main.Enabled = true;
            }
        }

        private async Task InicializarInventarioFisicoAsync()
        {
            try
            {
                int idUsuario = SesionUsuario.UsuarioActual.id;

                var inventarioExistente = await inventarioFisicoController.ObtenerInventarioActivo();

                if (inventarioExistente != null)
                {
                    idInventarioActual = inventarioExistente.id;
                    lbl_fecha.Text = inventarioExistente.fecha_inicio.ToString("dd/MM/yyyy");
                    lbl_fecha.Text = inventarioExistente.fecha_inicio.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));
                }
                else
                {
                    // No mostrar overlay antes del diálogo para evitar bloquear UI
                    DialogResult result = MessageBox.Show(
                        "¿Deseas incluir productos con stock 0 en el inventario físico?",
                        "Opciones de Inventario",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    bool incluirStockCero = (result == DialogResult.Yes);

                    loadingOverlay.ShowOverlay(); // Mostrar overlay justo antes de operación async que tarda


                    idInventarioActual = await Task.Run(() => inventarioFisicoController.CrearInventarioFisico(idUsuario, incluirStockCero));
                    lbl_fecha.Text = DateTime.Today.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));

                }
                lbl_inventario.Text = "# " + idInventarioActual;


                // Obtener detalles del inventario (usando async)
                var detalles = await inventarioFisicoController.ObtenerDetalle(idInventarioActual);

                using (var context = new la_ross_dbEntities())
                {
                    var productos = await (from d in context.InventarioFisicoDetalle
                                           join a in context.Articulos on d.id_articulo equals a.id_producto
                                           join m in context.Marcas on a.id_marca equals m.id_marca
                                           join c in context.Colores on a.id_color equals c.id_color
                                           join t in context.Tallas on a.id_talla equals t.id_talla
                                           join s in context.Sexos on a.id_sexo equals s.id_sexo
                                           join cat in context.Categorias on a.id_categoria equals cat.id_categoria
                                           where d.id_inventario == idInventarioActual
                                           select new ProductoInventarioFisicoDTO
                                           {
                                               IdDetalle = d.id,
                                               CodigoBarras = a.codigo_barras,
                                               Nombre = a.nombre,
                                               Marca = m.nombre,
                                               Color = c.nombre,
                                               Talla = t.nombre,
                                               Sexo = s.nombre,
                                               Categoria = cat.nombre,
                                               CantidadContada = d.cantidad_contada,
                                               StockSistema = a.stock,
                                               Diferencia = d.cantidad_contada - a.stock
                                           }).ToListAsync();

                    bindingSource.DataSource = productos;
                }
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private void GridViewHelper()
        {
            dgv_productos.AutoGenerateColumns = false;
            dgv_productos.DataSource = bindingSource;

            dgv_productos.Columns.Clear();

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoBarras",
                DataPropertyName = "CodigoBarras",
                HeaderText = "Código de Barras",
                Width = 120
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 210
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Marca",
                DataPropertyName = "Marca",
                HeaderText = "Marca",
                Width = 120
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Color",
                DataPropertyName = "Color",
                HeaderText = "Color",
                Width = 120
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
                Width = 100
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categoria",
                DataPropertyName = "Categoria",
                HeaderText = "Categoría",
                Width = 135
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockSistema",
                DataPropertyName = "StockSistema",
                HeaderText = "Stock Sistema",
                Width = 90
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadContada",
                DataPropertyName = "CantidadContada",
                HeaderText = "Cantidad Contada",
                Width = 90
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Diferencia",
                DataPropertyName = "Diferencia",
                HeaderText = "Diferencia",
                Width = 90
            });

            dgv_productos.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_productos.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);

            if (dgv_productos.Columns.Contains("CodigoBarras"))
                dgv_productos.Columns["CodigoBarras"].Frozen = true;

            dgv_productos.DoubleBuffered(true);
        }

        private async void txt_producto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string codigo = txt_producto.Text.Trim();
                if (!string.IsNullOrEmpty(codigo))
                {
                    loadingOverlay.ShowOverlay();
                    try
                    {
                        await ProcesarCodigoEscaneadoAsync(codigo);
                    }
                    finally
                    {
                        loadingOverlay.HideOverlay();
                    }
                    txt_producto.Clear();
                    txt_producto.Focus();
                }
                e.Handled = true;
            }
        }

        private async Task ProcesarCodigoEscaneadoAsync(string codigo)
        {
            loadingOverlay.ShowOverlay();
            var listaProductos = bindingSource.DataSource as List<ProductoInventarioFisicoDTO>;
            if (listaProductos == null) return;

            var producto = listaProductos.FirstOrDefault(p => p.CodigoBarras == codigo);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado en el inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nuevaCantidad = producto.CantidadContada + 1;

            bool exito = await inventarioFisicoController.RegistrarConteo(producto.IdDetalle, nuevaCantidad);

            if (exito)
            {
                producto.CantidadContada = nuevaCantidad;
                producto.Diferencia = nuevaCantidad - producto.StockSistema;

                bindingSource.ResetBindings(false);

                int index = listaProductos.IndexOf(producto);
                if (index >= 0 && index < dgv_productos.Rows.Count)
                {
                    dgv_productos.ClearSelection();
                    dgv_productos.Rows[index].Selected = true;
                    dgv_productos.CurrentCell = dgv_productos.Rows[index].Cells[0];
                }
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la cantidad contada en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loadingOverlay.HideOverlay();
        }

        private async void btn_finalizar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Estás seguro que quieres finalizar y ajustar el inventario? Esto actualizará el stock real.",
                "Confirmar Finalización",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No) return;

            var listaProductos = bindingSource.DataSource as List<ProductoInventarioFisicoDTO>;
            if (listaProductos == null || listaProductos.Count == 0)
            {
                MessageBox.Show("No hay productos en el inventario para finalizar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int productosConCero = listaProductos.Count(p => p.CantidadContada == 0);

            if (productosConCero > 0)
            {
                var resultado = MessageBox.Show(
                    $"Hay {productosConCero} productos con cantidad 0. ¿Deseas continuar y ajustar el inventario de todas formas?",
                    "Advertencia",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.No) return;
            }

            loadingOverlay.ShowOverlay();

            try
            {
                bool exito = await inventarioFisicoController.AjustarInventario(idInventarioActual);

                if (exito)
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("Inventario finalizado y ajustado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); 
                }
                else
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("Error al finalizar el inventario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                loadingOverlay.HideOverlay();
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }

        private async void btn_cancelar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Deseas cancelar este inventario sin guardar cambios?",
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            loadingOverlay.ShowOverlay();

            try
            {
                bool exito = await inventarioFisicoController.CancelarInventario(idInventarioActual);

                if (exito)
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("Inventario cancelado correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("No se pudo cancelar el inventario. Ya podría estar finalizado o no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                loadingOverlay.HideOverlay();
                MessageBox.Show($"Error al cancelar inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }


        private async Task RevertirAjusteInventario(int idInventario)
        {
            loadingOverlay.ShowOverlay();
            try
            {
                bool exito = await Task.Run(() => inventarioFisicoController.RevertirAjusteInventario(idInventario));
                // idInventarioActual = await Task.Run(() => inventarioFisicoController.CrearInventarioFisico(idUsuario, incluirStockCero));
                if (exito)
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("Ajuste revertido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    loadingOverlay.HideOverlay();
                    MessageBox.Show("No se pudo revertir el ajuste. Verifica que el inventario esté ajustado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                loadingOverlay.HideOverlay();
            }
        }
    }
}
