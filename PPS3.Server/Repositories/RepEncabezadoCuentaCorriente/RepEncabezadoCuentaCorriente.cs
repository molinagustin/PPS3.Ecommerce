﻿namespace PPS3.Server.Repositories.RepEncabezadoCuentaCorriente
{
    public class RepEncabezadoCuentaCorriente : IRepEncabezadoCuentaCorriente
    {
        private string _connectionString;

        public RepEncabezadoCuentaCorriente(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> ActualizarCuentaCorriente(EncabezadoCuentaCorriente encabezadoCC)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE cuentas_corrientes
                        SET
                            SaldoCCC = @SaldoCCC,
                            LimiteSaldo = @LimiteSaldo,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE NumCC = @NumCC
                        ";
            var result = await db.ExecuteAsync(sql, new 
                            { 
                            encabezadoCC.SaldoCCC,
                            encabezadoCC.LimiteSaldo,
                            UsuarioModif = 1,
                            FechaUltModif = DateTime.Now,
                            encabezadoCC.NumCC
                            });
            return result > 0;
        }

        public async Task<bool> BorrarCuentaCorriente(int numCC)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE cuentas_corrientes
                        SET
                            Activo = 0,
                            UsuarioModif = @UsuarioModif
                        WHERE NumCC = @NumCC
                        ";
            var result = await db.ExecuteAsync(sql, new 
                            { 
                            UsuarioModif = 1,
                            NumCC = numCC
                            });
            return result > 0;
        }

        public async Task<bool> InsertarCuentaCorriente(EncabezadoCuentaCorriente encabezadoCC)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO cuentas_corrientes
                                    (
                                    ClienteCC,
                                    SaldoCCC,
                                    LimiteSaldo,
                                    UsuarioCrea,
                                    UsuarioModif
                                    )
                        VALUES      (
                                    @ClienteCC,
                                    @SaldoCCC,
                                    @LimiteSaldo,
                                    @UsuarioCrea,
                                    @UsuarioModif
                                    )
                        ";
            var result = await db.ExecuteAsync(sql, new 
                                    {
                                    encabezadoCC.ClienteCC,
                                    encabezadoCC.SaldoCCC,
                                    encabezadoCC.LimiteSaldo,
                                    UsuarioCrea = 1,
                                    UsuarioModif = 1
                                    });
            return result > 0;
        }

        public async Task<EncabezadoCuentaCorriente> ObtenerCCCliente(int idCliente)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cuentas_corrientes
                        WHERE ClienteCC = @ClienteCC
                        ";
            var result = await db.QueryFirstOrDefaultAsync<EncabezadoCuentaCorriente>(sql, new { ClienteCC = idCliente});
            return result;
        }

        public async Task<EncabezadoCuentaCorriente> ObtenerCuentaCorriente(int numCC)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cuentas_corrientes
                        WHERE NumCC = @NumCC
                        ";
            var result = await db.QueryFirstOrDefaultAsync<EncabezadoCuentaCorriente>(sql, new { NumCC = numCC });
            return result;
        }

        public async Task<IEnumerable<EncabezadoCuentaCorriente>> ObtenerCuentasCorrientes()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cuentas_corrientes
                        ";
            var result = await db.QueryAsync<EncabezadoCuentaCorriente>(sql, new { });
            return result;
        }
    }
}
