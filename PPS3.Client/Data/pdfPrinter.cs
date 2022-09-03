using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using System.Diagnostics;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Layout.Borders;

namespace PPS3.Client.Data
{
    public class pdfPrinter
    {
        private readonly IWebHostEnvironment _env;

        public pdfPrinter(IWebHostEnvironment env) => _env = env;

        private string GenerarCadenaAleatoria()
        {
            var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var arregloCaracteres = new Char[5];
            var random = new Random();
            for (int i = 0; i < arregloCaracteres.Length; i++)
            {
                arregloCaracteres[i] = caracteres[random.Next(caracteres.Length)];
            }
            var aleatorio = new String(arregloCaracteres);
            return aleatorio;
        }

        public bool GenerarPdfPresupuesto(Presupuesto presBase)
        {
            //Compruebo la existencia del objeto y sus detalles
            if (presBase == null || presBase.DetallePresupuesto == null || presBase.DetallePresupuesto.Count() < 1)
                return false;

            //Guardo los datos basicos del encabezado
            var numPres = presBase.NumPresu;
            var cliente = presBase.Cliente;
            var observ = "NINGUNA";
            if(!string.IsNullOrEmpty(presBase.Observaciones))
                observ = presBase.Observaciones;
            var fecha = presBase.FechaCrea;
            var usuario = presBase.UsuarioCrea;

            var totalPres = presBase.DetallePresupuesto.Sum(s => s.SubTotal);

            //Otengo la ruta directa a mis Documentos para crear ahi los pdf
            string rutaDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Ecommerce\\Presupuestos";
            var randomStamp = GenerarCadenaAleatoria();
            string rutaCompleta = rutaDir + "\\presupuesto_num_" + numPres + "_" + randomStamp + ".pdf";
            //Compruebo si existe la carpeta donde se guardan los pdf, si no existe se crea, pero si existe no se crea
            Directory.CreateDirectory(rutaDir);

            //Comienzo a crear los objetos para configurar el pdf
            PdfWriter pdfW = new PdfWriter(rutaCompleta);
            PdfDocument pdfDoc = new PdfDocument(pdfW);
            Document document = new Document(pdfDoc);

            //Armo el encabezado
            Paragraph espacioBlanco = new Paragraph("\n");
            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = new Image(ImageDataFactory
                .Create(rutaImg))
                .SetHeight(50)
                .SetWidth(50)
                .SetTextAlignment(TextAlignment.LEFT);

            Paragraph headNumPer = new Paragraph("PRESUPUESTO Nº " + numPres + "\n" + "GENERAL ALVEAR, MENDOZA - " + fecha + "\n" + "CLIENTE: " + cliente)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBold()
                .SetFontSize(12);

            Table tablaEncabezado = new Table(2, true);
            Cell celdaEncab11 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.WHITE)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetWidth(35f)
                .Add(logo);
            Cell celdaEncab12 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.WHITE)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(headNumPer);

            tablaEncabezado.AddCell(celdaEncab11);
            tablaEncabezado.AddCell(celdaEncab12);
            document.Add(tablaEncabezado);
            document.Add(espacioBlanco);

            //Armo el cuerpo
            Table tablaDetalles = new Table(7, false);

            Cell celdaEncDet1 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetBold()
            .Add(new Paragraph("Nombre Producto"));

            Cell celdaEncDet2 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetBold()
            .Add(new Paragraph("Cantidad"));

            Cell celdaEncDet3 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetBold()
            .Add(new Paragraph("Unidad Medida"));

            Cell celdaEncDet4 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetBold()
            .Add(new Paragraph("Precio Venta"));

            Cell celdaEncDet5 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetBold()
            .Add(new Paragraph("% Bonif"));

            Cell celdaEncDet6 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetBold()
            .Add(new Paragraph("Bonificacion Total"));

            Cell celdaEncDet7 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetBold()
            .Add(new Paragraph("Sub Total"));

            tablaDetalles.AddCell(celdaEncDet1);
            tablaDetalles.AddCell(celdaEncDet2);
            tablaDetalles.AddCell(celdaEncDet3);
            tablaDetalles.AddCell(celdaEncDet4);
            tablaDetalles.AddCell(celdaEncDet5);
            tablaDetalles.AddCell(celdaEncDet6);
            tablaDetalles.AddCell(celdaEncDet7);

            foreach (var det in presBase.DetallePresupuesto)
            {
                //Genero las celdas y las uno a la tabla
                Cell celdaCuerpo1 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(det.NombreProducto));

                Cell celdaCuerpo2 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(det.Cantidad.ToString()));

                Cell celdaCuerpo3 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(det.DescripcionUnidad));

                Cell celdaCuerpo4 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph("$ " + det.PrecioUnit.ToString()));

                Cell celdaCuerpo5 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(det.Bonificacion.ToString() + " %"));

                Cell celdaCuerpo6 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph("$ " + det.BonificacionTotal.ToString()));

                Cell celdaCuerpo7 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph("$ " + det.SubTotal.ToString()));

                tablaDetalles.AddCell(celdaCuerpo1);
                tablaDetalles.AddCell(celdaCuerpo2);
                tablaDetalles.AddCell(celdaCuerpo3);
                tablaDetalles.AddCell(celdaCuerpo4);
                tablaDetalles.AddCell(celdaCuerpo5);
                tablaDetalles.AddCell(celdaCuerpo6);
                tablaDetalles.AddCell(celdaCuerpo7);
            }

            document.Add(tablaDetalles.SetHorizontalAlignment(HorizontalAlignment.CENTER));
            document.Add(espacioBlanco);

            //Agrego el total
            Paragraph textoTotal = new Paragraph("TOTAL: $ " + totalPres.ToString())
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBold()
                .SetFontSize(10);

            document.Add(textoTotal);

            //Muestro las observaciones
            Paragraph textoObserv = new Paragraph("Observaciones: " + observ)
                .SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetBold()
                .SetFontSize(10);

            document.Add(espacioBlanco);
            document.Add(textoObserv);

            //Muestro quien lo realizo
            Paragraph textoUsuario = new Paragraph("ENTREGADO POR: " + usuario)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetBold()
                .SetFontSize(8);

            document.Add(espacioBlanco);
            document.Add(textoUsuario);

            //Cierro el documento
            document.Close();

            //Al finalizar, abro el pdf en la ruta donde se guardo
            Process.Start(new ProcessStartInfo { FileName = rutaCompleta, UseShellExecute = true });

            return true;
        }
    }
}
