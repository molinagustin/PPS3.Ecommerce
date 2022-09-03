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
                            Activo = @Activo,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdRubro = @IdRubro
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        rubro.DescRubro, 
                                                        rubro.Activo,
                                                        rubro.UsuarioModif, 
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
                                                        rubro.UsuarioCrea, 
                                                        rubro.UsuarioModif
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

        public async Task<IEnumerable<RubroListado>> ObtenerRubrosListado()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT r.IdRubro, r.DescRubro, r.Activo, u1.NombreUs as UsuarioCrea, u2.NombreUs as UsuarioModif, r.FechaCrea, r.FechaUltModif
                        FROM rubros as r
                        INNER JOIN usuarios as u1 ON r.UsuarioCrea = u1.IdUsuarioAct
                        INNER JOIN usuarios as u2 ON r.UsuarioModif = u2.IdUsuarioAct
                        ORDER BY r.DescRubro
                        ";

            var result = await db.QueryAsync<RubroListado>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<RubroCategoria>> ObtenerRubrosCategorias()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT r.IdRubro, r.DescRubro
                        FROM rubros as r
                        WHERE r.Activo = 1
                        ORDER BY r.DescRubro
                        ";
            var result = await db.QueryAsync<RubroCategoria>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<TipoProductoCategoria>> ObtenerTiposProductosCategorias()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT pt.IdTipo, pt.DescripcionTipo, pt.Rubro
                        FROM productos_tipos as pt
                        WHERE pt.Activo = 1
                        ";
            var result = await db.QueryAsync<TipoProductoCategoria>(sql, new { });
            return result;
        }
    }
}
