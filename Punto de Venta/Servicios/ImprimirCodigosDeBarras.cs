using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Threading.Tasks;
using BarcodeStandard;
using SkiaSharp;

namespace Punto_de_Venta.Servicios
{
    public static class ImprimirCodigosDeBarras
    {
        // Convierte SKImage (SkiaSharp) a System.Drawing.Image
        private static Image ConvertirSkImageADrawingImage(SKImage skImage)
        {
            using (SKData data = skImage.Encode(SKEncodedImageFormat.Png, 100))
            using (MemoryStream ms = new MemoryStream(data.ToArray()))
            {
                return Image.FromStream(ms);
            }
        }

        public static async Task ImprimirCodigoAsync(string nombreProducto, string codigo, int cantidadCopias = 1, string nombreImpresora = "Xprinter XP-420B")
        {
            await Task.Run(() => ImprimirCodigo(nombreProducto, codigo, cantidadCopias, nombreImpresora));
        }

        // Imprime el código de barras generado para el string "codigo"
        // cantidadCopias indica cuántas veces se imprimirá el mismo código
        public static void ImprimirCodigo(string nombreProducto, string codigo, int cantidadCopias = 1, string nombreImpresora = "Xprinter XP-420B")
        {
            int dpi = 203; // dpi típico de la impresora térmica
            int anchoPx = (int)(1.97 * dpi);  // 50 mm ≈ 400 px
            int altoPx = (int)(0.98 * dpi);   // 25 mm ≈ 200 px

            int margenImpresora = 10;
            int anchoFinal = anchoPx - margenImpresora * 2;  // ≈ 380 px
            Bitmap etiqueta = new Bitmap(anchoFinal, 85);

            Graphics g = Graphics.FromImage(etiqueta);

            try
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.High;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                // Dibuja borde negro para ver límites (usando ancho y alto reales)

                Font fuenteNombre = new Font("Arial", 10, FontStyle.Bold);
                Font fuenteCodigo = new Font("Arial", 10);

                // Nombre del producto centrado arriba
                RectangleF rectNombre = new RectangleF(000000003, 5, etiqueta.Width, 20);
                g.DrawString(nombreProducto, fuenteNombre, Brushes.Black, rectNombre, new StringFormat { Alignment = StringAlignment.Center });

                // Código de barras: dimensiones y margen izquierdo ajustados
                int anchoBarcode = etiqueta.Width / 4;
                int altoBarcode = 30;
                int margenIzquierdo = (etiqueta.Width - anchoBarcode) / 2;

                Barcode barcode = new Barcode
                {
                    IncludeLabel = false,
                    Alignment = AlignmentPositions.Center
                };

                SKImage skImage = barcode.Encode(
                    BarcodeStandard.Type.Code128,
                    codigo,
                    new SKColorF(0, 0, 0),
                    new SKColorF(1, 1, 1),
                    94,
                    20
                );

                Image imagen = ConvertirSkImageADrawingImage(skImage);

                // Dibujar código de barras centrado horizontalmente
                g.DrawImage(imagen, new Rectangle(margenIzquierdo, 22, anchoBarcode, altoBarcode));

                // Texto del código debajo del código de barras
                RectangleF rectCodigo = new RectangleF(0, 22 + altoBarcode + 1, etiqueta.Width, 20);
                g.DrawString(codigo, fuenteCodigo, Brushes.Black, rectCodigo, new StringFormat { Alignment = StringAlignment.Center });

                // Preparar impresión
                int copiasImpresas = 0;
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("Etiqueta50x25", etiqueta.Width, etiqueta.Height+5);

                if (!string.IsNullOrEmpty(nombreImpresora))
                {
                    pd.PrinterSettings.PrinterName = nombreImpresora;
                    if (!pd.PrinterSettings.IsValid)
                        throw new Exception($"La impresora '{nombreImpresora}' no está instalada o no es válida.");
                }

                pd.PrintPage += (s, e) =>
                {
                    e.Graphics.DrawImage(etiqueta, 0, 0);
                    copiasImpresas++;
                    e.HasMorePages = copiasImpresas < cantidadCopias;
                };

                pd.Print();
            }
            finally
            {
                g.Dispose();
                etiqueta.Dispose();
            }
        }
    }
}
