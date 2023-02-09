namespace PPS3.Server.Repositories.RepReporte
{
    public interface IRepReporte
    {
        Task<IEnumerable<MasProducto>> ReporteMasProductos(Parametros parametros);
        Task<IEnumerable<Ganancia>> ReporteGanancias(Parametros parametros);
        Task<IEnumerable<StockProd>> ReporteStockProductos(Parametros parametros);
        Task<IEnumerable<ProductoFecha>> ReporteProductosFecha(Parametros parametros);
    }
}
