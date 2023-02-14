using PPS3.Shared.Models;

namespace PPS3.Server.Repositories.RepUnidadMedida
{
    public class RepUnidadMedida : IRepUnidadMedida
    {
        private string _connectionString;

        public RepUnidadMedida(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<bool> ActualizarUnidadMedida(UnidadMedida unidadMedida)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE unidades_medida
                        SET
                            DescripcionUnidad = @DescripcionUnidad,
                            Activo = @Activo,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUnidad = @IdUnidad
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        unidadMedida.DescripcionUnidad,
                                                        unidadMedida.Activo,
                                                        unidadMedida.UsuarioModif,
                                                        FechaUltModif = DateTime.Now,
                                                        unidadMedida.IdUnidad
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarUnidadMedida(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE unidades_medida
                        SET
                            Activo = 0,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUnidad = @IdUnidad
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        IdUnidad = id
                                                        });
            return result > 0;
        }

        public async Task<bool> InsertarUnidadMedida(UnidadMedida unidadMedida)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO unidades_medida (
                                                    DescripcionUnidad,
                                                    UsuarioCrea,
                                                    UsuarioModif
                                                    )
                        VALUES (
                                @DescripcionUnidad,
                                @UsuarioCrea,
                                @UsuarioModif
                                )
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        unidadMedida.DescripcionUnidad,
                                                        unidadMedida.UsuarioCrea,
                                                        unidadMedida.UsuarioModif
                                                        });
            return result > 0;
        }

        public async Task<IEnumerable<UnidadMedida>> ObtenerUnidadesMedida()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM unidades_medida
                        WHERE Activo = 1
                        ORDER BY DescripcionUnidad
                        ";

            var result = await db.QueryAsync<UnidadMedida>(sql, new { });
            return result;
        }

        public async Task<UnidadMedida> ObtenerUnidadMedida(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM unidades_medida
                        WHERE IdUnidad = @IdUnidad
                        ";

            var result = await db.QueryFirstOrDefaultAsync<UnidadMedida>(sql, new { IdUnidad = id });
            return result;
        }

        public async Task<IEnumerable<UnidadesMedidaListado>> ObtenerUnidadMedListado()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT um.IdUnidad, um.DescripcionUnidad, um.Activo, u1.NombreUs as UsuarioCrea, u2.NombreUs as UsuarioModif, um.FechaCrea, um.FechaUltModif
                        FROM unidades_medida as um
                        INNER JOIN usuarios as u1 ON um.UsuarioCrea = u1.IdUsuarioAct
                        INNER JOIN usuarios as u2 ON um.UsuarioModif = u2.IdUsuarioAct
                        ORDER BY um.DescripcionUnidad
                        ";

            var result = await db.QueryAsync<UnidadesMedidaListado>(sql, new { });
            return result;
        }
    }
}
