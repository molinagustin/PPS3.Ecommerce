namespace PPS3.Server.Repositories.RepCarroCompra
{
    public class RepCarroCompra : IRepCarroCompra
    {
        private string _connectionString;

        public RepCarroCompra(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Metodo con el que se actualizan los datos del carro de compra, como su estado (Pendiente, Aprobado, Finalizado, etc.), sus observaciones, las fechas de modoficacion y totales
        /// </summary>
        /// <param name="carroCompra">Objeto del Carro de Compras con datos.</param>
        /// <returns></returns>
        public async Task<bool> ActualizarCarroCompra(CarroCompra carroCompra)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE carros_compras
                        SET
                            Estado = @Estado,
                            FechaOrden = @FechaOrden,
                            FechaEntrega = @FechaEntrega,
                            FechaUltModif = @FechaUltModif,
                            Total = @Total,
                            Pagado = @Pagado,
                            FechaPago = @FechaPago,
                            MetodoPago = @MetodoPago,
                            Observaciones = @Observaciones
                        WHERE IdCarro = @IdCarro
                        ";

            var result = 0;
            try
            {

            
            result = await db.ExecuteAsync(sql, new { 
                                                        carroCompra.Estado,
                                                        carroCompra.FechaOrden,
                                                        carroCompra.FechaEntrega,
                                                        FechaUltModif = DateTime.Now,
                                                        carroCompra.Total,
                                                        carroCompra.Pagado,
                                                        carroCompra.FechaPago,
                                                        carroCompra.MetodoPago,
                                                        carroCompra.Observaciones,
                                                        carroCompra.IdCarro
                                                        });
            

            }
            catch (SqlException sq)
            {
                Console.WriteLine(sq.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result > 0;
        }

        /// <summary>
        /// Metodo que va a servir para crear un nuevo carro vacio Activo para un usuario y asignarselo
        /// </summary>
        /// <param name="id">Id del Usuario</param>
        /// <returns></returns>
        public async Task<int> InsertarCarroCompra(int idUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO carros_compras (
                                                    UsuarioCarro  
                                                    )
                        VALUES (
                                @UsuarioCarro
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { UsuarioCarro = idUsuario });

            if (result > 0)
            {
                var idCarroNuevo = await ObtenerIdCarroNuevo(idUsuario);
                if (idCarroNuevo > 0)
                    return idCarroNuevo;
                else
                    return 0;
            }
            else
                return 0;
        }

        public async Task<CarroCompra> ObtenerCarroCompra(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM carros_compras
                        WHERE IdCarro = @IdCarro
                        ";
            var result = await db.QueryFirstOrDefaultAsync<CarroCompra>(sql, new { IdCarro = id });
            return result;
        }

        public async Task<IEnumerable<CarroCompra>> ObtenerCarrosCompras()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM carros_compras
                        ";
            var result = await db.QueryAsync<CarroCompra>(sql, new { });
            return result;
        }

        public async Task<int> ObtenerIdCarroNuevo(int idUsuario)
        {
            var db = dbConnection();

            //Buscar el nuevo carro creado y que este en estado 1=Activo
            var sql = @"
                        SELECT *
                        FROM carros_compras
                        WHERE Estado = 1 AND UsuarioCarro = @UsuarioCarro
                        ";
            var result = await db.ExecuteScalarAsync<int>(sql, new { UsuarioCarro = idUsuario });
            return result;
        }

        public async Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompra()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT cc.IdCarro, cce.Estado, u.NombreUs as UsuarioCrea, cc.FechaCrea, cc.FechaOrden, cc.FechaEntrega, cc.FechaUltModif, 
                        cc.Total, cc.Pagado, cc.FechaPago, fp.FormaP, cc.Observaciones
                        FROM carros_compras as cc
                        INNER JOIN carros_compras_estados as cce ON cc.Estado = cce.IdEstado
                        INNER JOIN usuarios as u ON cc.UsuarioCarro = u.IdUsuarioAct
                        INNER JOIN formas_pago as fp ON cc.MetodoPago = fp.IdFormaP
                        WHERE cc.Estado > 1
                        ";
            var result = await db.QueryAsync<OrdenesCompraListado>(sql, new { });
            return result;
        }

        public async Task<OrdenesCompraListado> ObtenerOCDetalle(int NumOrden)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT cc.IdCarro, cce.Estado, u.NombreUs as UsuarioCrea, cc.FechaCrea, cc.FechaOrden, cc.FechaEntrega, cc.FechaUltModif, 
                        cc.Total, cc.Pagado, cc.FechaPago, fp.FormaP, cc.Observaciones
                        FROM carros_compras as cc
                        INNER JOIN carros_compras_estados as cce ON cc.Estado = cce.IdEstado
                        INNER JOIN usuarios as u ON cc.UsuarioCarro = u.IdUsuarioAct
                        INNER JOIN formas_pago as fp ON cc.MetodoPago = fp.IdFormaP
                        WHERE cc.IdCarro = @IdCarro
                        ";
            var result = await db.QueryFirstOrDefaultAsync<OrdenesCompraListado>(sql, new { IdCarro = NumOrden });
            return result;
        }

        public async Task<IEnumerable<DetalleCarroCompra>> ObtenerOCDetalles(int NumOrden)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT ccd.Producto, ccd.Cantidad, ccd.PrecioUnit, ccd.Bonificacion, ccd.BonificacionTotal, ccd.SubTotal, 
                        p.NombreProd as NombreProducto, um.DescripcionUnidad, p.ImagenDestacada
                        FROM carros_compras_detalles as ccd
                        INNER JOIN productos as p ON p.IdProducto = ccd.Producto
                        INNER JOIN unidades_medida as um ON p.UnidadMedida = um.IdUnidad
                        WHERE ccd.Carro = @Carro
                        ";
            var result = await db.QueryAsync<DetalleCarroCompra>(sql, new { Carro = NumOrden });
            return result;
        }

        public async Task<CarroCompra> ObtenerCarroActivoUsuario(int idUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM carros_compras
                        WHERE Estado = 1 AND UsuarioCarro = @idUsuario
                        ";
            var result = await db.QueryFirstOrDefaultAsync<CarroCompra>(sql, new { idUsuario });
            return result;
        }

        public async Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompraUsuario(int idUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT cc.IdCarro, cce.Estado, cc.FechaOrden, cc.FechaEntrega, cc.FechaUltModif, 
                        cc.Total, cc.Pagado, cc.FechaPago, fp.FormaP, cc.Observaciones
                        FROM carros_compras as cc
                        INNER JOIN carros_compras_estados as cce ON cc.Estado = cce.IdEstado
                        INNER JOIN formas_pago as fp ON cc.MetodoPago = fp.IdFormaP
                        WHERE cc.Estado > 1 AND cc.UsuarioCarro = @UsuarioCarro
                        ORDER BY cc.IdCarro DESC
                        ";
            var result = await db.QueryAsync<OrdenesCompraListado>(sql, new { UsuarioCarro = idUsuario});
            return result;
        }
    }
}