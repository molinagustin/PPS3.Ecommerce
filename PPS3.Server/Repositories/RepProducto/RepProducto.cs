namespace PPS3.Server.Repositories.RepProducto
{
    public class RepProducto : IRepProducto
    {
        //Se crea un campo para almacenar la cadena de conexion, que va a ser obtenida desde el constructor
        private string _connectionString = string.Empty;

        public RepProducto(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        //Para abrir la conexion, se usa la funcion SqlConnection de la libreria System.Data.SqlClient donde devuelve la misma conexion pero a la cual se le asigna la cadena de conexion apropiada.
        protected SqlConnection dbConnection() 
        {
            return new SqlConnection(_connectionString);
        } 

        public async Task<bool> ActualizarProducto(Producto producto)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE productos
                        SET 
                            NombreProd=@NombreProd, 
                            Descripcion=@Descripcion, 
                            Rubro=@Rubro, 
                            TipoProd=@TipoProd, 
                            PrecioCosto=@PrecioCosto, 
                            PrecioFinal=@PrecioFinal, 
                            Proveedor=@Proveedor, 
                            UnidadMedida=@UnidadMedida, 
                            CantMinAlerta=@CantMinAlerta, 
                            StockExistencia=@StockExistencia,
                            Stockeable=@Stockeable, 
                            UsuarioModif=@UsuarioModif, 
                            FechaUltModif=@FechaUltModif
                            
                        WHERE IdProducto = @IdProducto
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        producto.NombreProd, 
                                                        producto.Descripcion, 
                                                        producto.Rubro, 
                                                        producto.TipoProd, 
                                                        producto.PrecioCosto, 
                                                        producto.PrecioFinal, 
                                                        producto.Proveedor, 
                                                        producto.UnidadMedida, 
                                                        producto.CantMinAlerta, 
                                                        producto.StockExistencia,
                                                        producto.Stockeable, 
                                                        UsuarioModif = 1, 
                                                        FechaUltModif = DateTime.Now, 
                                                        producto.IdProducto 
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarProducto(int id)
        {
            var db = dbConnection();

            var sql = @"UPDATE productos 
                        SET 
                            Activo=0,
                            UsuarioModif=@UsuarioModif,
                            FechaUltModif=@FechaUltModif
                        WHERE IdProducto = @IdProducto
                        ";

            var result = await db.ExecuteAsync(sql, new { 
                                                        UsuarioModif = 1,
                                                        FechaUltModif = DateTime.Now,
                                                        IdProducto = id
                                                        });

            return result > 0;
        }

        public async Task<bool> InsertarProducto(Producto producto)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO productos (
                                NombreProd, 
                                Descripcion, 
                                Rubro, 
                                TipoProd, 
                                PrecioCosto, 
                                PrecioFinal, 
                                Proveedor, 
                                UnidadMedida, 
                                CantMinAlerta,
                                StockExistencia,
                                Stockeable, 
                                UsuarioCrea, 
                                UsuarioModif
                                )
                        VALUES (
                                @NombreProd, 
                                @Descripcion, 
                                @Rubro, 
                                @TipoProd, 
                                @PrecioCosto, 
                                @PrecioFinal, 
                                @Proveedor, 
                                @UnidadMedida, 
                                @CantMinAlerta, 
                                @StockExistencia
                                @Stockeable, 
                                @UsuarioCrea, 
                                @UsuarioModif
                                )";

            //El metodo ExecuteAsync ejecuta una query sql y devuelve las filas afectadas
            var result = await db.ExecuteAsync(sql, new { 
                                                        producto.NombreProd, 
                                                        producto.Descripcion, 
                                                        producto.Rubro, 
                                                        producto.TipoProd, 
                                                        producto.PrecioCosto, 
                                                        producto.PrecioFinal, 
                                                        producto.Proveedor, 
                                                        producto.UnidadMedida, 
                                                        producto.CantMinAlerta, 
                                                        producto.StockExistencia,
                                                        producto.Stockeable, 
                                                        UsuarioCrea = 1, 
                                                        UsuarioModif = 1 });
            return result > 0;
        }

        public async Task<Producto> ObtenerProducto(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT * 
                        FROM productos 
                        WHERE IdProducto = @IdProducto
                        ";

            var result = await db.QueryFirstOrDefaultAsync<Producto>(sql, new { IdProducto = id });
            return result;
        }      

        public async Task<IEnumerable<Producto>> ObtenerProductos()
        {
            var db = dbConnection();
            //Se coloca el @ para que se pueda usar multiples lineas en la misma cadena, sino habria que concatenarla con +
            var sql = @"SELECT * 
                        FROM productos
                        ";

            //El metodo QueryAsync va a devolver un IEnumerable con los datos del modelo que pasamos por parametro
            var result = await db.QueryAsync<Producto>(sql, new { });
            return result;
        }
    }
}
