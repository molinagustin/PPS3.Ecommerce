namespace PPS3.Server.Repositories.RepCarroCompra
{
    public interface IRepCarroCompra
    {
        Task<IEnumerable<CarroCompra>> ObtenerCarrosCompras();
        Task<CarroCompra> ObtenerCarroCompra(int id);
        Task<int> ObtenerIdCarroNuevo(int idUsuario);
        Task<int> InsertarCarroCompra(int idUsuario);
        Task<bool> ActualizarCarroCompra(CarroCompra carroCompra);        
    }
}
