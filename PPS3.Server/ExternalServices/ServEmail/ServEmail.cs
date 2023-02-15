using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

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
