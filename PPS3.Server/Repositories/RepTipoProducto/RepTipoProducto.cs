namespace PPS3.Server.Repositories.RepTipoProducto
{
    public class RepTipoProducto : IRepTipoProducto
    {
        private string _connectionString;

        public RepTipoProducto(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> ActualizarTipoProducto(TipoProducto tipoProducto)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE productos_tipos
                        SET
                            DescripcionTipo = @DescripcionTipo,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdTipo = @IdTipo
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        tipoProducto.DescripcionTipo,
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        tipoProducto.IdTipo
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarTipoProducto(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE productos_tipos
                        SET
                            Activo = 0,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdTipo = @IdTipo
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        IdTipo = id
                                                        });
            return result > 0;
        }

        public async Task<bool> InsertarTipoProducto(TipoProducto tipoProducto)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO productos_tipos (
                                                    DescripcionTipo,
                                                    UsuarioCrea,
                                                    UsuarioModif
                                                    )
                        VALUES (
                                @DescripcionTipo,
                                @UsuarioCrea,
                                @UsuarioModif
                                )
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        tipoProducto.DescripcionTipo,
                                                        UsuarioCrea = 1,
                                                        UsuarioModif = 1
                                                        });
            return result > 0;
        }

        public async Task<TipoProducto> ObtenerTipoProducto(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM productos_tipos
                        WHERE IdTipo=@IdTipo
                        ";

            var result = await db.QueryFirstOrDefaultAsync<TipoProducto>(sql, new { IdTipo = id });
            return result;
        }

        public async Task<IEnumerable<TipoProducto>> ObtenerTiposProductos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM productos_tipos
                        ";

            var result = await db.QueryAsync<TipoProducto>(sql, new { });
            return result;
        }
    }
}
