using PPS3.Shared.Models;

namespace PPS3.Server.Repositories.RepEncabezadoRecibo
{
    public class RepEncabezadoRecibo : IRepEncabezadoRecibo
    {
        private string _connectionString;

        public RepEncabezadoRecibo(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> InsertarRecibo(EncabezadoRecibo encRecibo)
        {
            //Se coloca la tarjeta con ID 8 porque es pago sin tarjeta y necesita una relacion el modelo
            if (encRecibo.FormaPago != 2 && encRecibo.FormaPago != 3)
                encRecibo.Tarjeta = 8;

            var db = dbConnection();

            var sql = @"
                        INSERT INTO recibos_encabezado  (
                                                        IdCliente,
                                                        FechaRecibo,
                                                        FormaPago,
                                                        Tarjeta,
                                                        NumTarjeta,
                                                        ImporteTotal,
                                                        Observaciones,
                                                        UsuarioCrea
                                                        )
                        VALUES  (
                                @IdCliente,
                                @FechaRecibo,
                                @FormaPago,
                                @Tarjeta,
                                @NumTarjeta,
                                @ImporteTotal,
                                @Observaciones,
                                @UsuarioCrea
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new {
                                                        encRecibo.IdCliente,
                                                        encRecibo.FechaRecibo,
                                                        encRecibo.FormaPago,
                                                        encRecibo.Tarjeta,
                                                        encRecibo.NumTarjeta,
                                                        encRecibo.ImporteTotal,
                                                        encRecibo.Observaciones,
                                                        encRecibo.UsuarioCrea
                                                        });
            if (result > 0)
            {
                var id = await ObtenerUltimoIDRecibo(encRecibo.IdCliente);
                if (id > 0)
                    return id;
                else
                    return 0;
            }
            else
                return 0;
        }

        public async Task<EncabezadoRecibo> ObtenerRecibo(int idRecibo)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM recibos_encabezado
                        WHERE IdRecibo = @IdRecibo
                        ";            
            var result = await db.QueryFirstOrDefaultAsync<EncabezadoRecibo>(sql, new { IdRecibo = idRecibo});
            return result;
        }

        public async Task<IEnumerable<EncabezadoRecibo>> ObtenerRecibosPorCliente(int idCliente)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM recibos_encabezado
                        WHERE IdCliente = @IdCliente
                        ";
            var result = await db.QueryAsync<EncabezadoRecibo>(sql, new { IdCliente = idCliente });
            return result;
        }

        //Obtengo el ultimo recibo ingresado para devolverlo si es necesario en algun otro ingreso de datos
        public async Task<int> ObtenerUltimoIDRecibo(int idCliente)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT MAX(IdRecibo)
                        FROM recibos_encabezado
                        WHERE IdCliente = @IdCliente
                        ";
            var result = await db.ExecuteScalarAsync<int>(sql, new { IdCliente = idCliente });
            return result;
        }

        public async Task<IEnumerable<Recibo>> ObtenerRecibosListPorCliente(int idCliente)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT re.IdRecibo, rd.IdDetalle, rd.IdComprobante, rd.Importe, re.FechaRecibo, fp.FormaP, tj.NombreTarj, re.NumTarjeta,
                        re.ImporteTotal, re.Observaciones, u.NombreUs as UsuarioCrea, re.FechaCrea
                        FROM recibos_encabezado as re
                        INNER JOIN recibos_detalles as rd ON re.IdRecibo = rd.IdRecibo
                        INNER JOIN formas_pago as fp ON re.FormaPago = fp.IdFormaP
                        INNER JOIN tarjetas as tj ON re.Tarjeta = tj.IdTarjeta
                        INNER JOIN usuarios as u ON re.UsuarioCrea = u.IdUsuarioAct
                        WHERE re.IdCliente = @IdCliente
                        ";
            var result = await db.QueryAsync<Recibo>(sql, new { IdCliente = idCliente });
            return result;
        }

        public async Task<IEnumerable<Recibo>> ObtenerRecibosList()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT re.IdRecibo, rd.IdDetalle, rd.IdComprobante, rd.Importe, re.FechaRecibo, fp.FormaP, tj.NombreTarj, re.NumTarjeta,
                        re.ImporteTotal, re.Observaciones, u.NombreUs as UsuarioCrea, re.FechaCrea
                        FROM recibos_encabezado as re
                        INNER JOIN recibos_detalles as rd ON re.IdRecibo = rd.IdRecibo
                        INNER JOIN formas_pago as fp ON re.FormaPago = fp.IdFormaP
                        INNER JOIN tarjetas as tj ON re.Tarjeta = tj.IdTarjeta
                        INNER JOIN usuarios as u ON re.UsuarioCrea = u.IdUsuarioAct
                        ";
            var result = await db.QueryAsync<Recibo>(sql, new {  });
            return result;
        }
    }
}
