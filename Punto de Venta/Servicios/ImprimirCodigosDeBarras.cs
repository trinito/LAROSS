using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using BarcodeStandard;
using SkiaSharp;

namespace Punto_de_Venta.Servicios
{
    public class ImprimirCodigosDeBarras
    {
        // Convierte SKImage (SkiaSharp) a System.Drawing.Image
        private Image ConvertirSkImageADrawingImage(SKImage skImage)
        {
            using (SKData data = skImage.Encode(SKEncodedImageFormat.Png, 100))
            using (MemoryStream ms = new MemoryStream(data.ToArray()))
            {
                return Image.FromStream(ms);
            }
        }

        // Imprime el código de barras generado para el string "codigo"
        // cantidadCopias indica cuántas veces se imprimirá el mismo código
        public void ImprimirCodigo(string codigo, int cantidadCopias = 1, string nombreImpresora = "Xprinter XP-420B")
        {
            int dpi = 203; // dpi típico de la impresora térmica
            int anchoPx = (int)(1.97 * dpi);  // 50 mm en px
            int altoPx = (int)(0.98 * dpi);   // 25 mm en px

            var barcode = new Barcode
            {
                IncludeLabel = true,
                Alignment = AlignmentPositions.Center,
                LabelFont = new SKFont(SKTypeface.FromFamilyName("Arial"), 10)
            };

            SKImage skImage = barcode.Encode(
                BarcodeStandard.Type.Code128,
                codigo,
                new SKColorF(0, 0, 0),  // negro
                new SKColorF(1, 1, 1),  // blanco
                anchoPx,
                altoPx
            );

            Image imagen = ConvertirSkImageADrawingImage(skImage);

            int copiasImpresas = 0;
            PrintDocument pd = new PrintDocument();

            if (!string.IsNullOrEmpty(nombreImpresora))
            {
                pd.PrinterSettings.PrinterName = nombreImpresora;
                if (!pd.PrinterSettings.IsValid)
                    throw new Exception($"La impresora '{nombreImpresora}' no está instalada o no es válida.");
            }

            int imagenAncho = anchoPx / 2;
            int imagenAlto = altoPx / 2;
            int x = (anchoPx - imagenAncho) / 2;
            int y = 40; // Deja espacio arriba para el nombre del producto

            pd.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawImage(imagen, new Rectangle(x, y, imagenAncho, imagenAlto));
                copiasImpresas++;
                e.HasMorePages = copiasImpresas < cantidadCopias;
            };

            pd.Print();
        }
    }
}
