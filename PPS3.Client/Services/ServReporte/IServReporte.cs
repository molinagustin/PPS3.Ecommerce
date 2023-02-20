namespace PPS3.Client.Services.ServReporte
{
    public interface IServReporte
    {
        Task<IEnumerable<MasProducto>> ReporteMasProductos(Parametros parametros);
        Task<IEnumerable<Ganancia>> ReporteGanancias(Parametros parametros);
        Task<IEnumerable<StockProd>> ReporteStockProductos(Parametros parametros);
        Task<IEnumerable<ProductoFecha>> ReporteProductosFecha(Parametros parametros);
        Task<IEnumerable<ProductoFechaReporte>> ReporteProductosFechaReporte(Parametros parametros);
    }
}
