﻿namespace PPS3.Client.Services.ServEncabezadoRecibo
{
    public interface IServEncabezadoRecibo
    {
        Task<IEnumerable<EncabezadoRecibo>> ObtenerEncabRecCliente(int idCliente);
        Task<IEnumerable<Recibo>> ObtenerRecibosListPorCliente(int idCliente);
        Task<IEnumerable<Recibo>> ObtenerRecibosList();
        Task<IEnumerable<ReciboListado>> ObtenerRecibosListado();
        Task<EncabezadoRecibo> ObtenerEncab(int idRecibo);
        Task<int> CrearEncabRec(EncabezadoRecibo encabRec);
    }
}
