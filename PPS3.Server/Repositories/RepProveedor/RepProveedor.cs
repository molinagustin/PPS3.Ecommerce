namespace PPS3.Server.Repositories.RepProveedor
{
    public class RepProveedor : IRepProveedor
    {
        private string _connectionString = string.Empty;

        public RepProveedor(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<bool> ActualizarProveedor(Proveedor proveedor)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE proveedores
                        SET
                            NombreProv=@NombreProv,
                            DomicilioProv=@DomicilioProv,
                            UsuarioModif=@UsuarioModif,
                            FechaUltModif=@FechaUltModif
                        
                        WHERE IdProveedor = @IdProveedor
                        ";
            var result = await db.ExecuteAsync(sql, new {
                                                        proveedor.NombreProv,
                                                        proveedor.DomicilioProv,
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        proveedor.IdProveedor
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarProveedor(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE proveedores 
                        SET 
                            Activo=0,
                            UsuarioModif=@UsuarioModif,
                            FechaUltModif=@FechaUltModif
                        WHERE IdProveedor=@IdProveedor
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        IdProveedor = id 
                                                        });

            return result > 0;
        }

        public async Task<bool> InsertarProveedor(Proveedor proveedor)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO proveedores (
                                                NombreProv, 
                                                DomicilioProv, 
                                                UsuarioCrea, 
                                                UsuarioModif
                                                )
                        VALUES (
                                @NombreProv, 
                                @DomicilioProv, 
                                @UsuarioCrea, 
                                @UsuarioModif
                                )
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        proveedor.NombreProv,
                                                        proveedor.DomicilioProv,
                                                        UsuarioCrea = 1,
                                                        UsuarioModif = 1
                                                        });
            return result > 0;
        }

        public async Task<Proveedor> ObtenerProveedor(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM proveedores
                        WHERE IdProveedor = @IdProveedor
                        ";

            var result = await db.QueryFirstOrDefaultAsync<Proveedor>(sql, new {IdProveedor = id});
            return result;
        }

        public async Task<IEnumerable<Proveedor>> ObtenerProveedores()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM proveedores
                        ";

            var result = await db.QueryAsync<Proveedor>(sql, new {});
            return result;
        }
    }
}
