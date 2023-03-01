using PPS3.Shared.Models;

namespace PPS3.Server.Repositories.RepHerramientas
{
    public class RepHerramientas : IRepHerramientas
    {
        private string _connectionString;

        public RepHerramientas(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> CambiarPrecios(CambioPrecios cambios)
        {
            var db = dbConnection();

            decimal valorCambio = 1M;
            if (cambios.Aumentar) valorCambio += (cambios.Porcentaje/100);
            else valorCambio -= (cambios.Porcentaje / 100);

            if (valorCambio < 0) return 0;
            else if (valorCambio > 3) return 0;

            var strValorCambio = valorCambio.ToString().Replace(',', '.');
            var sqlAnd = "";

            if (!cambios.Global)
            {
                if (cambios.Rubros) sqlAnd += $" AND Rubro={cambios.Rubro} ";
                if (cambios.TiposProds) sqlAnd += $" AND TipoProd={cambios.TipoProd} ";
                if (cambios.Provs) sqlAnd += $" AND Proveedor={cambios.Prov} ";
            }

            var sql = @$"
                    UPDATE productos 
                    SET PrecioCosto = (PrecioCosto * {strValorCambio}), PrecioFinal = (PrecioFinal * {strValorCambio}), UsuarioModif={cambios.IdUsAct}, FechaUltModif=@FechaUltModif
                    WHERE 1=1 {sqlAnd};
                    ";

            var result = await db.ExecuteAsync(sql, new { FechaUltModif = DateTime.Now });

            return result;
        }
    }
}
