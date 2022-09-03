using PPS3.Shared.Models;

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
                            encabezadoComp.UsuarioCrea
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

        public async Task<IEnumerable<Comprobante>> ObtenerComprobantesListCliente(int idCliente)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT ce.IdEncab, ce.Periodo, ce.NumComp, ce.ClienteComp, tc.TipoComp, ce.FechaComp, cl.NombreCompleto as Cliente, fp.FormaP, ce.ImporteFinal, 
                        ce.SaldoRestante, ce.Pagado, ce.Observaciones, us.NombreUs as UsuarioCrea, ce.FechaCrea
                        FROM comprobantes_encabezados as ce
                        INNER JOIN tipos_comprobantes as tc ON ce.TipoComprobante = tc.IdTipoC
                        INNER JOIN clientes as cl ON cl.IdCliente = ce.ClienteComp
                        INNER JOIN formas_pago as fp ON ce.FormaPago = fp.IdFormaP
                        INNER JOIN usuarios as us ON ce.UsuarioCrea = us.IdUsuarioAct
                        WHERE ce.ClienteComp = @ClienteComp AND ce.TipoVta = 1
                        ORDER BY ce.FechaComp DESC
                        ";
            var result = await db.QueryAsync<Comprobante>(sql, new { ClienteComp = idCliente });
            return result;
        }

        public async Task<IEnumerable<DetalleComprobante>> ObtenerDetallesComprobantesList()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT cc.IdCuerpo, cc.IdEncab, cc.ProductoCuerpo, pr.NombreProd, um.DescripcionUnidad, cc.CantidadProdC, cc.PrecioUnitario, 
                        cc.Bonificacion, cc.BonificacionTotal, cc.Total
                        FROM comprobantes_cuerpos as cc
                        INNER JOIN productos as pr ON cc.ProductoCuerpo = pr.IdProducto
						INNER JOIN unidades_medida as um ON pr.UnidadMedida = um.IdUnidad
                        "
;
            var result = await db.QueryAsync<DetalleComprobante>(sql, new {  });
            return result;
        }

        public async Task<IEnumerable<Comprobante>> ObtenerComprobantesList()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT ce.IdEncab, ce.Periodo, ce.NumComp, ce.ClienteComp, tc.TipoComp, ce.FechaComp, cl.NombreCompleto as Cliente, fp.FormaP, ce.TipoVta, ce.ImporteFinal, 
                        ce.SaldoRestante, ce.Pagado, ce.Observaciones, us.NombreUs as UsuarioCrea, ce.FechaCrea
                        FROM comprobantes_encabezados as ce
                        INNER JOIN tipos_comprobantes as tc ON ce.TipoComprobante = tc.IdTipoC
                        INNER JOIN clientes as cl ON cl.IdCliente = ce.ClienteComp
                        INNER JOIN formas_pago as fp ON ce.FormaPago = fp.IdFormaP
                        INNER JOIN usuarios as us ON ce.UsuarioCrea = us.IdUsuarioAct
                        ORDER BY ce.IdEncab DESC
                        ";
            var result = await db.QueryAsync<Comprobante>(sql, new {  });
            return result;
        }

        public async Task<bool> ActualizarComprobante(Comprobante comprobante)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE comprobantes_encabezados
                        SET
                            SaldoRestante = @SaldoRestante,
                            Pagado = @Pagado
                        WHERE IdEncab = @IdEncab
                        ";

            var result = await db.ExecuteAsync(sql, new
                                                    {
                                                        comprobante.SaldoRestante,
                                                        comprobante.Pagado,
                                                        comprobante.IdEncab
                                                    });            

            return result > 0;
        }
    }
}
