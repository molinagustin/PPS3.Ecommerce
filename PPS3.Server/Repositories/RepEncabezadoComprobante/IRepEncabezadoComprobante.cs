﻿namespace PPS3.Server.Repositories.RepEncabezadoComprobante
{
    public interface IRepEncabezadoComprobante
    {
        Task<IEnumerable<EncabezadoComprobante>> ObtenerEncabezadosComp();
        Task<IEnumerable<Comprobante>> ObtenerComprobantesListCliente(int idCliente);
        Task<IEnumerable<Comprobante>> ObtenerComprobantesList();
        Task<IEnumerable<DetalleComprobante>> ObtenerDetallesComprobantesList();
        Task<EncabezadoComprobante> ObtenerEncabezadoComp(int id);
        Task<int> InsertarEncabezadoComp(EncabezadoComprobante encabezadoComp);
        Task<bool> ActualizarComprobante(Comprobante comprobante);
    }
}
