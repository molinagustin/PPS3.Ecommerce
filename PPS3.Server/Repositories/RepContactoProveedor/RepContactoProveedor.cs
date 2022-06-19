namespace PPS3.Server.Repositories.RepContactoProveedor
{
    public class RepContactoProveedor : IRepContactoProveedor
    {
        private string _connectionString;

        public RepContactoProveedor(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> ActualizarContactoProveedor(ContactoProveedor contactoProveedor)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE proveedores_contacto
                        SET
                            Nombre = @Nombre,
                            Domicilio = @Domicilio,
                            Telefono = @Telefono,
                            Email = @Email,
                            Proveedor = @Proveedor,
                            Activo = @Activo,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdProvCont = @IdProvCont
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        contactoProveedor.Nombre,
                                                        contactoProveedor.Domicilio,
                                                        contactoProveedor.Telefono,
                                                        contactoProveedor.Email,
                                                        contactoProveedor.Proveedor,
                                                        contactoProveedor.Activo,
                                                        contactoProveedor.UsuarioModif,
                                                        FechaUltModif = DateTime.Now,
                                                        contactoProveedor.IdProvCont
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarContactoProveedor(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE proveedores_contacto
                        SET
                            Activo = 0,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdProvCont = @IdProvCont
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        IdProvCont = id
                                                        });
            return result > 0;
        }

        public async Task<bool> InsertarContactoProveedor(ContactoProveedor contactoProveedor)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO proveedores_contacto(
                                                        Nombre, 
                                                        Domicilio, 
                                                        Telefono, 
                                                        Email, 
                                                        Proveedor, 
                                                        UsuarioCrea, 
                                                        UsuarioModif
                                                        )
                        VALUES  (
                                @Nombre, 
                                @Domicilio, 
                                @Telefono, 
                                @Email, 
                                @Proveedor, 
                                @UsuarioCrea, 
                                @UsuarioModif
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        contactoProveedor.Nombre,
                                                        contactoProveedor.Domicilio,
                                                        contactoProveedor.Telefono,
                                                        contactoProveedor.Email,
                                                        contactoProveedor.Proveedor,
                                                        contactoProveedor.UsuarioCrea,
                                                        contactoProveedor.UsuarioModif
                                                        });
            return result > 0;
        }

        public async Task<ContactoProveedor> ObtenerContactoProveedor(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM proveedores_contacto
                        WHERE IdProvCont = @IdProvCont
                        ";
            var result = await db.QueryFirstOrDefaultAsync<ContactoProveedor>(sql, new { IdProvCont = id });
            return result;
        }

        public async Task<ContactoProveedor> ObtenerContactoProveedor(string nombreContacto)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM proveedores_contacto
                        WHERE Nombre = @Nombre
                        ";
            var result = await db.QueryFirstOrDefaultAsync<ContactoProveedor>(sql, new { Nombre = nombreContacto });
            return result;
        }

        public async Task<IEnumerable<ContactoProveedor>> ObtenerTodosContactos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM proveedores_contacto
                        ";
            var result = await db.QueryAsync<ContactoProveedor>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<ContactoProvListado>> ObtenerContactosListado()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT  pc.IdProvCont, pc.Nombre, pc.Domicilio, pc.Telefono, pc.Email, pv.NombreProv, pc.Activo, u1.NombreUs as UsuarioCrea, 
                        u2.NombreUs as UsuarioModif, pc.FechaCrea, pc.FechaUltModif
                        FROM proveedores_contacto as pc
                        INNER JOIN proveedores as pv ON pc.Proveedor = pv.IdProveedor
                        INNER JOIN usuarios as u1 ON pc.UsuarioCrea = u1.IdUsuarioAct
                        INNER JOIN usuarios as u2 ON pc.UsuarioModif = u2.IdUsuarioAct
                        ORDER BY pc.Nombre
                        ";
            var result = await db.QueryAsync<ContactoProvListado>(sql, new { });
            return result;
        }
    }
}
