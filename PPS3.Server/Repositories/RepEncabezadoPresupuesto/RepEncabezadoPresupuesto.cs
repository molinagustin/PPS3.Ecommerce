namespace PPS3.Server.Repositories.RepEncabezadoPresupuesto
{
    public class RepEncabezadoPresupuesto : IRepEncabezadoPresupuesto
    {
        private string _connectionString;

        public RepEncabezadoPresupuesto(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> InsertarPresupuesto(EncabezadoPresupuesto encabezadoPresupuesto)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO presupuestos_encabezados(
                                                            Observaciones,
                                                            UsuarioCrea
                                                            )
                        VALUES  (
                                @Observaciones,
                                @UsuarioCrea
                                )                        
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        encabezadoPresupuesto.Observaciones,
                                                        encabezadoPresupuesto.UsuarioCrea
                                                        });

            //Si se creo correctamente el registro, busco su ID para devolverlo
            if (result > 0)
            {
                var numPres = await ObtenerUltimoIdCreado(encabezadoPresupuesto.UsuarioCrea);
                if (numPres > 0)
                    return numPres;
                else
                    return 0;
            }
            else
                return 0;
        }

        private async Task<int> ObtenerUltimoIdCreado(int idUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT MAX(presupuestos_encabezados.NumPresu)
                        FROM presupuestos_encabezados
                        WHERE UsuarioCrea = @UsuarioCrea
                        ";
            //Uso ExecuteScalar para obtener solo el ID del presupuesto. 
            var result = await db.ExecuteScalarAsync<int>(sql, new { UsuarioCrea = idUsuario });

            if (result > 0)
                return result;
            else
                return 0;
        }

        public async Task<EncabezadoPresupuesto> ObtenerPresupuesto(int numPres)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM presupuestos_encabezados
                        WHERE NumPresu = @NumPresu
                        ";
            var result = await db.QueryFirstOrDefaultAsync<EncabezadoPresupuesto>(sql, new { NumPresu = numPres });
            return result;
        }

        public async Task<IEnumerable<EncabezadoPresupuesto>> ObtenerTodosPresupuestos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM presupuestos_encabezados
                        ";
            var result = await db.QueryAsync<EncabezadoPresupuesto>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<Presupuesto>> ObtenerPresupuestosList()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT pe.NumPresu, pe.Observaciones, u.NombreUs as UsuarioCrea, pe.FechaCrea
                        FROM presupuestos_encabezados as pe
                        INNER JOIN usuarios as u ON pe.UsuarioCrea = u.IdUsuarioAct
                        ORDER BY pe.NumPresu
                        ";
            var result = await db.QueryAsync<Presupuesto>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<DetallePresupuesto>> ObtenerDetallesPresupuestosList()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT pc.IdCuerpoPres, pc.IdEncab, pc.Producto, p.NombreProd as NombreProducto, pc.Cantidad, um.DescripcionUnidad,
                        pc.PrecioUnit, pc.Bonificacion, pc.BonificacionTotal, pc.SubTotal
                        FROM presupuestos_cuerpos as pc
                        INNER JOIN productos as p ON pc.Producto = p.IdProducto
                        INNER JOIN unidades_medida as um ON p.UnidadMedida = um.IdUnidad
                        ORDER BY pc.IdEncab
                        ";
            var result = await db.QueryAsync<DetallePresupuesto>(sql, new { });
            return result;
        }
    }
}
