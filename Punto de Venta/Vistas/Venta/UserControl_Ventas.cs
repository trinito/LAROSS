using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Punto_de_Venta.Modelo;
using Punto_de_Venta.Controlador;
using Punto_de_Venta.Servicios;
using System.Drawing.Printing;
using System.Globalization;

namespace Punto_de_Venta.Vistas
{
    public partial class UserControl_Ventas : UserControl
    {
        #region VARIABLES Y PROPIEDADES
        private decimal total = 0;

        private decimal total_copia = 0;
        private decimal pago_copia = 0;
        private decimal cambio_copia = 0;
        private string forma_pago_copia = "";

        private List<ProductoVentaDTO> productos;

        private List<ProductoVentaDTO> productos_copia;

        #endregion

        public UserControl_Ventas()
        {
            InitializeComponent();
            productos = new List<ProductoVentaDTO>();
        }

        private void UserControl_Ventas_Load(object sender, EventArgs e)
        {
            dgv_productos.AutoGenerateColumns = false;
            txt_producto.Focus();
            ConfigurarDgvProductos();
        }

        private void ConfigurarDgvProductos()
        {
            dgv_productos.DataSource = null;
            dgv_productos.Columns.Clear();

            // Configurar columnas específicas del DTO
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
                DataPropertyName = "Cantidad",
                HeaderText = "Cant.",
                Width = 60
            });

