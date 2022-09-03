namespace PPS3.Server.Repositories.RepLocalidad
{
    public class RepLocalidad : IRepLocalidad
    {
        private string _connectionString;

        public RepLocalidad(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<bool> ActualizarLocalidad(Localidad localidad)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE localidades
                        SET 
                            NombreLoc=@NombreLoc,
                            Provincia=@Provincia,
                            CP=@CP,
                            UsuarioModif=@UsuarioModif,
                            FechaUltModif=@FechaUltModif
                        WHERE IdLocalidad=@IdLocalidad
                        ";
            var result = await db.ExecuteAsync(sql, new {
                                                        localidad.NombreLoc,
                                                        localidad.Provincia,
                                                        localidad.CP,
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        localidad.IdLocalidad
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarLocalidad(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE localidades
                        SET 
                            Activo = 0,
                            UsuarioModif=@UsuarioModif,
                            FechaUltModif=@FechaUltModif
                        WHERE IdLocalidad=@IdLocalidad
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        IdLocalidad = id
                                                        });
            return result > 0;
        }

        public async Task<bool> InsertarLocalidad(Localidad localidad)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO localidades (
                                                NombreLoc,
                                                Provincia,
                                                CP,
                                                UsuarioCrea,
                                                UsuarioModif
                                                )
                        VALUES (
                                @NombreLoc,
                                @Provincia,
                                @CP,
                                @UsuarioCrea,
                                @UsuarioModif
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        localidad.NombreLoc,
                                                        localidad.Provincia,
                                                        localidad.CP,
                                                        UsuarioCrea = 1,
                                                        UsuarioModif = 1
                                                        });
            return result > 0;
        }

        public async Task<Localidad> ObtenerLocalidad(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM localidades
                        WHERE IdLocalidad=@IdLocalidad
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Localidad>(sql, new { IdLocalidad = id });
            return result;
        }

        public async Task<Localidad> ObtenerLocalidad(string nombreLocalidad)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM localidades
                        WHERE NombreLoc=@NombreLoc
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Localidad>(sql, new { NombreLoc = nombreLocalidad });
            return result;
        }

        public async Task<IEnumerable<Localidad>> ObtenerLocalidades()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM localidades
                        ";
            var result = await db.QueryAsync<Localidad>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<ListaLocalidad>> ObtenerListaLocalidades()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT l.IdLocalidad, l.NombreLoc, p.NombreProv as Provincia, l.CP, l.Activo, l.UsuarioCrea, 
                        l.FechaCrea, l.UsuarioModif, l.FechaUltModif
                        FROM localidades as l
                        INNER JOIN provincias as p ON p.IdProvincia = l.Provincia
                        ";
            var result = await db.QueryAsync<ListaLocalidad>(sql, new { });
            return result;
        }
    }
}
