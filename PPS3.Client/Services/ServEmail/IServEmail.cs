namespace PPS3.Client.Services.ServEmail
{
    public interface IServEmail
    {
        Task<bool> EmailContacto(EmailBasico datosEmail);
        Task<bool> EmailVerificacion(EmailAutenticacion datosEmail);
        Task<bool> EmailModificacionOrden(OrdenesCompraListado orden);
    }
}
