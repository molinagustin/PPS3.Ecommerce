﻿using Microsoft.JSInterop;
using iTextSharp.text;
using iTextSharp.text.pdf;

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

        /*public bool GenerarPdfPresupuesto(Presupuesto presBase)
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
        }*/

        public string DescargarPDF(IJSRuntime js, int tipoReporte, object entidad)
        {
            var error = "";
            var filename = GenerarCadenaAleatoria() + ".pdf";

            switch (tipoReporte)
            {
                //Presupuestos
                case 1:
                    var pres = (Presupuesto)entidad;
                    if (pres == null || pres.DetallePresupuesto == null || pres.DetallePresupuesto.Count() < 1)
                    {
                        error = "El presupuesto no tiene el formato correcto o se encuentra sin detalles.";
                        break;
                    }
                    filename = "presupuesto_num_" + pres.NumPresu + "_" + filename;
                    js.InvokeVoidAsync("DescargarPDF", filename, Convert.ToBase64String(PresupuestoReport(pres)));
                    break;

                //Comprobantes
                case 2:
                    break;

                //Recibos
                case 3:
                    break;
            }
            return error;            
        }

        //Configuraciones para el PDF
        MemoryStream memoryStream;
        float mLeft = (1.5f / 2.54f) * 72; //Pasarlo a DPI
        float mRight = (1.5f / 2.54f) * 72;
        float mTop = (1.0f / 2.54f) * 72;
        float mBottom = (1.0f / 2.54f) * 72;

        Font fontStyleEncabezado = FontFactory.GetFont("Helvetica", 20, BaseColor.White);
        Font fontStyleFooter = FontFactory.GetFont("Helvetica", 12, BaseColor.White);
        Font fontStyleBody = FontFactory.GetFont("Tahoma", 12f, Font.NORMAL);
        Font fontTitleBody = new Font(Font.HELVETICA, 18, Font.BOLD);
        Font fontSubTitleBody = new Font(Font.HELVETICA, 12, Font.BOLD);
        Font fontEncabTabla = new Font(Font.HELVETICA, 12, Font.BOLD);
        Font fontCuerpoTabla = new Font(Font.HELVETICA, 11, Font.NORMAL);

        //Reporte de iTextSharp
        private byte[] PresupuestoReport(Presupuesto pres)
        {
            //Datos para el presupuesto
            var numPres = pres.NumPresu;
            var cliente = pres.Cliente;
            var observ = "NINGUNA";
            if (!string.IsNullOrEmpty(pres.Observaciones))
                observ = pres.Observaciones;
            var fecha = pres.FechaCrea;
            var usuario = pres.UsuarioCrea;
            var totalPres = pres.DetallePresupuesto.Sum(s => s.SubTotal);

            Paragraph espacioBlanco = new Paragraph("\n");
            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = Image.GetInstance(rutaImg);
            logo.ScalePercent(24);

            memoryStream = new MemoryStream();

            var pdf = new Document(PageSize.A4, mLeft, mRight, mTop, mBottom);
            pdf.AddTitle("Presupuesto");
            pdf.AddAuthor("ExpoCeramica");
            pdf.AddCreationDate();
            pdf.AddKeywords("Presupuesto");
            pdf.AddSubject("Presupuesto de ExpoCeramica");

            //Si bien no se usa el objeto, es necesario el metodo GetInstance para comenzar la escritura interna del pdf
            var writer = PdfWriter.GetInstance(pdf, memoryStream);

            //Encabezado            
            var labelHeader = new Chunk("ExpoCeramica", fontStyleEncabezado);
            var header = new HeaderFooter(new Phrase(labelHeader), false)
            {
                BackgroundColor = new BaseColor(89, 74, 226),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };

            //Pie de Pagina
            var labelFooter = new Chunk("Contacto: Cel. +54 9 2266 656989 - Email. expoceramica@gmail.com", fontStyleFooter);
            var footer = new HeaderFooter(new Phrase(labelFooter), false)
            {
                BackgroundColor = new BaseColor(89, 74, 226),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };

            pdf.Header = header;
            pdf.Footer = footer;

            //Cuerpo
            pdf.Open();

            var titel = new Paragraph("PRESUPUESTO N° " + pres.NumPresu, fontTitleBody);
            titel.SpacingAfter = 26f;
            titel.SpacingBefore = -50f;
            titel.Alignment = Element.ALIGN_CENTER;

            var strLugar = new Paragraph("General Alvear, Mendoza", fontSubTitleBody);
            var strFechaHoy = new Paragraph("Fecha Hoy: " + DateTime.Now, fontSubTitleBody);
            var strFechaPres = new Paragraph("Fecha Presupuesto: " + fecha, fontSubTitleBody);
            var strCliente = new Paragraph("Cliente: " + cliente, fontSubTitleBody);
            var strEntregado = new Paragraph("Usuario Generador: " + usuario, fontSubTitleBody);

            pdf.Add(logo);
            pdf.Add(titel);
            pdf.Add(strLugar);
            pdf.Add(strFechaHoy);
            pdf.Add(strFechaPres);
            pdf.Add(strCliente);
            pdf.Add(strEntregado);


            //Comienzo a construir el detalle en la tabla
            var tabla = new PdfPTable(6);
            tabla.SpacingBefore = 20f;
            tabla.SpacingAfter = 20f;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.WidthPercentage = 100;

            var cellEnc1 = new PdfPCell(new Phrase("Producto", fontEncabTabla));
            cellEnc1.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc1.PaddingBottom = 5;

            var cellEnc2 = new PdfPCell(new Phrase("Cantidad", fontEncabTabla));
            cellEnc2.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc2.PaddingBottom = 5;

            var cellEnc3 = new PdfPCell(new Phrase("Precio", fontEncabTabla));
            cellEnc3.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc3.PaddingBottom = 5;

            var cellEnc4 = new PdfPCell(new Phrase("Bonif.", fontEncabTabla));
            cellEnc4.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc4.PaddingBottom = 5;

            var cellEnc5 = new PdfPCell(new Phrase("Bonif. Total", fontEncabTabla));
            cellEnc5.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc5.PaddingBottom = 5;

            var cellEnc6 = new PdfPCell(new Phrase("Sub Total", fontEncabTabla));
            cellEnc6.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc6.PaddingBottom = 5;

            tabla.AddCell(cellEnc1);
            tabla.AddCell(cellEnc2);
            tabla.AddCell(cellEnc3);
            tabla.AddCell(cellEnc4);
            tabla.AddCell(cellEnc5);
            tabla.AddCell(cellEnc6);

            foreach (var det in pres.DetallePresupuesto)
            {
                var cellDet1 = new PdfPCell(new Phrase(det.NombreProducto, fontCuerpoTabla));
                cellDet1.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet1.PaddingBottom = 7;
                cellDet1.PaddingTop = 3;
                cellDet1.PaddingLeft = 2;

                var cellDet2 = new PdfPCell(new Phrase(det.Cantidad.ToString() + " " + det.DescripcionUnidad, fontCuerpoTabla));
                cellDet2.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet2.PaddingBottom = 7;
                cellDet2.PaddingTop = 3;
                cellDet2.PaddingLeft = 2;

                var cellDet3 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.PrecioUnit), fontCuerpoTabla));
                cellDet3.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellDet3.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet3.PaddingBottom = 7;
                cellDet3.PaddingTop = 3;

                var cellDet4 = new PdfPCell(new Phrase(det.Bonificacion.ToString() + "%", fontCuerpoTabla));
                cellDet4.HorizontalAlignment = Element.ALIGN_CENTER;
                cellDet4.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet4.PaddingBottom = 7;
                cellDet4.PaddingTop = 3;

                var cellDet5 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.BonificacionTotal), fontCuerpoTabla));
                cellDet5.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellDet5.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet5.PaddingBottom = 7;
                cellDet4.PaddingTop = 3;

                var cellDet6 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.SubTotal), fontCuerpoTabla));
                cellDet6.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellDet6.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet6.PaddingBottom = 7;
                cellDet4.PaddingTop = 3;

                tabla.AddCell(cellDet1);
                tabla.AddCell(cellDet2);
                tabla.AddCell(cellDet3);
                tabla.AddCell(cellDet4);
                tabla.AddCell(cellDet5);
                tabla.AddCell(cellDet6);
            }

            pdf.Add(tabla);

            var strTotal = new Paragraph("TOTAL: " + string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", totalPres), fontSubTitleBody);
            strTotal.Alignment = Element.ALIGN_RIGHT;
            strTotal.SpacingAfter = 20;
            pdf.Add(strTotal);

            var strObserv = new Paragraph("Observaciones: " + observ, fontStyleBody);
            pdf.Add(strObserv);

            pdf.Close();

            return memoryStream.ToArray();
        }
    }
}
