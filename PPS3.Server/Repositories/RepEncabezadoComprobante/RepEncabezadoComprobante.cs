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
            //Verifico si la forma de pago fue con tarjeta o no
            if (encabezadoComp.FormaPago != 2 && encabezadoComp.FormaPago != 3)
                encabezadoComp.Tarjeta = 8;

            var db = dbConnection();

            var sql = @"
                        INSERT INTO comprobantes_encabezados
                            (
                            NumComprobante,
                            CAE,
                            TipoComprobante,
                            FechaComp,
                            ClienteComp,
                            ConceptoInc,
                            PuntoVta,
                            TipoVenta,
                            FormaPago,
                            Tarjeta,
                            NumeroTarjDebito,
                            NumeroTarjCredito,
                            UsuarioCrea
                            )
                        VALUES 
                            (
                            @NumComprobante,
                            @CAE,
                            @TipoComprobante,
                            @FechaComp,
                            @ClienteComp,
                            @ConceptoInc,
                            @PuntoVta,
                            @TipoVenta,
                            @FormaPago,
                            @Tarjeta,
                            @NumeroTarjDebito,
                            @NumeroTarjCredito,
                            @UsuarioCrea
                            )
                        ";

            var result = await db.ExecuteAsync(sql, new 
                            { 
                            encabezadoComp.NumComprobante,
                            encabezadoComp.CAE,
                            encabezadoComp.TipoComprobante,
                            encabezadoComp.FechaComp,
                            encabezadoComp.ClienteComp,
                            encabezadoComp.ConceptoInc,
                            encabezadoComp.PuntoVta,
                            encabezadoComp.TipoVenta,
                            encabezadoComp.FormaPago,
                            encabezadoComp.Tarjeta,
                            encabezadoComp.NumeroTarjDebito,
                            encabezadoComp.NumeroTarjCredito,
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