            dgv_productos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Total",
                HeaderText = "Total",
                DefaultCellStyle = { Format = "C2" },
                Width = 100
            });

            // Estilo visual igual a dgv_marcas
            dgv_productos.ReadOnly = true;
            dgv_productos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_productos.MultiSelect = false;
            dgv_productos.AllowUserToAddRows = false;
            dgv_productos.AllowUserToDeleteRows = false;
            dgv_productos.AllowUserToResizeRows = false;
            dgv_productos.AllowUserToResizeColumns = false;
            dgv_productos.RowHeadersVisible = false;

            dgv_productos.EnableHeadersVisualStyles = false;
            dgv_productos.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv_productos.ColumnHeadersDefaultCellStyle.BackColor;
            dgv_productos.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv_productos.ColumnHeadersDefaultCellStyle.ForeColor;

            dgv_productos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_productos.DefaultCellStyle.Font = new Font("Rockwell", 10);
            dgv_productos.ColumnHeadersDefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
        }



        private void txt_producto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && !string.IsNullOrEmpty(txt_producto.Text))
            {
                try
                {
                    int cantidad_aux = 1;
                    string codigo_producto = txt_producto.Text.Trim();
                    if (lbl_cambio.Text != "$0")
                    {
                        lbl_cambio.Text = "$0";
                    }
                    Busqueda(codigo_producto, cantidad_aux);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
                finally
                {
                    Limpiar();
                }
            }
        }
        private void button_buscar_Click(object sender, EventArgs e)
        {
            AbrirBuscar();
        }

        private void button_cobrar_Click(object sender, EventArgs e)
        {
            AbrirCobrar();
        }

        private void button_quitar_Click(object sender, EventArgs e)
        {
            EliminarProducto();
        }



        #region METODOS


        void AbrirBuscar()
        {
            using (var form = new View_Buscar())
            {
                form.ShowDialog();
                if (form.productoSelect != null)
                {
                    Busqueda(form.productoSelect.CodigoBarras, 1);
                }
            }
        }

        private void EliminarProducto()
        {
            if (dgv_productos.CurrentRow != null)
            {
                ProductoVentaDTO producto = (ProductoVentaDTO)dgv_productos.CurrentRow.DataBoundItem;
                dgv_productos.DataSource = null;
                productos.Remove(producto);
                dgv_productos.DataSource = productos;
                Totalizar();
            }
        }

        void AbrirCobrar()
        {
            if (total == 0)
            {
                MessageBox.Show(
                 "Por favor, ingresa al menos un producto antes de cobrar.",
                 "Atención",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
                return;
            }
            View_Cobrar form = new View_Cobrar();
            form.total = this.total;
            form.ShowDialog();
            if (form.compra)
            {
                GuardarVenta(form.total, form.pago, form.cambio, form.forma_pago);
            }
            form.Dispose();
        }

        private void Busqueda(string codigo, int cantidad_aux)
        {
            var producto = productos.FirstOrDefault(x => x.CodigoBarras == codigo);

            if (producto == null)
            {
                ProductosController productosController = new ProductosController();
                var nuevoProducto = productosController.BuscarProductoParaVenta(codigo);

                if (nuevoProducto == null)
                {
                    MessageBox.Show(
                     "No encontramos ningún producto con ese código.\nIntenta con otro código o usa el buscador.",
                     "Producto no encontrado",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    return;
                }

                if (cantidad_aux > nuevoProducto.Stock)
                {
                    MessageBox.Show($"Stock insuficiente. Disponible: {nuevoProducto.Stock}", "¡Stock limitado!");
                    return;
                }

                nuevoProducto.Cantidad = cantidad_aux;
                productos.Add(nuevoProducto);
            }
            else
            {
                if ((producto.Cantidad + cantidad_aux) > producto.Stock)
                {
                    MessageBox.Show($"No hay suficiente stock para agregar más unidades. Disponible: {producto.Stock}", "¡Stock insuficiente!");
                    return;
                }

                producto.Cantidad += cantidad_aux;
            }

            dgv_productos.DataSource = null;
            dgv_productos.DataSource = productos;

            Totalizar();
        }

        private void Totalizar()
        {
            total = productos.Sum(x => x.Total);
            lbl_por_pagar.Text = total.ToString("C", CultureInfo.CurrentCulture);
        }

        private void Limpiar()
        {
            txt_producto.Text = "";
            txt_producto.Focus();
        }
        private void LimpiarTodo(decimal cambio)
        {
            total = 0;
            productos = new List<ProductoVentaDTO>();
            dgv_productos.DataSource = null;
            lbl_por_pagar.Text = "$0";
            lbl_cambio.Text = cambio.ToString("C", CultureInfo.CurrentCulture); ;
            Limpiar();

        }

        private ProductoCaja GetProducto(string codigo)
        {
            ProductosController productosController = new ProductosController();
            Articulos producto = productosController.GetProducto(codigo);
            if (producto == null)
            {
                return null;
            }


            return ConvertMenu(producto);

        }

        private ProductoCaja ConvertMenu(Articulos producto)
        {
            ProductoCaja productoCaja = new ProductoCaja
            { id_producto = producto.id_producto, codigo = producto.codigo_barras, nombre = producto.nombre, precio = producto.precio_venta };


            return productoCaja;
        }

        private void GuardarVenta(decimal total, decimal pago, decimal cambio, string forma_pago)
        {
            try
            {
                VentaService ventaService = new VentaService();
                // Se asume que "productos" es List<ProductoVentaDTO> y que cada producto tiene la propiedad Cantidad (int)
                bool exito = ventaService.RealizarVenta(
                    DateTime.Now,
                    DateTime.Now.ToString("hh:mm tt"),
                    productos,
                    forma_pago,
                    idUsuario: SesionUsuario.UsuarioActual.id // asigna aquí el id del usuario que realiza la venta
                );

                if (exito)
                {
                    // Guardar copias para impresión o futuras referencias
                    total_copia = total;
                    pago_copia = pago;
                    cambio_copia = cambio;
                    forma_pago_copia = forma_pago;
                    productos_copia = productos;

                    ImprimirTicket(total, pago, cambio, forma_pago);
                }
                else
                {
                    MessageBox.Show(
                    "Ocurrió un problema al registrar la venta. Por favor, intenta de nuevo.",
                    "Error en la venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la venta: " + ex.Message, "¡Alerta!");
            }
        }


        private void ImprimirTicket(decimal total, decimal pago, decimal cambio, string forma_pago, bool isCopia = false)
        {
            try
            {
                ImprimirTickets ticket = new ImprimirTickets();
                //ticket.TextoIzquierda(" ");
                //ticket.TextoCentro("SU RECIBO GRACIAS HASTA PRONTO");
                ticket.TextoCentro("LA ROSS");
                ticket.TextoCentro("BLVD. RIO FUERTE 728, COL. SCALLY");
                ticket.TextoCentro("LOS MOCHIS, SINALOA");
                ticket.TextoIzquierda(" ");
                VentaController controler = new VentaController();
                int ticke = controler.NumTicket();
                if (ticke > 0)
                {
                    ticket.TextoIzquierda("No. TICKET " + ticke.ToString());
                }

                ticket.TextoExtremos(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("hh:mm tt"));
                ticket.TextoIzquierda(" ");
                ticket.EncabezadoVenta();
                ticket.lineasGuio();
                if (isCopia)
                {
                    foreach (ProductoVentaDTO producto in productos_copia)
                    {
                        ticket.AgregaArticulo(producto.Nombre, producto.Cantidad, producto.PrecioVenta);
                    }
                }
                else
                {
                    foreach (ProductoVentaDTO producto in productos)
                    {
                        ticket.AgregaArticulo(producto.Nombre, producto.Cantidad, producto.PrecioVenta);
                    }
                }

                ticket.lineasGuio();
                ticket.TextoDerecha(forma_pago);
                ticket.AgregarTotales("               TOTAL:  ", total);
                ticket.AgregarTotales("            SU PAGO :  ", pago);
                ticket.AgregarTotales("              CAMBIO: ", cambio);
                //ticket.TextoIzquierda(" ");
                //ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda(" ");
                //ticket.TextoIzquierda(" ");
                ticket.TextoCentro("NO SE ACEPTA CAMBIO NI DEVOLUCION");
                ticket.TextoIzquierda(" ");
                ticket.TextoCentro("SU RECIBO GRACIAS HASTA PRONTO");
                //ticket.TextoIzquierda(" ");

                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda(" ");
                ticket.CortaTicket();

                ticket.ImprimirTicket("ZJ-58");
                //if (!isCopia)
                //{
                //    printDocument1.PrintPage -= Imprimir;
                //    printDocument1.PrintPage += ImprimirImagen;
                //    printDocument1.Print();
                //}
                LimpiarTodo(cambio);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir el ticket: {ex.Message}");
            }

        }

        #endregion

        private void button_copia_Click(object sender, EventArgs e)
        {
            ImprimirCopia();
        }

        private void ImprimirCopia()
        {
            if (total_copia != 0 && pago_copia != 0)
            {
                ImprimirTicket(total_copia, pago_copia, cambio_copia, forma_pago_copia, true);
                total_copia = 0; pago_copia = 0; cambio_copia = 0;
            }

        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F4:
                    AbrirCobrar();
                    break;
                case Keys.F1:
                    AbrirBuscar();
                    break;
                case Keys.Delete:
                    EliminarProducto();
                    break;
                case Keys.F5:
                    ImprimirCopia();
                    break;
            }
        }
    }
}
