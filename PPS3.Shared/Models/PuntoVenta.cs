namespace PPS3.Shared.Models
{
    //Punto de venta relacionado al encabezado del comprobante por si se hacen ventas en mas de 1 sucursal
    public class PuntoVenta
    {
        public int IdPuntoVta { get; set; }
        public string PuntoVta { get; set; } = string.Empty;
        public DateTime FechaCrea { get; set; }
    }
}
