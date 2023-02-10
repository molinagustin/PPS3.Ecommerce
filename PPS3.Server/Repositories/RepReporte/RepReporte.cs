namespace PPS3.Server.Repositories.RepReporte
{
    public class RepReporte : IRepReporte
    {
        private string _connectionString;

        public RepReporte(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Ganancia>> ReporteGanancias(Parametros parametros)
        {
            // LAS GANANCIAS OBTENIDAS
            /*
                CARACTERISTICAS DE BUSQUEDA
                Por Rango Fecha
                Con y Sin Bonificacion
                Comprobantes Pagados
                Por Forma de Pago
                Tipo de Venta
             */
            var sqlAnd = "";

            if (parametros.RangoFechas)
                sqlAnd += " AND re.FechaRecibo BETWEEN '" + parametros.FechaDesde.ToString("yyyyMMdd") + "' AND '" + parametros.FechaHasta.ToString("yyyyMMdd") + "' ";

            if (parametros.UsarBonif)
                sqlAnd += (parametros.ConBonif) ? " AND ccue.Bonificacion > 0 " : " AND ccue.Bonificacion = 0 ";

            if (parametros.CompPagos)
                sqlAnd += " AND ce.Pagado = 1 ";

            if (parametros.UsarFP)
                sqlAnd += $" AND fp.IdFormaP={parametros.FormaPago} ";

            if (parametros.CC)
                sqlAnd += $" AND ce.TipoVta={parametros.TipoVta} ";

            var db = dbConnection();

            var sql = @$"
                        SELECT re.FechaRecibo, SUM(re.ImporteTotal) TotalDia
                        FROM comprobantes_encabezados ce
                        INNER JOIN comprobantes_cuerpos ccue ON ccue.IdEncab = ce.IdEncab
                        INNER JOIN recibos_detalles rd ON rd.IdComprobante = ce.IdEncab
                        INNER JOIN recibos_encabezado re ON re.IdRecibo = rd.IdRecibo
                        INNER JOIN formas_pago fp ON fp.IdFormaP = re.FormaPago
                        WHERE fp.IdFormaP != 9 {sqlAnd}
                        GROUP BY re.FechaRecibo;
                        ";

            var result = await db.QueryAsync<Ganancia>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<MasProducto>> ReporteMasProductos(Parametros parametros)
        {
            // LOS PRODUCTOS MAS VENDIDOS EN UN RANGO DE FECHAS
            /*
                CARACTERISTICAS DE BUSQUEDA
                Vendidos a CC o sin CC
                Rango de Fechas
                Modificar TOP
             */

            var sqlAnd = "";

            if (parametros.CC)
                sqlAnd += $" AND ce.TipoVta={parametros.TipoVta} ";

            if (parametros.RangoFechas)
                sqlAnd += " AND re.FechaRecibo BETWEEN '" + parametros.FechaDesde.ToString("yyyyMMdd") + "' AND '" + parametros.FechaHasta.ToString("yyyyMMdd") + "' ";

            var db = dbConnection();

            var sql = @$"
                        SELECT TOP {parametros.Top} p.NombreProd, SUM(ccd.Cantidad) AS Cantidad, um.DescripcionUnidad
                        FROM comprobantes_encabezados ce
                        INNER JOIN recibos_detalles rd ON rd.IdComprobante = ce.IdEncab
                        INNER JOIN recibos_encabezado re ON re.IdRecibo = rd.IdRecibo
                        INNER JOIN formas_pago fp ON fp.IdFormaP = re.FormaPago
                        INNER JOIN carros_compras cc ON cc.IdCarro = ce.Carro
                        INNER JOIN carros_compras_detalles ccd ON ccd.Carro = cc.IdCarro
                        INNER JOIN productos p ON p.IdProducto = ccd.Producto
                        INNER JOIN unidades_medida um ON um.IdUnidad = p.UnidadMedida
                        WHERE ce.Pagado = 1 AND fp.IdFormaP != 9 AND cc.Pagado = 1 AND cc.CompGenerado = 1 {sqlAnd}
                        GROUP BY p.NombreProd, um.DescripcionUnidad
                        ORDER BY Cantidad DESC;
                        ";

            var result = await db.QueryAsync<MasProducto>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<ProductoFecha>> ReporteProductosFecha(Parametros parametros)
        {
            // LOS ULTIMOS PRODUCTOS VENDIDOS (UN LISTADO GENERAL DE LOS PRODUCTOS VENDIDOS Y LA CANTIDAD EN UN RANGO DE FECHA)
            /*
                CARACTERISTICAS DE BUSQUEDA
                Tipo de Venta
                Comprobante Pagado
                Fecha Comp
             */

            var sqlAnd = "";

            if (parametros.CC)
                sqlAnd += $" AND ce.TipoVta={parametros.TipoVta} ";

            if (parametros.CompPagos)
                sqlAnd += " AND ce.Pagado = 1 AND cc.Pagado = 1 ";

            if (parametros.RangoFechas)
                sqlAnd += " AND ce.FechaComp BETWEEN '" + parametros.FechaDesde.ToString("yyyyMMdd") + "' AND '" + parametros.FechaHasta.ToString("yyyyMMdd") + "' ";

            var db = dbConnection();

            var sql = @$"
                        SELECT ce.FechaComp, p.NombreProd, SUM(ccd.Cantidad) AS Cantidad, um.DescripcionUnidad
                        FROM comprobantes_encabezados ce
                        INNER JOIN recibos_detalles rd ON rd.IdComprobante = ce.IdEncab
                        INNER JOIN recibos_encabezado re ON re.IdRecibo = rd.IdRecibo
                        INNER JOIN formas_pago fp ON fp.IdFormaP = re.FormaPago
                        INNER JOIN carros_compras cc ON cc.IdCarro = ce.Carro
                        INNER JOIN carros_compras_detalles ccd ON ccd.Carro = cc.IdCarro
                        INNER JOIN productos p ON p.IdProducto = ccd.Producto
                        INNER JOIN unidades_medida um ON um.IdUnidad = p.UnidadMedida
                        WHERE fp.IdFormaP != 9 AND cc.CompGenerado = 1 {sqlAnd}
                        GROUP BY ce.FechaComp, p.NombreProd, um.DescripcionUnidad
                        ORDER BY ce.FechaComp;
                        ";

            var result = await db.QueryAsync<ProductoFecha>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<StockProd>> ReporteStockProductos(Parametros parametros)
        {
            //  LOS PRODUCTOS SIN STOCK O QUE TENGAN POCO STOCK
            /*
                CARACTERISTICAS DE BUSQUEDA
                Modificar TOP
             */

            var db = dbConnection();

            var sql = @$"
                        SELECT TOP {parametros.Top} p.NombreProd, p.StockExistencia, um.DescripcionUnidad
                        FROM productos p
                        INNER JOIN unidades_medida um ON um.IdUnidad = p.UnidadMedida
                        GROUP BY p.NombreProd, p.StockExistencia, um.DescripcionUnidad
                        ORDER BY p.StockExistencia;
                        ";

            var result = await db.QueryAsync<StockProd>(sql, new { });
            return result;
        }
    }
}
