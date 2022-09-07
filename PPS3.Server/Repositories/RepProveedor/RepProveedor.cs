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
        
        public async Task<int> ActualizarProveedor(Proveedor proveedor)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE proveedores
                        SET
                            NombreProv=@NombreProv,
                            DomicilioProv=@DomicilioProv,
                            Activo=@Activo,
                            UsuarioModif=@UsuarioModif,
                            FechaUltModif=@FechaUltModif                        
                        WHERE IdProveedor = @IdProveedor
                        ";
            var result = await db.ExecuteAsync(sql, new {
                                                        proveedor.NombreProv,
                                                        proveedor.DomicilioProv,
                                                        proveedor.Activo,
                                                        proveedor.UsuarioModif,
                                                        FechaUltModif = DateTime.Now,
                                                        proveedor.IdProveedor
                                                        });
            return result; //Devuelvo el valor haciendo referencia que se creo o no un registro
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

        public async Task<int> InsertarProveedor(Proveedor proveedor)
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
                                                        proveedor.UsuarioCrea,
                                                        proveedor.UsuarioModif
                                                        });
            if (result > 0)
            {
                var ultimoId = await UltimoIdProveedor();

                return ultimoId;
            }
            else
                return 0;
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
                        ORDER BY NombreProv
                        ";

            var result = await db.QueryAsync<Proveedor>(sql, new {});
            return result;
        }

        public async Task<IEnumerable<ProveedorListado>> ObtenerProveedoresListado()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT prov.IdProveedor, prov.NombreProv, prov.DomicilioProv, prov.Activo,              us1.NombreUs as UsuarioCrea, 
                            us2.NombreUs as UsuarioModif, prov.FechaCrea, prov.FechaUltModif
                        FROM proveedores as prov
                        INNER JOIN usuarios as us1 ON prov.UsuarioCrea = us1.IdUsuarioAct
                        INNER JOIN usuarios as us2 ON prov.UsuarioModif = us2.IdUsuarioAct
                        ORDER BY prov.NombreProv
                        ";

            var result = await db.QueryAsync<ProveedorListado>(sql, new { });
            return result;
        }

        public async Task<int> UltimoIdProveedor()
        {
            var db = dbConnection();    

            var sql = @"
                        SELECT MAX(p.IdProveedor) as IdProveedor
                        FROM proveedores as p
                        ";

            var result = await db.ExecuteScalarAsync<int>(sql, new { });
            return result;
        }
    }    
}
