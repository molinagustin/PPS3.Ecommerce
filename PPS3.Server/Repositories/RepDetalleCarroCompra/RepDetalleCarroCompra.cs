using PPS3.Shared.Models;

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
                            PrecioUnit = @PrecioUnit,
                            Bonificacion = @Bonificacion,
|                           BonificacionTotal = @BonificacionTotal,
                            SubTotal = @SubTotal
                        WHERE IdDetalle = @IdDetalle
                        ";
            var result = await db.ExecuteAsync(sql, new {
                                                        detalleCarroCompra.Producto,
                                                        detalleCarroCompra.Cantidad,
                                                        detalleCarroCompra.PrecioUnit,
                                                        detalleCarroCompra.Bonificacion,
                                                        detalleCarroCompra.BonificacionTotal,
                                                        detalleCarroCompra.SubTotal,
                                                        detalleCarroCompra.IdDetalle
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarDetalleCarro(int id)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE
                        FROM carros_compras_detalles
                        WHERE IdDetalle = @IdDetalle
                        ";
            var result = await db.ExecuteAsync(sql, new {
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
                                                            PrecioUnit,
                                                            Bonificacion,
                                                            BonificacionTotal,
                                                            SubTotal
                                                            )
                        VALUES (
                                @Carro,
                                @Producto,
                                @Cantidad,
                                @PrecioUnit,
                                @Bonificacion,
                                @BonificacionTotal,
                                @SubTotal
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        detalleCarroCompra.Carro,
                                                        detalleCarroCompra.Producto,
                                                        detalleCarroCompra.Cantidad,
                                                        detalleCarroCompra.PrecioUnit,
                                                        detalleCarroCompra.Bonificacion,
                                                        detalleCarroCompra.BonificacionTotal,
                                                        detalleCarroCompra.SubTotal
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
                        SELECT ccd.IdDetalle, ccd.Carro, ccd.Producto, ccd.Cantidad, ccd.PrecioUnit, ccd.Bonificacion, ccd.BonificacionTotal, ccd.SubTotal,
                        p.NombreProd as NombreProducto, p.Descripcion, p.ImagenDestacada, um.DescripcionUnidad
                        FROM carros_compras_detalles as ccd
                        INNER JOIN productos as p ON ccd.Producto = p.IdProducto
                        INNER JOIN unidades_medida as um ON p.UnidadMedida = um.IdUnidad
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

        public async Task<bool> ActualizarStockProductos(int id)
        {
            var db = dbConnection();

            var sql = $"SELECT ccd.Producto AS IdProducto, (ccd.Cantidad + p.StockExistencia) AS StockExistencia FROM carros_compras_detalles ccd INNER JOIN productos p ON ccd.Producto = p.IdProducto WHERE ccd.Carro = {id};";
            var result = await db.QueryAsync<StockProducto>(sql);

            if(result != null && result.Count() > 0)
            {
                //Actualizo el stock de cada producto
                var cantProd = result.Count();
                var cantProdAct = 0;

                foreach (var prod in result)
                {
                    var sqlAct = "UPDATE productos SET StockExistencia=@StockExistencia WHERE IdProducto=@IdProducto;";
                    cantProdAct += await db.ExecuteAsync(sqlAct, new { prod.StockExistencia, prod.IdProducto });
                }

                return (cantProd == cantProdAct);
            }
            else return false;
        }
    }
}
