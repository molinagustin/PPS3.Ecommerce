namespace PPS3.Server.Repositories.RepIngresoCuentaCorriente
{
    public interface IRepIngresoCuentaCorriente
    {
        Task<IEnumerable<IngresoCuentaCorriente>> ObtenerIngresosCC();
        Task<IngresoCuentaCorriente> ObtenerIngresoCC(int id);
        Task<bool> InsertarIngresoCC(IngresoCuentaCorriente ingresoCC);
    }
}