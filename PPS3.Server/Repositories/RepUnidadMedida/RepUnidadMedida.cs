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
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUnidad = @IdUnidad
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        unidadMedida.DescripcionUnidad,
                                                        UsuarioModif = 1,
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
                                                        UsuarioCrea = 1,
                                                        UsuarioModif = 1
                                                        });
            return result > 0;
        }

        public async Task<IEnumerable<UnidadMedida>> ObtenerUnidadesMedida()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM unidades_medida
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
    }
}
