using Punto_de_Venta.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Punto_de_Venta.Controlador
{
    public class VistasController
    {
        public List<ViewCorte> CorteProductos(DateTime fecha)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var result = context.ViewCorte
                        .Where(x => x.fecha == fecha)
                        .OrderByDescending(x => x.Total)
                        .ToList();

                    return result?.Count > 0 ? result : null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener el corte de productos: " + e.Message, e);
            }
        }

        public List<ViewCorte> ReporteVentasMes(DateTime fecha)
        {
            try
            {
                using (var context = new la_ross_dbEntities())
                {
                    var result = context.ViewCorte
                        .Where(x => x.fecha.Month == fecha.Month)
                        .OrderByDescending(x => x.Total)
                        .ToList();

                    if (result == null || result.Count == 0)
                        return null;

                    var auxiliar = new List<ViewCorte>();

                    foreach (var x in result)
                    {
                        var existente = auxiliar.FirstOrDefault(y => y.Nombre == x.Nombre);
                        if (existente != null)
                        {
                            existente.Cantidad += x.Cantidad;
                            existente.Total += x.Total;
                        }
                        else
                        {
                            auxiliar.Add(new ViewCorte
                            {
                                Nombre = x.Nombre,
                                Cantidad = x.Cantidad,
                                Total = x.Total
                            });
                        }
                    }

                    return auxiliar.OrderByDescending(x => x.Total).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al generar reporte mensual: " + e.Message, e);
            }
        }
    }
}
