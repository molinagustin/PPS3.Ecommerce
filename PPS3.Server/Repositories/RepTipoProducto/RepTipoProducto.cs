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
                            Rubro = @Rubro,
                            Activo = @Activo,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdTipo = @IdTipo
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        tipoProducto.DescripcionTipo,
                                                        tipoProducto.Rubro,
                                                        tipoProducto.Activo,
                                                        tipoProducto.UsuarioModif,
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
                                                    Rubro,
                                                    UsuarioCrea,
                                                    UsuarioModif
                                                    )
                        VALUES (
                                @DescripcionTipo,
                                @Rubro,
                                @UsuarioCrea,
                                @UsuarioModif
                                )
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        tipoProducto.DescripcionTipo,
                                                        tipoProducto.Rubro,
                                                        tipoProducto.UsuarioCrea,
                                                        tipoProducto.UsuarioModif
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

        public async Task<IEnumerable<TiposProductosListado>> ObtenerTiposProdList()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT pt.IdTipo, pt.DescripcionTipo, r.DescRubro as Rubro, pt.Activo, u1.NombreUs as UsuarioCrea, u2.NombreUs as UsuarioModif,
                        pt.FechaCrea, pt.FechaUltModif
                        FROM productos_tipos as pt
						INNER JOIN rubros as r ON pt.Rubro = r.IdRubro
                        INNER JOIN usuarios as u1 ON pt.UsuarioCrea = u1.IdUsuarioAct
                        INNER JOIN usuarios as u2 ON pt.UsuarioModif = u2.IdUsuarioAct
                        ORDER BY pt.DescripcionTipo
                        ";

            var result = await db.QueryAsync<TiposProductosListado>(sql, new { });
            return result;
        }
    }
}