using PPS3.Shared.InternalModels;

namespace PPS3.Server.ExternalServices.ServEmail
{
    public interface IServEmail
    {
        bool EmailContacto(EmailBasico datosEmail);
        bool EmailVerificacion(EmailAutenticacion datosEmail);
        bool EmailModificacionOrden(OrdenesCompraListado orden);
    }
}
