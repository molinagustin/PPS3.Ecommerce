﻿namespace PPS3.Client.Services.ServRubro
{
    public interface IServRubro
    {
        Task<IEnumerable<Rubro>> ObtenerRubros();
        Task<IEnumerable<RubroListado>> ObtenerRubrosListado();
        Task<IEnumerable<RubroCategoria>> ObtenerRubrosCategorias();
        Task<IEnumerable<TipoProductoCategoria>> ObtenerTiposProductosCategorias();
        Task<Rubro> ObtenerRubro(int id);
        Task<bool> GuardarRubro(Rubro rubro);
        Task<bool> BorrarRubro(int id);
    }
}
