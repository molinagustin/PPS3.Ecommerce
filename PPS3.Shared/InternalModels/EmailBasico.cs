namespace PPS3.Shared.InternalModels
{
    public class EmailBasico
    {
        public string Remitente { get; set; } = string.Empty;
        public string Destinatario { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}
