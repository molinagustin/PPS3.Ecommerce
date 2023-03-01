namespace PPS3.Client.Services.ServHerramientas
{
    public interface IServHerramientas
    {
        Task<int> CambiarPrecios(CambioPrecios cambios);
    }
}