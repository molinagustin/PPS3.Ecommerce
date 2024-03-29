﻿namespace PPS3.Client.Services.ServTipoProducto
{
    public interface IServTipoProducto
    {
        Task<IEnumerable<TipoProducto>> ObtenerTiposProd();
        Task<IEnumerable<TiposProductosListado>> ObtenerTiposProdList();
        Task<TipoProducto> ObtenerTipoProd(int id);
        Task<bool> GuardarTipoProd(TipoProducto tipoProd);
        Task<bool> BorrarTipoProd(int id);
        Task<int> CantidadProductosActivos(int idTipo);
    }
}
