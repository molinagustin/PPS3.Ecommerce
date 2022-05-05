﻿namespace PPS3.Client.Services.ServProducto
{
    public interface IServProducto
    {
        Task<IEnumerable<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProducto(int id);
        Task<Producto> ObtenerProducto(string nombreProd);
        Task GuardarProducto(Producto producto);
        Task BorrarProducto(int id);
    }
}