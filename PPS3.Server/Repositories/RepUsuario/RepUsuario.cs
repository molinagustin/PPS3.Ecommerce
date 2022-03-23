namespace PPS3.Server.Repositories.RepUsuario
{
    public class RepUsuario : IRepUsuario
    {
        private string _connectionString;

        public RepUsuario(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE usuarios
                        SET
                            NombreCompleto = @NombreCompleto,
                            NombreUs = @NombreUs,
                            SaltCont = @SaltCont,
                            HashCont = @HashCont,
                            Privilegio = @Privilegio,
                            IdCliente = @IdCliente,
                            Email = @Email,
                            EmailVerificado = @EmailVerificado,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUsuarioAct = @IdUsuarioAct
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        usuario.NombreCompleto,
                                                        usuario.NombreUs,
                                                        usuario.SaltCont,
                                                        usuario.HashCont,
                                                        usuario.Privilegio,
                                                        usuario.IdCliente,
                                                        usuario.Email,
                                                        usuario.EmailVerificado,
                                                        FechaUltModif = DateTime.Now,
                                                        usuario.IdUsuarioAct
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE usuarios
                        SET
                            Activo = 0,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUsuarioAct = @IdUsuarioAct
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        FechaUltModif = DateTime.Now,
                                                        IdUsuarioAct = id
                                                        });
            return result > 0;
        }

        public async Task<bool> CrearUsuario(Usuario usuario)
        {
            var db = dbConnection();

            //El privilegio esta por defecto en la base de datos que sea para cliente, si se quiere un usuario administrador, lo tiene que modificar un usuario con privilegios adecuados
            var sql = @"
                        INSERT INTO usuarios(
                                            NombreCompleto,
                                            NombreUs,
                                            SaltCont,
                                            HashCont,
                                            IdCliente,
                                            Email
                                            )
                        VALUES (
                                @NombreCompleto,
                                @NombreUs,
                                @SaltCont,
                                @HashCont,
                                @IdCliente,
                                @Email
                                )
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        usuario.NombreCompleto,
                                                        usuario.NombreUs,
                                                        SaltCont = "",
                                                        HashCont = "",
                                                        IdCliente = 2,
                                                        usuario.Email
                                                        });
            return result > 0;
        }

        public async Task<Usuario> ObtenerUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios
                        WHERE IdUsuarioAct = @IdUsuarioAct
                        ";

            var result = await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { IdUsuarioAct = id });
            return result;
        }

        public async Task<Usuario> ObtenerUsuario(string nombreUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios
                        WHERE NombreUs = @NombreUs
                        ";

            var result = await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { NombreUs = nombreUsuario });
            return result;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios
                        ";

            var result = await db.QueryAsync<Usuario>(sql, new { });
            return result;
        }
    }
}
