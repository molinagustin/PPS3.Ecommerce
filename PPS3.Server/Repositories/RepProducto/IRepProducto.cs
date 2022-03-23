﻿namespace PPS3.Server.Repositories.RepProducto
{
    public interface IRepProducto
    {
        Task<IEnumerable<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProducto(int id);
        Task<bool> InsertarProducto(Producto producto);
        Task<bool> ActualizarProducto(Producto producto);
        Task<bool> BorrarProducto(int id);
    }
}
