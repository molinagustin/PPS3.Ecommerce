using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServCarroCompra
{
    public interface IServCarroCompra
    {
        Task<IEnumerable<CarroCompra>> ObtenerCarros();
        Task<CarroCompra> ObtenerCarro(int id);
        Task<bool> GuardarCarro(CarroCompra carro);
    }
}
