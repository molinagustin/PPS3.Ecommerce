using Microsoft.JSInterop;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PPS3.Shared.Models;
using MudBlazor.Extensions;
using PPS3.Client.Pages.Reportes;

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

        public string DescargarPDF(IJSRuntime js, int tipoReporte, object entidad, Parametros? parametros = null)
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
                        error = "La cotización no tiene el formato correcto o se encuentra sin detalles.";
                        break;
                    }
                    filename = "cotiz_num_" + pres.NumPresu + "_" + filename;
                    js.InvokeVoidAsync("DescargarPDF", filename, Convert.ToBase64String(PresupuestoReport(pres)));
                    break;

                //Comprobantes
                case 2:
                    var comp = (Comprobante)entidad;
                    if(comp == null || comp.DetallesComprobante == null || comp.DetallesComprobante.Count() < 1)
                    {
                        error = "El comprobante no tiene el formato correcto o se encuentra sin detalles.";
                        break;
                    }
                    filename = "comp_num_" + comp.IdEncab + "_" + filename;
                    js.InvokeVoidAsync("DescargarPDF", filename, Convert.ToBase64String(ComprobanteReport(comp)));
                    break;

                //Recibos
                case 3:
                    var rec = (ReciboListado)entidad;
                    if(rec == null)
                    {
                        error = "El recibo no tiene el formato correcto o se encuentra sin detalles.";
                        break;
                    }
                    filename = "rec_num_" + rec.IdRecibo + "_" + filename;
                    js.InvokeVoidAsync("DescargarPDF", filename, Convert.ToBase64String(ReciboReport(rec)));
                    break;
                
                //REPORTES  
                //Stock
                case 4:
                    var stock = (List<StockProd>)entidad;
                    if(stock == null)
                    {
                        error = "El reporte a imprimir no es correcto.";
                        break;
                    }
                    filename = "ReporteStock" + "_" + filename;
                    js.InvokeVoidAsync("DescargarPDF", filename, Convert.ToBase64String(StockReport(stock)));
                    break;

                //Venta Productos
                case 5:
                    var prods = (List<ProductoFecha>)entidad;
                    if (prods == null || parametros == null)
                    {
                        error = "El reporte a imprimir no es correcto.";
                        break;
                    }
                    filename = "ReporteProductosVendidos" + "_" + filename;
                    js.InvokeVoidAsync("DescargarPDF", filename, Convert.ToBase64String(ProductosFechaReport(prods, parametros)));
                    break;

                //Productos Mas Vendidos
                case 6:
                    var produs = (List<MasProducto>)entidad;
                    if (produs == null || parametros == null)
                    {
                        error = "El reporte a imprimir no es correcto.";
                        break;
                    }
                    filename = "ReporteMasProductosVendidos" + "_" + filename;
                    js.InvokeVoidAsync("DescargarPDF", filename, Convert.ToBase64String(MasProductosReport(produs, parametros)));
                    break;

                //Ingresos
                case 7:
                    var ganancias = (List<Ganancia>)entidad;
                    if (ganancias == null || parametros == null)
                    {
                        error = "El reporte a imprimir no es correcto.";
                        break;
                    }
                    filename = "ReporteIngresos" + "_" + filename;
                    js.InvokeVoidAsync("DescargarPDF", filename, Convert.ToBase64String(GananciasReport(ganancias, parametros)));
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
            var fecha = pres.FechaCrea.Date;
            var fechaValidez = fecha.AddDays(7);
            var usuario = pres.UsuarioCrea;
            var totalPres = pres.DetallePresupuesto.Sum(s => s.SubTotal);
            
            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = Image.GetInstance(rutaImg);
            logo.ScalePercent(24);

            memoryStream = new MemoryStream();

            var pdf = new Document(PageSize.A4, mLeft, mRight, mTop, mBottom);
            pdf.AddTitle("Cotización");
            pdf.AddAuthor("ExpoCeramica");
            pdf.AddCreationDate();
            pdf.AddKeywords("Cotización");
            pdf.AddSubject("Cotización de ExpoCeramica");

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
            var labelFooter = new Chunk("Contacto: Cel. +54 9 2625 656989 - Email. expoceramica@gmail.com", fontStyleFooter);
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

            var titel = new Paragraph("COTIZACIÓN N° " + pres.NumPresu, fontTitleBody);
            titel.SpacingAfter = 26f;
            titel.SpacingBefore = -50f;
            titel.Alignment = Element.ALIGN_CENTER;

            var strLugar = new Paragraph("General Alvear, Mendoza", fontSubTitleBody);            
            var strFechaPres = new Paragraph("Fecha Cotización: " + fecha.ToShortDateString(), fontSubTitleBody);
            var strValidez = new Paragraph("Válido Hasta: " + fechaValidez.ToShortDateString(), fontSubTitleBody);
            var strCliente = new Paragraph("Cliente: " + cliente, fontSubTitleBody);
            var strEntregado = new Paragraph("Usuario Generador: " + usuario, fontSubTitleBody);

            pdf.Add(logo);
            pdf.Add(titel);
            pdf.Add(strLugar);
            pdf.Add(strFechaPres);
            pdf.Add(strValidez);
            pdf.Add(strCliente);
            pdf.Add(strEntregado);


            //Comienzo a construir el detalle en la tabla
            var tabla = new PdfPTable(6);
            tabla.SpacingBefore = 20f;
            tabla.SpacingAfter = 20f;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.WidthPercentage = 100;

            var cellEncTitle = new PdfPCell(new Phrase("Productos Cotizados", fontEncabTabla));
            cellEncTitle.Colspan = 6;
            cellEncTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEncTitle.PaddingBottom = 5;

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

            tabla.AddCell(cellEncTitle);
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

            var strTotal = new Paragraph("TOTAL COTIZADO: " + string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", totalPres), fontSubTitleBody);
            strTotal.Alignment = Element.ALIGN_RIGHT;
            strTotal.SpacingAfter = 20;
            pdf.Add(strTotal);

            var strObserv = new Paragraph("Observaciones: " + observ, fontStyleBody);
            pdf.Add(strObserv);

            pdf.Close();

            return memoryStream.ToArray();
        }
    
        private byte[] ComprobanteReport(Comprobante comp)
        {
            //Datos para el comprobante
            var tipoVta = (comp.TipoVta == 1) ? "Con Cuenta Corriente" : "Sin Cuenta Corriente";
            var obs = (!string.IsNullOrEmpty(comp.Observaciones)) ? comp.Observaciones : "NINGUNA";

            memoryStream = new MemoryStream();
            
            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = Image.GetInstance(rutaImg);
            logo.ScalePercent(24);

            var pdf = new Document(PageSize.A4, mLeft, mRight, mTop, mBottom);
            pdf.AddTitle("Comprobante");
            pdf.AddAuthor("ExpoCeramica");
            pdf.AddCreationDate();
            pdf.AddKeywords("Comprobante");
            pdf.AddSubject("Comprobante de ExpoCeramica");

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
            var labelFooter = new Chunk("Contacto: Cel. +54 9 2625 656989 - Email. expoceramica@gmail.com", fontStyleFooter);
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

            var titel = new Paragraph("COMPROBANTE N° " + comp.IdEncab, fontTitleBody);
            titel.SpacingAfter = 26f;
            titel.SpacingBefore = -50f;
            titel.Alignment = Element.ALIGN_CENTER;

            var strLugar = new Paragraph("General Alvear, Mendoza", fontSubTitleBody);
            var strOrden = new Paragraph("Orden Compra N°: " + comp.Carro, fontSubTitleBody);
            var strFechaComp = new Paragraph("Fecha Comprobante: " + comp.FechaComp.ToShortDateString(), fontSubTitleBody);
            var strCliente = new Paragraph("Cliente: " + comp.Cliente, fontSubTitleBody);
            var strTipoVta = new Paragraph("Tipo Venta: " + tipoVta, fontSubTitleBody);
            var strTipoComp = new Paragraph("Documento Fiscal Asociado: " + comp.TipoComp,fontSubTitleBody);
            var strGenerado = new Paragraph("Generado Por: " + comp.UsuarioCrea, fontSubTitleBody);

            pdf.Add(logo);
            pdf.Add(titel);
            pdf.Add(strLugar);
            pdf.Add(strOrden);
            pdf.Add(strFechaComp);
            pdf.Add(strCliente);
            pdf.Add(strTipoVta);
            pdf.Add(strTipoComp);
            pdf.Add(strGenerado);

            //Comienzo a construir el detalle en la tabla
            var tabla = new PdfPTable(6);
            tabla.SpacingBefore = 20f;
            tabla.SpacingAfter = 30f;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.WidthPercentage = 100;

            var cellEncTitle = new PdfPCell(new Phrase("Productos Solicitados", fontEncabTabla));
            cellEncTitle.Colspan = 6;
            cellEncTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEncTitle.PaddingBottom = 5;

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

            tabla.AddCell(cellEncTitle);
            tabla.AddCell(cellEnc1);
            tabla.AddCell(cellEnc2);
            tabla.AddCell(cellEnc3);
            tabla.AddCell(cellEnc4);
            tabla.AddCell(cellEnc5);
            tabla.AddCell(cellEnc6);

            foreach (var det in comp.DetallesComprobante)
            {
                var cellDet1 = new PdfPCell(new Phrase(det.NombreProd, fontCuerpoTabla));
                cellDet1.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet1.PaddingBottom = 7;
                cellDet1.PaddingTop = 3;
                cellDet1.PaddingLeft = 2;

                var cellDet2 = new PdfPCell(new Phrase(det.CantidadProdC.ToString() + " " + det.DescripcionUnidad, fontCuerpoTabla));
                cellDet2.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet2.PaddingBottom = 7;
                cellDet2.PaddingTop = 3;
                cellDet2.PaddingLeft = 2;

                var cellDet3 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.PrecioUnitario), fontCuerpoTabla));
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

                var cellDet6 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.Total), fontCuerpoTabla));
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

            var strTotal = new Paragraph("TOTAL A PAGAR: " + string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", comp.ImporteFinal), fontSubTitleBody);
            strTotal.Alignment = Element.ALIGN_RIGHT;
            strTotal.SpacingAfter = 20;
            pdf.Add(strTotal);

            var strObserv = new Paragraph("Observaciones: " + obs, fontStyleBody);
            pdf.Add(strObserv);

            pdf.Close();

            return memoryStream.ToArray();
        }

        private byte[] ReciboReport(ReciboListado rec)
        {
            //Datos para el recibo
            var formaPago = (!string.IsNullOrEmpty(rec.NumTarjeta)) ? rec.FormaP + " (Terminación " + rec.NumTarjeta + ")" : rec.FormaP;
            var obs = (!string.IsNullOrEmpty(rec.Observaciones)) ? rec.Observaciones : "NINGUNA";

            memoryStream= new MemoryStream();

            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = Image.GetInstance(rutaImg);
            logo.ScalePercent(24);

            var pdf = new Document(PageSize.A4, mLeft, mRight, mTop, mBottom);
            pdf.AddTitle("Recibo");
            pdf.AddAuthor("ExpoCeramica");
            pdf.AddCreationDate();
            pdf.AddKeywords("Recibo");
            pdf.AddSubject("Recibo de ExpoCeramica");

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
            var labelFooter = new Chunk("Contacto: Cel. +54 9 2625 656989 - Email. expoceramica@gmail.com", fontStyleFooter);
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

            var titel = new Paragraph("RECIBO N° " + rec.IdRecibo, fontTitleBody);
            titel.SpacingAfter = 26f;
            titel.SpacingBefore = -50f;
            titel.Alignment = Element.ALIGN_CENTER;

            var strLugar = new Paragraph("General Alvear, Mendoza", fontSubTitleBody);            
            var strFechaRec = new Paragraph("Fecha Recibo: " + rec.FechaRecibo.ToShortDateString(), fontSubTitleBody);
            var strCliente = new Paragraph("Cliente: " + rec.Cliente, fontSubTitleBody);
            var strFormaPago = new Paragraph("Forma Pago: " + formaPago, fontSubTitleBody);
            var strGenerado = new Paragraph("Generado Por: " + rec.UsuarioCrea, fontSubTitleBody);

            pdf.Add(logo);
            pdf.Add(titel);
            pdf.Add(strLugar);
            pdf.Add(strFechaRec);            
            pdf.Add(strCliente);
            pdf.Add(strFormaPago);
            pdf.Add(strGenerado);

            //Comienzo a construir el detalle en la tabla
            var tabla = new PdfPTable(7);
            tabla.SpacingBefore = 20f;
            tabla.SpacingAfter = 30f;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.WidthPercentage = 100;

            var cellEncTitle = new PdfPCell(new Phrase("Comprobantes del Recibo", fontEncabTabla));
            cellEncTitle.Colspan = 7;
            cellEncTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEncTitle.PaddingBottom = 5;

            var cellEnc1 = new PdfPCell(new Phrase("N° Comp.", fontEncabTabla));
            cellEnc1.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc1.PaddingBottom = 5;

            var cellEnc7 = new PdfPCell(new Phrase("Fecha Comp.", fontEncabTabla));
            cellEnc7.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc7.PaddingBottom = 5;

            var cellEnc2 = new PdfPCell(new Phrase("N° Orden Comp.", fontEncabTabla));
            cellEnc2.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc2.PaddingBottom = 5;

            var cellEnc3 = new PdfPCell(new Phrase("Importe Comp.", fontEncabTabla));
            cellEnc3.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc3.PaddingBottom = 5;

            var cellEnc4 = new PdfPCell(new Phrase("Importe Abonado", fontEncabTabla));
            cellEnc4.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc4.PaddingBottom = 5;

            var cellEnc5 = new PdfPCell(new Phrase("Resto Al " + DateTime.Today.ToShortDateString(), fontEncabTabla));
            cellEnc5.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc5.PaddingBottom = 5;

            var cellEnc6 = new PdfPCell(new Phrase("Pagado", fontEncabTabla));
            cellEnc6.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc6.PaddingBottom = 5;

            tabla.AddCell(cellEncTitle);
            tabla.AddCell(cellEnc1);
            tabla.AddCell(cellEnc7);
            tabla.AddCell(cellEnc2);
            tabla.AddCell(cellEnc3);
            tabla.AddCell(cellEnc4);
            tabla.AddCell(cellEnc5);
            tabla.AddCell(cellEnc6);

            foreach (var det in rec.DetallesRecibo)
            {
                var cellDet1 = new PdfPCell(new Phrase(det.IdComprobante.ToString(), fontCuerpoTabla));
                cellDet1.HorizontalAlignment = Element.ALIGN_CENTER;
                cellDet1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet1.PaddingBottom = 7;
                cellDet1.PaddingTop = 3;
                cellDet1.PaddingLeft = 2;

                var cellDet7 = new PdfPCell(new Phrase(det.FechaComp.ToShortDateString(), fontCuerpoTabla));
                cellDet7.HorizontalAlignment = Element.ALIGN_CENTER;
                cellDet7.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet7.PaddingBottom = 7;
                cellDet7.PaddingTop = 3;
                cellDet7.PaddingLeft = 2;

                var cellDet2 = new PdfPCell(new Phrase(det.Carro.ToString(), fontCuerpoTabla));
                cellDet2.HorizontalAlignment = Element.ALIGN_CENTER;
                cellDet2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet2.PaddingBottom = 7;
                cellDet2.PaddingTop = 3;
                cellDet2.PaddingLeft = 2;

                var cellDet3 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.ImporteFinal), fontCuerpoTabla));
                cellDet3.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellDet3.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet3.PaddingBottom = 7;
                cellDet3.PaddingTop = 3;

                var cellDet4 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.Importe), fontCuerpoTabla));
                cellDet4.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellDet4.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet4.PaddingBottom = 7;
                cellDet4.PaddingTop = 3;

                var cellDet5 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.SaldoRestante), fontCuerpoTabla));
                cellDet5.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellDet5.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet5.PaddingBottom = 7;
                cellDet4.PaddingTop = 3;

                var cellDet6 = new PdfPCell(new Phrase((det.Pagado) ? "SI" : "NO" , fontCuerpoTabla));
                cellDet6.HorizontalAlignment = Element.ALIGN_CENTER;
                cellDet6.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet6.PaddingBottom = 7;
                cellDet4.PaddingTop = 3;

                tabla.AddCell(cellDet1);
                tabla.AddCell(cellDet7);
                tabla.AddCell(cellDet2);
                tabla.AddCell(cellDet3);
                tabla.AddCell(cellDet4);
                tabla.AddCell(cellDet5);
                tabla.AddCell(cellDet6);
            }

            pdf.Add(tabla);

            var strTotal = new Paragraph("TOTAL ABONADO: " + string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", rec.ImporteTotal), fontSubTitleBody);
            strTotal.Alignment = Element.ALIGN_RIGHT;
            strTotal.SpacingAfter = 20;
            pdf.Add(strTotal);

            var strObserv = new Paragraph("Observaciones: " + obs, fontStyleBody);
            pdf.Add(strObserv);

            pdf.Close();

            return memoryStream.ToArray();
        }
    
        private byte[] StockReport(List<StockProd> stock)
        {
            memoryStream = new MemoryStream();

            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = Image.GetInstance(rutaImg);
            logo.ScalePercent(24);

            var pdf = new Document(PageSize.A4, mLeft, mRight, mTop, mBottom);
            pdf.AddTitle("Reporte");
            pdf.AddAuthor("ExpoCeramica");
            pdf.AddCreationDate();
            pdf.AddKeywords("Reporte");
            pdf.AddSubject("Reporte de ExpoCeramica");

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
            var labelFooter = new Chunk("Contacto: Cel. +54 9 2625 656989 - Email. expoceramica@gmail.com", fontStyleFooter);
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

            var titel = new Paragraph("REPORTE STOCK DE PRODUCTOS");
            titel.SpacingAfter = 26f;
            titel.SpacingBefore = -50f;
            titel.Alignment = Element.ALIGN_CENTER;

            var strLugar = new Paragraph("General Alvear, Mendoza", fontSubTitleBody);
            var strFecha = new Paragraph("Fecha: " + DateTime.Today.ToShortDateString(), fontSubTitleBody);

            pdf.Add(logo);
            pdf.Add(titel);
            pdf.Add(strLugar);
            pdf.Add(strFecha);

            //Comienzo a construir el detalle en la tabla
            var tabla = new PdfPTable(2);
            tabla.SpacingBefore = 20f;
            tabla.SpacingAfter = 30f;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;

            var cellEncTitle = new PdfPCell(new Phrase("STOCK EXISTENTE", fontEncabTabla));
            cellEncTitle.Colspan = 2;
            cellEncTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEncTitle.PaddingBottom = 5;

            var cellEnc1 = new PdfPCell(new Phrase("Producto", fontEncabTabla));
            cellEnc1.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc1.PaddingBottom = 5;

            var cellEnc2 = new PdfPCell(new Phrase("Stock", fontEncabTabla));
            cellEnc2.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc2.PaddingBottom = 5;

            tabla.AddCell(cellEncTitle);
            tabla.AddCell(cellEnc1);
            tabla.AddCell(cellEnc2);

            foreach (var sto in stock)
            {
                var cellDet1 = new PdfPCell(new Phrase(sto.NombreProd, fontCuerpoTabla));
                cellDet1.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet1.PaddingBottom = 7;
                cellDet1.PaddingTop = 3;
                cellDet1.PaddingLeft = 2;

                var cellDet2 = new PdfPCell(new Phrase(sto.StockExistencia.ToString() + " " + sto.DescripcionUnidad, fontCuerpoTabla));
                cellDet2.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet2.PaddingBottom = 7;
                cellDet2.PaddingTop = 3;
                cellDet2.PaddingLeft = 2;

                tabla.AddCell(cellDet1);
                tabla.AddCell(cellDet2);
            }

            pdf.Add(tabla);

            pdf.Close();

            return memoryStream.ToArray();
        }

        private byte[] ProductosFechaReport(List<ProductoFecha> prods, Parametros parametros)
        {
            memoryStream = new MemoryStream();

            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = Image.GetInstance(rutaImg);
            logo.ScalePercent(24);

            var pdf = new Document(PageSize.A4, mLeft, mRight, mTop, mBottom);
            pdf.AddTitle("Reporte");
            pdf.AddAuthor("ExpoCeramica");
            pdf.AddCreationDate();
            pdf.AddKeywords("Reporte");
            pdf.AddSubject("Reporte de ExpoCeramica");

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
            var labelFooter = new Chunk("Contacto: Cel. +54 9 2625 656989 - Email. expoceramica@gmail.com", fontStyleFooter);
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

            var titel = new Paragraph("REPORTE PRODUCTOS VENDIDOS");
            titel.SpacingAfter = 26f;
            titel.SpacingBefore = -50f;
            titel.Alignment = Element.ALIGN_CENTER;

            var strLugar = new Paragraph("General Alvear, Mendoza", fontSubTitleBody);
            var strFecha = new Paragraph("Fecha: " + DateTime.Today.ToShortDateString(), fontSubTitleBody);

            pdf.Add(logo);
            pdf.Add(titel);
            pdf.Add(strLugar);
            pdf.Add(strFecha);

            if(parametros.CompPagos) pdf.Add(new Paragraph("Solo En Comprobantes Pagados", fontSubTitleBody));
            if(parametros.CC)
            {
                if(parametros.TipoVta == 1) pdf.Add(new Paragraph("Con Venta a Cuenta Corriente", fontSubTitleBody));
                else pdf.Add(new Paragraph("Sin Venta a Cuenta Corriente", fontSubTitleBody));
            }
            if (parametros.RangoFechas)
            {
                pdf.Add(new Paragraph("Rango Consulta: " + parametros.FechaDesde.ToShortDateString() + " -> " + parametros.FechaHasta.ToShortDateString(), fontSubTitleBody));
            }

            //Comienzo a construir el detalle en la tabla
            var tabla = new PdfPTable(3);
            tabla.SpacingBefore = 20f;
            tabla.SpacingAfter = 30f;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;

            var cellEncTitle = new PdfPCell(new Phrase("PRODUCTOS VENDIDOS", fontEncabTabla));
            cellEncTitle.Colspan = 3;
            cellEncTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEncTitle.PaddingBottom = 5;

            var cellEnc0 = new PdfPCell(new Phrase("Fecha", fontEncabTabla));
            cellEnc0.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc0.PaddingBottom = 5;

            var cellEnc1 = new PdfPCell(new Phrase("Producto", fontEncabTabla));
            cellEnc1.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc1.PaddingBottom = 5;

            var cellEnc2 = new PdfPCell(new Phrase("Cantidad", fontEncabTabla));
            cellEnc2.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc2.PaddingBottom = 5;

            tabla.AddCell(cellEncTitle);
            tabla.AddCell(cellEnc0);
            tabla.AddCell(cellEnc1);
            tabla.AddCell(cellEnc2);

            foreach (var p in prods)
            {
                var cellDet0 = new PdfPCell(new Phrase(p.FechaComp.ToShortDateString(),fontCuerpoTabla));
                cellDet0.HorizontalAlignment = Element.ALIGN_CENTER;
                cellDet0.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet0.PaddingBottom = 7;
                cellDet0.PaddingTop = 3;
                cellDet0.PaddingLeft = 2;

                var cellDet1 = new PdfPCell(new Phrase(p.NombreProd, fontCuerpoTabla));
                cellDet1.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet1.PaddingBottom = 7;
                cellDet1.PaddingTop = 3;
                cellDet1.PaddingLeft = 2;

                var cellDet2 = new PdfPCell(new Phrase(p.Cantidad.ToString() + " " + p.DescripcionUnidad, fontCuerpoTabla));
                cellDet2.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet2.PaddingBottom = 7;
                cellDet2.PaddingTop = 3;
                cellDet2.PaddingLeft = 2;

                tabla.AddCell(cellDet0);
                tabla.AddCell(cellDet1);
                tabla.AddCell(cellDet2);
            }

            pdf.Add(tabla);

            pdf.Close();

            return memoryStream.ToArray();
        }

        private byte[] MasProductosReport(List<MasProducto> prods, Parametros parametros)
        {
            memoryStream = new MemoryStream();

            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = Image.GetInstance(rutaImg);
            logo.ScalePercent(24);

            var pdf = new Document(PageSize.A4, mLeft, mRight, mTop, mBottom);
            pdf.AddTitle("Reporte");
            pdf.AddAuthor("ExpoCeramica");
            pdf.AddCreationDate();
            pdf.AddKeywords("Reporte");
            pdf.AddSubject("Reporte de ExpoCeramica");

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
            var labelFooter = new Chunk("Contacto: Cel. +54 9 2625 656989 - Email. expoceramica@gmail.com", fontStyleFooter);
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

            var titel = new Paragraph("REPORTE PRODUCTOS MAS VENDIDOS");
            titel.SpacingAfter = 26f;
            titel.SpacingBefore = -50f;
            titel.Alignment = Element.ALIGN_CENTER;

            var strLugar = new Paragraph("General Alvear, Mendoza", fontSubTitleBody);
            var strFecha = new Paragraph("Fecha: " + DateTime.Today.ToShortDateString(), fontSubTitleBody);

            pdf.Add(logo);
            pdf.Add(titel);
            pdf.Add(strLugar);
            pdf.Add(strFecha);
                        
            if (parametros.CC)
            {
                if (parametros.TipoVta == 1) pdf.Add(new Paragraph("Con Venta a Cuenta Corriente", fontSubTitleBody));
                else pdf.Add(new Paragraph("Sin Venta a Cuenta Corriente", fontSubTitleBody));
            }
            if (parametros.RangoFechas)
            {
                pdf.Add(new Paragraph("Rango Consulta: " + parametros.FechaDesde.ToShortDateString() + " -> " + parametros.FechaHasta.ToShortDateString(), fontSubTitleBody));
            }

            //Comienzo a construir el detalle en la tabla
            var tabla = new PdfPTable(2);
            tabla.SpacingBefore = 20f;
            tabla.SpacingAfter = 30f;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;

            var cellEncTitle = new PdfPCell(new Phrase("PRODUCTOS VENDIDOS", fontEncabTabla));
            cellEncTitle.Colspan = 2;
            cellEncTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEncTitle.PaddingBottom = 5;

            var cellEnc1 = new PdfPCell(new Phrase("Producto", fontEncabTabla));
            cellEnc1.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc1.PaddingBottom = 5;

            var cellEnc2 = new PdfPCell(new Phrase("Cantidad", fontEncabTabla));
            cellEnc2.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc2.PaddingBottom = 5;

            tabla.AddCell(cellEncTitle);
            tabla.AddCell(cellEnc1);
            tabla.AddCell(cellEnc2);

            foreach (var p in prods)
            {
                var cellDet1 = new PdfPCell(new Phrase(p.NombreProd, fontCuerpoTabla));
                cellDet1.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet1.PaddingBottom = 7;
                cellDet1.PaddingTop = 3;
                cellDet1.PaddingLeft = 2;

                var cellDet2 = new PdfPCell(new Phrase(p.Cantidad.ToString() + " " + p.DescripcionUnidad, fontCuerpoTabla));
                cellDet2.HorizontalAlignment = Element.ALIGN_LEFT;
                cellDet2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet2.PaddingBottom = 7;
                cellDet2.PaddingTop = 3;
                cellDet2.PaddingLeft = 2;

                tabla.AddCell(cellDet1);
                tabla.AddCell(cellDet2);
            }

            pdf.Add(tabla);

            pdf.Close();

            return memoryStream.ToArray();
        }

        private byte[] GananciasReport(List<Ganancia> ganancias, Parametros parametros)
        {
            memoryStream = new MemoryStream();

            var rutaImg = _env.WebRootFileProvider.GetFileInfo("images/expo_ceramicas_logo.png").PhysicalPath;
            Image logo = Image.GetInstance(rutaImg);
            logo.ScalePercent(24);

            var pdf = new Document(PageSize.A4, mLeft, mRight, mTop, mBottom);
            pdf.AddTitle("Reporte");
            pdf.AddAuthor("ExpoCeramica");
            pdf.AddCreationDate();
            pdf.AddKeywords("Reporte");
            pdf.AddSubject("Reporte de ExpoCeramica");

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
            var labelFooter = new Chunk("Contacto: Cel. +54 9 2625 656989 - Email. expoceramica@gmail.com", fontStyleFooter);
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

            var titel = new Paragraph("REPORTE DE INGRESOS");
            titel.SpacingAfter = 26f;
            titel.SpacingBefore = -50f;
            titel.Alignment = Element.ALIGN_CENTER;

            var strLugar = new Paragraph("General Alvear, Mendoza", fontSubTitleBody);
            var strFecha = new Paragraph("Fecha: " + DateTime.Today.ToShortDateString(), fontSubTitleBody);

            pdf.Add(logo);
            pdf.Add(titel);
            pdf.Add(strLugar);
            pdf.Add(strFecha);

            if (parametros.CompPagos) pdf.Add(new Paragraph("Solo En Comprobantes Pagados", fontSubTitleBody));

            if (parametros.CC)
            {
                if (parametros.TipoVta == 1) pdf.Add(new Paragraph("Con Venta a Cuenta Corriente", fontSubTitleBody));
                else pdf.Add(new Paragraph("Sin Venta a Cuenta Corriente", fontSubTitleBody));
            }

            if (parametros.RangoFechas)
            {
                pdf.Add(new Paragraph("Rango Consulta: " + parametros.FechaDesde.ToShortDateString() + " -> " + parametros.FechaHasta.ToShortDateString(), fontSubTitleBody));
            }

            if(parametros.UsarBonif)
            {
                if (parametros.ConBonif) pdf.Add(new Paragraph("Comprobantes Con Bonificación", fontSubTitleBody));
                else pdf.Add(new Paragraph("Comprobantes Sin Bonificación", fontSubTitleBody));
            }

            if(parametros.UsarFP) pdf.Add(new Paragraph("Forma Pago: " + parametros.FormaPagoStr, fontSubTitleBody));

            //Comienzo a construir el detalle en la tabla
            var tabla = new PdfPTable(2);
            tabla.SpacingBefore = 20f;
            tabla.SpacingAfter = 30f;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;

            var cellEncTitle = new PdfPCell(new Phrase("INGRESOS", fontEncabTabla));
            cellEncTitle.Colspan = 2;
            cellEncTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEncTitle.PaddingBottom = 5;

            var cellEnc1 = new PdfPCell(new Phrase("Día", fontEncabTabla));
            cellEnc1.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc1.PaddingBottom = 5;

            var cellEnc2 = new PdfPCell(new Phrase("Importe", fontEncabTabla));
            cellEnc2.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEnc2.PaddingBottom = 5;

            tabla.AddCell(cellEncTitle);
            tabla.AddCell(cellEnc1);
            tabla.AddCell(cellEnc2);

            foreach (var g in ganancias)
            {
                var cellDet1 = new PdfPCell(new Phrase(g.FechaRecibo.ToShortDateString(), fontCuerpoTabla));
                cellDet1.HorizontalAlignment = Element.ALIGN_CENTER;
                cellDet1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet1.PaddingBottom = 7;
                cellDet1.PaddingTop = 3;
                cellDet1.PaddingLeft = 2;

                var cellDet2 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", g.TotalDia), fontCuerpoTabla));
                cellDet2.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellDet2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellDet2.PaddingBottom = 7;
                cellDet2.PaddingTop = 3;
                cellDet2.PaddingLeft = 2;

                tabla.AddCell(cellDet1);
                tabla.AddCell(cellDet2);
            }

            var cellDetTot1 = new PdfPCell(new Phrase("TOTAL:", fontCuerpoTabla));
            cellDetTot1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellDetTot1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellDetTot1.PaddingBottom = 7;
            cellDetTot1.PaddingTop = 3;
            cellDetTot1.PaddingLeft = 2;

            var cellDetTot2 = new PdfPCell(new Phrase(string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", ganancias.Sum(x=>x.TotalDia)), fontCuerpoTabla));
            cellDetTot2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellDetTot2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellDetTot2.PaddingBottom = 7;
            cellDetTot2.PaddingTop = 3;
            cellDetTot2.PaddingLeft = 2;

            tabla.AddCell(cellDetTot1);
            tabla.AddCell(cellDetTot2);

            pdf.Add(tabla);

            pdf.Close();

            return memoryStream.ToArray();
        }
    }
}
