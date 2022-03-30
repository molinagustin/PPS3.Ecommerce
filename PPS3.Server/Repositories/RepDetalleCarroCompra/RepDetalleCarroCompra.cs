namespace PPS3.Server.Repositories.RepDetalleCarroCompra
{
    public class RepDetalleCarroCompra : IRepDetalleCarroCompra
    {
        private string _connectionString;

        public RepDetalleCarroCompra(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> ActualizarDetalleCarro(DetalleCarroCompra detalleCarroCompra)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE carros_compras_detalles
                        SET
                            Producto = @Producto,
                            Cantidad = @Cantidad,
                            Descuento = @Descuento,
                            FechaUltModif = @FechaUltModif
                        WHERE IdDetalle = @IdDetalle
                        ";
            var result = await db.ExecuteAsync(sql, new {
                                                        detalleCarroCompra.Producto,
                                                        detalleCarroCompra.Cantidad,
                                                        detalleCarroCompra.Descuento,
                                                        FechaUltModif = DateTime.Now,
                                                        detalleCarroCompra.IdDetalle
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarDetalleCarro(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE carros_compras_detalles
                        SET
                            Activo = 0,
                            FechaUltModif = @FechaUltModif
                        WHERE IdDetalle = @IdDetalle
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        FechaUltModif = DateTime.Now,
                                                        IdDetalle = id
                                                        });
            return result > 0;
        }

        public async Task<bool> InsertarDetalleCarro(DetalleCarroCompra detalleCarroCompra)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO carros_compras_detalles (
                                                            Carro,
                                                            Producto,
                                                            Cantidad,
                                                            Descuento
                                                            )
                        VALUES (
                                @Carro,
                                @Producto,
                                @Cantidad,
                                @Descuento
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        detalleCarroCompra.Carro,
                                                        detalleCarroCompra.Producto,
                                                        detalleCarroCompra.Cantidad,
                                                        detalleCarroCompra.Descuento
                                                        });
            return result > 0;
        }

        public async Task<DetalleCarroCompra> ObtenerDetalle(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM carros_compras_detalles
                        WHERE IdDetalle = @IdDetalle
                        ";
            var result = await db.QueryFirstOrDefaultAsync<DetalleCarroCompra>(sql, new { IdDetalle = id });
            return result;
        }

        /// <summary>
        /// Metodo que busca todos los detalles de carro de compras asignados a un carro en particular
        /// </summary>
        /// <param name="idCarro">Id del Carro de Compras</param>
        /// <returns></returns>
        public async Task<IEnumerable<DetalleCarroCompra>> ObtenerDetallesCarro(int idCarro)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM carros_compras_detalles
                        WHERE Carro = @Carro
                        ";
            var result = await db.QueryAsync<DetalleCarroCompra>(sql, new { Carro = idCarro });
            return result;
        }

        public async Task<IEnumerable<DetalleCarroCompra>> ObtenerTodosDetalles()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM carros_compras_detalles
                        ";
            var result = await db.QueryAsync<DetalleCarroCompra>(sql, new { });
            return result;
        }
    }
}
