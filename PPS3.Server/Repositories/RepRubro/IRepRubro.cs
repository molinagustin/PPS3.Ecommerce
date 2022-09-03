﻿namespace PPS3.Server.Repositories.RepRubro
{
    public interface IRepRubro
    {
        Task<IEnumerable<Rubro>> ObtenerRubros();
        Task<IEnumerable<RubroListado>> ObtenerRubrosListado();
        Task<IEnumerable<RubroCategoria>> ObtenerRubrosCategorias();
        Task<IEnumerable<TipoProductoCategoria>> ObtenerTiposProductosCategorias();
        Task<Rubro> ObtenerRubro(int id);
        Task<bool> InsertarRubro(Rubro rubro);
        Task<bool> ActualizarRubro(Rubro rubro);
        Task<bool> BorrarRubro(int id);
    }
}
