using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Punto_de_Venta.Controlador
{
    public class InventarioFisicoController
    {
        public async Task<int> CrearInventarioFisico(int idUsuario, bool incluirStockCero, string observaciones = null)
        {
            using (var context = new la_ross_dbEntities())
            {
                var inventario = new InventarioFisico
                {
                    fecha_inicio = DateTime.Now,
                    id_usuario = idUsuario,
                    observaciones = observaciones,
                    ajustado = false,
                    estatus = true
                };

                context.InventarioFisico.Add(inventario);
                await context.SaveChangesAsync();

                int idInventario = inventario.id;

                var articulos = await context.Articulos
                    .Where(a => a.estatus == true && (incluirStockCero || a.stock > 0))
                    .ToListAsync();

                foreach (var articulo in articulos)
                {
                    var detalle = new InventarioFisicoDetalle
                    {
                        id_inventario = idInventario,
                        id_articulo = articulo.id_producto,
                        stock_sistema = articulo.stock,
                        cantidad_contada = 0
                    };

                    context.InventarioFisicoDetalle.Add(detalle);
                }

                await context.SaveChangesAsync();
                return idInventario;
            }
        }

        public async Task<List<InventarioFisico>> ObtenerInventarios()
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.InventarioFisico
                    .Where(i => i.estatus == true)
                    .OrderByDescending(i => i.fecha_inicio)
                    .ToListAsync();
            }
        }

        public async Task<InventarioFisico> ObtenerInventarioActivo()
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.InventarioFisico
                    .Where(i => i.estatus == true && i.ajustado == false)
                    .OrderByDescending(i => i.fecha_inicio)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<List<InventarioFisicoDetalle>> ObtenerDetalle(int idInventario)
        {
            using (var context = new la_ross_dbEntities())
            {
                return await context.InventarioFisicoDetalle
                    .Where(d => d.id_inventario == idInventario)
                    .ToListAsync();
            }
        }

        public async Task<bool> RegistrarConteo(int idDetalle, int cantidadContada)
        {
            using (var context = new la_ross_dbEntities())
            {
                var detalle = await context.InventarioFisicoDetalle.FindAsync(idDetalle);
                if (detalle == null) return false;

                detalle.cantidad_contada = cantidadContada;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> AjustarInventario(int idInventario)
        {
            using (var context = new la_ross_dbEntities())
            {
                var inventario = await context.InventarioFisico.FindAsync(idInventario);
                if (inventario == null || inventario.ajustado == true) return false;

                var detalles = await context.InventarioFisicoDetalle
                    .Where(d => d.id_inventario == idInventario)
                    .ToListAsync();

                int totalNoCuadraron = 0;
                int totalMenos = 0;
                int totalMas = 0;

                foreach (var detalle in detalles)
                {
                    int diferencia = detalle.cantidad_contada - detalle.stock_sistema;

                    if (diferencia != 0)
                    {
                        totalNoCuadraron++;

                        if (diferencia < 0)
                            totalMenos++;
                        else if (diferencia > 0)
                            totalMas++;
                    }

                    var articulo = await context.Articulos.FindAsync(detalle.id_articulo);
                    if (articulo != null)
                    {
                        articulo.stock = detalle.cantidad_contada;
                    }
                }

                inventario.ajustado = true;
                inventario.fecha_fin = DateTime.Now;

                inventario.observaciones = $"Resumen de Inventario Ajustado: \n" +
                                           $"Productos con diferencia: {totalNoCuadraron}\n" +
                                           $"Productos con stock MENOR al sistema: {totalMenos}\n" +
                                           $"Productos con stock MAYOR al sistema: {totalMas}";

                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> RevertirAjusteInventario(int idInventario)
        {
            using (var context = new la_ross_dbEntities())
            {
                var inventario = await context.InventarioFisico.FindAsync(idInventario);
                if (inventario == null || inventario.ajustado == false)
                    return false; // No existe o no está ajustado para revertir

                var detalles = await context.InventarioFisicoDetalle
                    .Where(d => d.id_inventario == idInventario)
                    .ToListAsync();

                foreach (var detalle in detalles)
                {
                    var articulo = await context.Articulos.FindAsync(detalle.id_articulo);
                    if (articulo != null)
                    {
                        // Revertir el stock al valor original guardado en stock_sistema
                        articulo.stock = detalle.stock_sistema;
                    }
                }

                // Marca el inventario como no ajustado y limpia fecha_fin y observaciones
                inventario.ajustado = false;
                inventario.fecha_fin = null;
                inventario.observaciones = "Ajuste revertido automáticamente";

                await context.SaveChangesAsync();
                return true;
            }
        }



        public async Task<bool> CancelarInventario(int idInventario)
        {
            using (var context = new la_ross_dbEntities())
            {
                var inventario = await context.InventarioFisico.FindAsync(idInventario);
                if (inventario == null || inventario.ajustado == true) return false;

                inventario.estatus = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
