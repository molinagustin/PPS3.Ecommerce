namespace PPS3.Server.Repositories.RepCliente
{
    public class RepCliente : IRepCliente
    {
        private string _connectionString;

        public RepCliente(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE clientes
                        SET
                            TipoCliente=@TipoCliente,
                            Genero=@Genero,
                            TipoDocumento=@TipoDocumento,
                            NumDocumento=@NumDocumento,
                            NombreCompleto=@NombreCompleto,
                            CondIva=@CondIva,
                            DomicilioC=@DomicilioC,
                            Telefono=@Telefono,
                            Localidad=@Localidad,
                            FechaUltModif=@FechaUltModif
                        WHERE IdCliente=@IdCliente
                        ";
            var result = await db.ExecuteAsync(sql, new{
                                                        cliente.TipoCliente,
                                                        cliente.Genero,
                                                        cliente.TipoDocumento,
                                                        cliente.NumDocumento,
                                                        cliente.NombreCompleto,
                                                        cliente.CondIva,
                                                        cliente.DomicilioC,
                                                        cliente.Telefono,
                                                        cliente.LocalidadC,
                                                        FechaUltModif = DateTime.Now,
                                                        IdCliente = cliente.IdCliente
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarCliente(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE clientes
                        SET
                            Activo = 0,
                            FechaUltModif=@FechaUltModif
                        WHERE IdCliente=@IdCliente
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        FechaUltModif = DateTime.Now,
                                                        IdCliente = id
                                                        });
            return result > 0;
        }

        public async Task<int> CrearCliente(UsuarioCliente usuarioCliente)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO clientes(
                                            TipoCliente,
                                            Genero,
                                            TipoDocumento,
                                            NumDocumento,
                                            NombreCompleto,
                                            CondIva,
                                            DomicilioC,
                                            Telefono,
                                            LocalidadC
                                            )
                        VALUES  (
                                @TipoCliente,
                                @Genero,
                                @TipoDocumento,
                                @NumDocumento,
                                @NombreCompleto,
                                @CondIva,
                                @DomicilioC,
                                @Telefono,
                                @LocalidadC
                                )                                
                        ";

            var result = await db.ExecuteAsync(sql, new {
                                                        usuarioCliente.TipoCliente,
                                                        usuarioCliente.Genero,
                                                        usuarioCliente.TipoDocumento,
                                                        usuarioCliente.NumDocumento,
                                                        usuarioCliente.NombreCompleto,
                                                        usuarioCliente.CondIva,
                                                        usuarioCliente.DomicilioC,
                                                        usuarioCliente.Telefono,
                                                        usuarioCliente.LocalidadC
                                                        });
            //Si el resultado de la insercion es mayor a 0 quiere decir que fue exitosa
            if (result > 0)
            {
                //Obtengo el id del nuevo cliente y si se obtiene correctamente  lo devuelvo, sino devuelvo un 0
                var idNuevoCliente = await ObtenerIdCliente(usuarioCliente.NombreCompleto, usuarioCliente.NumDocumento);
                if (idNuevoCliente > 0)
                    return idNuevoCliente;
                else
                    return 0;
            }
            else
                return 0;                
        }

        public async Task<Cliente> ObtenerCliente(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM clientes
                        WHERE IdCliente = @IdCliente
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { IdCliente = id });
            return result;
        }

        public async Task<Cliente> ObtenerCliente(string nombreCliente)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM clientes
                        WHERE NombreCompleto = @NombreCompleto
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { NombreCompleto = nombreCliente });
            return result;
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientes()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM clientes
                        ";
            var result = await db.QueryAsync<Cliente>(sql, new { });
            return result;
        }

        public async Task<int> ObtenerIdCliente(string nombreCliente, string numDocumento)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM clientes
                        WHERE NombreCompleto = @NombreCompleto AND NumDocumento = @NumDocumento
                        ";
            //Uso ExecuteScalar para obtener solo el ID del cliente. Tambien le especifico de que tipo es el dato que quiero obtener
            var result = await db.ExecuteScalarAsync<int>(sql, new { 
                                                                   NombreCompleto = nombreCliente, 
                                                                   NumDocumento = numDocumento 
                                                                   });
            return result;
        }
    }
}
