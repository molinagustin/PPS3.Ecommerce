using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;

namespace PPS3.Server.ExternalServices.ServEmail
{
    public class ServEmail : IServEmail
    {
        private readonly IConfiguration _config;

        public ServEmail(IConfiguration config)
        {
            _config = config;
        }

        public bool EmailContacto(EmailBasico datosEmail)
        {
            try
            {
                // Creo el mensaje
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(datosEmail.Remitente));
                email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                email.Subject = datosEmail.Tema;
                email.Body = new TextPart(TextFormat.Plain) { Text = datosEmail.Mensaje };

                // Configuro el envio del correo
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch
            {
                return false;
            }            
        }

        public bool EmailModificacionOrden(OrdenesCompraListado orden)
        {
            try
            {
                // Creo el mensaje
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(orden.Email));
                email.Subject = "ExpoCeramica: Actualización de Orden Compra nº " + orden.IdCarro.ToString();

                var contenido = new StringBuilder();
                contenido.AppendLine($"<h3>La orden de compra nº {orden.IdCarro.ToString()} ha sido actualizada.</h4>");
                contenido.AppendLine("<ul>");
                contenido.AppendLine($"<li>Estado: <b>{orden.Estado}</b></li>");
                contenido.AppendLine($"<li>Fecha Orden: <b>{orden.FechaOrden}</b></li>");
                if(orden.FechaEntrega != null)
                    contenido.AppendLine($"<li>Fecha Entrega: <b>{orden.FechaEntrega.Value.ToShortDateString()}</b></li>");
                contenido.AppendLine($"<li>Última Modif.: <b>{orden.FechaUltModif}</b></li>");
                contenido.AppendLine($"<li>Total Orden: <b>{string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", orden.Total)}</b></li>");

                if(orden.Pagado)
                {
                    contenido.AppendLine($"<li>Pagado: <b>Si</b></li>");
                    if(orden.FechaPago != null)
                        contenido.AppendLine($"<li>Fecha Pago: <b>{orden.FechaPago.Value.ToShortDateString()}</b></li>");
                }
                else contenido.AppendLine($"<li>Pagado: <b>No</b></li>");

                if(!string.IsNullOrEmpty(orden.Observaciones))
                    contenido.AppendLine($"<li>Observaciones: <b>{orden.Observaciones}</b></li>");
                else contenido.AppendLine($"<li>Observaciones: <b>-----</b></li>");
                contenido.AppendLine("</ul>");

                contenido.AppendLine("<table>");
                contenido.AppendLine("<thead>");
                contenido.AppendLine("<tr><th colspan=\"3\">Productos Solicitados</th></tr>");
                contenido.AppendLine("<tr>");
                contenido.AppendLine("<th>Producto</th>");
                contenido.AppendLine("<th>Cantidad</th>");
                contenido.AppendLine("<th>Sub Total</th>");
                contenido.AppendLine("</tr>");
                contenido.AppendLine("</thead>");
                contenido.AppendLine("<tbody>");
                if(orden.DetallesCarro == null || orden.DetallesCarro.Count() == 0)
                    contenido.AppendLine("<tr><th colspan=\"3\"><b>Sin Productos</b></th></tr>");
                else
                {
                    foreach (var det in orden.DetallesCarro)
                    {
                        contenido.AppendLine("<tr>");
                        contenido.AppendLine($"<td>{det.NombreProducto}</td>");
                        contenido.AppendLine($"<td>{det.Cantidad} {det.DescripcionUnidad}</td>");
                        contenido.AppendLine($"<td>{string.Format(new System.Globalization.CultureInfo("es-AR"), "{0:C}", det.SubTotal)}</td>");
                        contenido.AppendLine("</tr>");
                    }
                }
                contenido.AppendLine("</tbody></table>");

                contenido.AppendLine("<br><br>");
                contenido.AppendLine($"<h6>Ante cualquier duda, puedes ponerte en contacto con nosotros enviandonos un <a href='{orden.UrlString}contacto'>Email</a> ó hablar con nuestro <a href='https://api.whatsapp.com/send?phone=5492625663454'>Whatsapp</a>. Te agracedemos habernos elegido :D.</h6>");

                email.Body = new TextPart(TextFormat.Html) { Text = contenido.ToString() };

                // Configuro el envio del correo
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EmailVerificacion(EmailAutenticacion datosEmail)
        {
            try
            {
                //Una pequeña forma de encriptar su id
                var usuario = (datosEmail.Usuario * 37 * 3) + 1;

                // Creo el mensaje
                var email = new MimeMessage();                
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(datosEmail.Destinatario));
                email.Subject = "Email de Verificacion";                
                email.Body = new TextPart(TextFormat.Html) { Text = $"Abrir el siguiente enlace en el navegador para validar su correo: <a href='{datosEmail.URL}validacionEmail/{usuario}'>{datosEmail.URL}validacionEmail/{usuario}</a>" };

                // Configuro el envio del correo
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
