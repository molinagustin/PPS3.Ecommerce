namespace PPS3.Server.Repositories.RepRubro
{
    public class RepRubro : IRepRubro
    {
        private string _connectionString;

        public RepRubro(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> ActualizarRubro(Rubro rubro)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE rubros
                        SET
                            DescRubro = @DescRubro,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdRubro = @IdRubro
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        rubro.DescRubro, 
                                                        UsuarioModif = 1, 
                                                        FechaUltModif = DateTime.Now, 
                                                        rubro.IdRubro
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarRubro(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE rubros
                        SET 
                            Activo=0,
                            UsuarioModif=@UsuarioModif,
                            FechaUltModif=@FechaUltModif
                        WHERE IdRubro = @IdRubro
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        IdRubro = id 
                                                        });
            return result > 0;
        }

        public async Task<bool> InsertarRubro(Rubro rubro)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO rubros (
                                            DescRubro, 
                                            UsuarioCrea, 
                                            UsuarioModif
                                            )
                        VALUES (
                                @DescRubro, 
                                @UsuarioCrea, 
                                @UsuarioModif
                                )
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        rubro.DescRubro, 
                                                        UsuarioCrea = 1, 
                                                        UsuarioModif = 1
                                                        });
            return result > 0;
        }

        public async Task<Rubro> ObtenerRubro(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * 
                        FROM rubros
                        WHERE IdRubro = @IdRubro
                        ";

            var result = await db.QueryFirstOrDefaultAsync<Rubro>(sql, new { IdRubro = id });
            return result;
        }

        public async Task<IEnumerable<Rubro>> ObtenerRubros()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * 
                        FROM rubros
                        ";

            var result = await db.QueryAsync<Rubro>(sql, new { });
            return result;
        }
    }
}
