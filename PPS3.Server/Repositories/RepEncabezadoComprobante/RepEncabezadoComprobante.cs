namespace PPS3.Server.Repositories.RepEncabezadoComprobante
{
    public class RepEncabezadoComprobante : IRepEncabezadoComprobante
    {
        private string _connectionString;

        public RepEncabezadoComprobante(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> InsertarEncabezadoComp(EncabezadoComprobante encabezadoComp)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO comprobantes_encabezados
                            (
                            Periodo,
                            NumComp,
                            TipoComprobante,
                            FechaComp,
                            ClienteComp,
                            FormaPago,
                            TipoVta,
                            ImporteFinal,
                            SaldoRestante,
                            Observaciones,
                            UsuarioCrea
                            )
                        VALUES 
                            (
                            @Periodo,
                            @NumComp,
                            @TipoComprobante,
                            @FechaComp,
                            @ClienteComp,
                            @FormaPago,
                            @TipoVta,
                            @ImporteFinal,
                            @SaldoRestante,
                            @Observaciones,
                            @UsuarioCrea
                            )
                        ";

            var result = await db.ExecuteAsync(sql, new 
                            { 
                            encabezadoComp.Periodo,
                            encabezadoComp.NumComp,
                            encabezadoComp.TipoComprobante,
                            encabezadoComp.FechaComp,
                            encabezadoComp.ClienteComp,
                            encabezadoComp.FormaPago,
                            encabezadoComp.TipoVta,
                            encabezadoComp.ImporteFinal,
                            encabezadoComp.SaldoRestante,
                            encabezadoComp.Observaciones,
                            UsuarioCrea = 1
                            });

            //Si el resultado de la insecion es exitoso, busco el ID del comprobante ingresado
            if (result > 0)
                return await ObtenerUltimoID(encabezadoComp.ClienteComp);
            else
                return 0;
        }

        public async Task<EncabezadoComprobante> ObtenerEncabezadoComp(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM comprobantes_encabezados
                        WHERE IdEncab = @IdEncab
                        ";
            var result = await db.QueryFirstOrDefaultAsync<EncabezadoComprobante>(sql, new { IdEncab = id });
            return result;
        }

        public async Task<IEnumerable<EncabezadoComprobante>> ObtenerEncabezadosComp()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM comprobantes_encabezados
                        ";
            var result = await db.QueryAsync<EncabezadoComprobante>(sql, new { });
            return result;
        }


        //Metodo por el cual obtenemos el Id del ultimo comprobante ingresado de un determinado cliente y asi reutilizarlo donde sea necesario.
        private async Task<int> ObtenerUltimoID(int idCliente)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT MAX(IdEncab)
                        FROM comprobantes_encabezados
                        WHERE ClienteComp = @ClienteComp
                        ";
            var result = await db.ExecuteScalarAsync<int>(sql, new { ClienteComp = idCliente });
            return result;
        }
    }
}
