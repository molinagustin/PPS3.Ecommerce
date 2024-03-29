﻿namespace PPS3.Server.Repositories.RepTipoProducto
{
    public interface IRepTipoProducto
    {
        Task<IEnumerable<TipoProducto>> ObtenerTiposProductos();
        Task<IEnumerable<TiposProductosListado>> ObtenerTiposProdList();
        Task<TipoProducto> ObtenerTipoProducto(int id);
        Task<bool> InsertarTipoProducto(TipoProducto tipoProducto);
        Task<bool> ActualizarTipoProducto(TipoProducto tipoProducto);
        Task<bool> BorrarTipoProducto(int id);
        Task<int> CantidadProductosActivos(int idTipo);
    }
}
