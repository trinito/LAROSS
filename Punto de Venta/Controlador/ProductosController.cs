using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class ProductosController
    {
        public async Task<IEnumerable<Articulos>> GetProductosAsync()
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var productos = await context.Articulos
                        .Where(p => p.estatus)
                        .ToListAsync();

                    productos.ForEach(p => p.precio_venta = Math.Round(p.precio_venta, 2));
                    return productos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message, ex);
            }
        }

        // Buscar por código interno (codigo_barras)
        public Articulos GetProducto(string codigo)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var producto = context.Articulos.FirstOrDefault(p => p.estatus && p.codigo_barras == codigo);

                    if (producto != null)
                        producto.precio_venta = Math.Round(producto.precio_venta, 2);

                    return producto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto: " + ex.Message, ex);
            }
        }

        // Nuevo: Buscar por código original (codigo_barras_original)
        public Articulos GetProductoPorCodigoOriginal(string codigoOriginal)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var producto = context.Articulos.FirstOrDefault(p => p.estatus && p.codigo_barras_original == codigoOriginal);

                    if (producto != null)
                        producto.precio_venta = Math.Round(producto.precio_venta, 2);

                    return producto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto por código original: " + ex.Message, ex);
            }
        }

        public bool UpdateProducto(Articulos producto)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var existente = context.Articulos.SingleOrDefault(p => p.id_producto == producto.id_producto);

                    if (existente == null) return false;

                    existente.codigo_barras = producto.codigo_barras;
                    existente.codigo_barras_original = producto.codigo_barras_original;
                    existente.id_categoria = producto.id_categoria;
                    existente.id_marca = producto.id_marca;
                    existente.nombre = producto.nombre;
                    existente.precio_costo = producto.precio_costo;
                    existente.precio_venta = producto.precio_venta;
                    existente.stock = producto.stock;
                    existente.id_color = producto.id_color;
                    existente.id_talla = producto.id_talla;
                    existente.id_sexo = producto.id_sexo;
                    existente.foto = producto.foto;
                    existente.estatus = producto.estatus;

                    context.Entry(existente).State = System.Data.Entity.EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto: " + ex.Message, ex);
            }
        }

        public async Task<bool> UpdateProductoAsync(Articulos producto)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var existente = await context.Articulos
                        .SingleOrDefaultAsync(p => p.id_producto == producto.id_producto);

                    if (existente == null) return false;

                    // Actualizar campos
                    existente.codigo_barras = producto.codigo_barras;
                    existente.codigo_barras_original = producto.codigo_barras_original;
                    existente.id_categoria = producto.id_categoria;
                    existente.id_marca = producto.id_marca;
                    existente.nombre = producto.nombre;
                    existente.precio_costo = producto.precio_costo;
                    existente.precio_venta = producto.precio_venta;
                    existente.stock = producto.stock;
                    existente.id_color = producto.id_color;
                    existente.id_talla = producto.id_talla;
                    existente.id_sexo = producto.id_sexo;
                    existente.foto = producto.foto;
                    existente.estatus = producto.estatus;

                    context.Entry(existente).State = System.Data.Entity.EntityState.Modified;

                    return await context.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto: " + ex.Message, ex);
            }
        }

        public async Task<int> InsertProductoAsync(Articulos producto)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    // Validación: buscar producto activo con el mismo código de barras original
                    bool existe = await context.Articulos.AnyAsync(p =>
                        p.estatus &&
                        p.codigo_barras_original == producto.codigo_barras_original);

                    if (existe)
                        throw new InvalidOperationException("Ya existe un producto con el mismo código de barras.");

                    context.Articulos.Add(producto);
                    await context.SaveChangesAsync(); // Asíncrono
                    return producto.id_producto;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("updating the entries"))
                {
                    throw new InvalidOperationException("El producto con ese código ya estuvo registrado pero se dio de baja.", ex);
                }
                throw new Exception("Error al insertar producto: " + ex.Message, ex);
            }
        }

        public int InsertProducto(Articulos producto)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    // Validación: buscar producto activo con el mismo código de barras original
                    bool existe = context.Articulos.Any(p =>
                        p.estatus &&
                        p.codigo_barras_original == producto.codigo_barras_original);

                    if (existe)
                        throw new InvalidOperationException("Ya existe un producto con el mismo código de barras.");

                    context.Articulos.Add(producto);
                    context.SaveChanges();
                    return producto.id_producto;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("updating the entries"))
                {
                    throw new InvalidOperationException("El producto con ese código ya estuvo registrado pero se dio de baja.", ex);
                }
                throw new Exception("Error al insertar producto: " + ex.Message, ex);
            }
        }

        public bool DeleteProducto(string codigo)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var producto = context.Articulos.SingleOrDefault(p => p.codigo_barras == codigo);

                    if (producto == null) return false;

                    producto.estatus = false;
                    context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto: " + ex.Message, ex);
            }
        }
        public async Task<List<ProductoVentaDTO>> ObtenerProductosParaVentaAsync()
        {
            using (var context = new la_ross_dbEntities())
            {
                var query = from a in context.Articulos
                            join m in context.Marcas on a.id_marca equals m.id_marca
                            join c in context.Colores on a.id_color equals c.id_color
                            join t in context.Tallas on a.id_talla equals t.id_talla
                            join s in context.Sexos on a.id_sexo equals s.id_sexo
                            join cat in context.Categorias on a.id_categoria equals cat.id_categoria
                            where a.estatus
                            select new ProductoVentaDTO
                            {
                                CodigoBarras = a.codigo_barras,
                                Nombre = a.nombre,
                                Marca = m.nombre,
                                Color = c.nombre,
                                Talla = t.nombre,
                                Sexo = s.nombre,
                                Categoria = cat.nombre,
                                PrecioVenta = a.precio_venta,
                                Stock = a.stock,
                                Foto = a.foto
                            };

                return await Task.FromResult(query.ToList());
            }
        }

        public ProductoVentaDTO BuscarProductoParaVenta(string codigo)
        {
            try
            {
                codigo = codigo.Trim();

                using (var context = new la_ross_dbEntities())
                {
                    var query = from a in context.Articulos
                                join m in context.Marcas on a.id_marca equals m.id_marca
                                join c in context.Colores on a.id_color equals c.id_color
                                join t in context.Tallas on a.id_talla equals t.id_talla
                                join s in context.Sexos on a.id_sexo equals s.id_sexo
                                join cat in context.Categorias on a.id_categoria equals cat.id_categoria
                                where a.estatus &&
                                      (a.codigo_barras == codigo)
                                select new ProductoVentaDTO
                                {
                                    CodigoBarras = a.codigo_barras,
                                    Nombre = a.nombre,
                                    Marca = m.nombre,
                                    Color = c.nombre,
                                    Talla = t.nombre,
                                    Sexo = s.nombre,
                                    Categoria = cat.nombre,
                                    PrecioVenta = a.precio_venta,
                                    Stock = a.stock  // <-- aquí agregas el stock
                                };

                    return query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar producto para venta: " + ex.Message, ex);
            }
        }
    }
}
