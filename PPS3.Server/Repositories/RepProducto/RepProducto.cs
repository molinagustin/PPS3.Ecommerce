﻿using System.Text;

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
                            Bonificacion=@Bonificacion,
                            CantMinAlerta=@CantMinAlerta, 
                            StockExistencia=@StockExistencia,
                            Activo=@Activo,
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
                                                        producto.Bonificacion,
                                                        producto.CantMinAlerta, 
                                                        producto.StockExistencia,
                                                        producto.Activo,
                                                        producto.UsuarioModif, 
                                                        FechaUltModif = DateTime.Now, 
                                                        producto.IdProducto 
                                                        });
            return result > 0;
        }

        public async Task<bool> BorrarProducto(int id, int idUsu)
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
                                                        UsuarioModif = idUsu,
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
                                @StockExistencia,
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
                                                        producto.UsuarioCrea, 
                                                        producto.UsuarioModif });
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

        public async Task<Producto> ObtenerProducto(string nombreProd)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM productos
                        WHERE NombreProd = @NombreProd
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Producto>(sql, new { NombreProd = nombreProd });
            return result;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductos()
        {
            var db = dbConnection();
            //Se coloca el @ para que se pueda usar multiples lineas en la misma cadena, sino habria que concatenarla con +
            var sql = @"
                        SELECT  prod.IdProducto, prod.NombreProd, prod.Descripcion, r.DescRubro,                    tp.DescripcionTipo, prod.PrecioCosto, 
                                prod.PrecioFinal, prov.NombreProv, um.DescripcionUnidad, prod.Bonificacion, prod.CantMinAlerta,      prod.StockExistencia, u.NombreUs as UsuarioCrea, u2.NombreUs as UsuarioModif, 
                                prod.FechaCrea, prod.FechaUltModif
                        FROM productos as prod
                        INNER JOIN rubros as r ON prod.Rubro = r.IdRubro
                        INNER JOIN productos_tipos as tp ON prod.TipoProd = tp.IdTipo
                        INNER JOIN proveedores as prov ON prod.Proveedor = prov.IdProveedor
                        INNER JOIN unidades_medida as um ON prod.UnidadMedida = um.IdUnidad
                        INNER JOIN usuarios as u ON prod.UsuarioCrea = u.IdUsuarioAct
                        INNER JOIN usuarios as u2 ON prod.UsuarioModif = u2.IdUsuarioAct
                        WHERE prod.Activo = 1
                        ORDER BY prod.NombreProd
                        ";

            //El metodo QueryAsync va a devolver un IEnumerable con los datos del modelo que pasamos por parametro
            var result = await db.QueryAsync<ProductoListado>(sql, new { });
            return result;
        }

        public async Task<int> UltimoProductoCreado(int idUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT MAX(productos.IdProducto)
                        FROM productos
                        WHERE UsuarioCrea = @UsuarioCrea
                        ";
            var result = await db.ExecuteScalarAsync<int>(sql, new { UsuarioCrea = idUsuario });
            return result;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductosInactivos()
        {
            var db = dbConnection();
            //Se coloca el @ para que se pueda usar multiples lineas en la misma cadena, sino habria que concatenarla con +
            var sql = @"
                        SELECT  prod.IdProducto, prod.NombreProd, prod.Descripcion, r.DescRubro,                    tp.DescripcionTipo, prod.PrecioCosto, 
                                prod.PrecioFinal, prov.NombreProv, um.DescripcionUnidad, prod.Bonificacion, prod.CantMinAlerta,      prod.StockExistencia, u.NombreUs as UsuarioCrea, u2.NombreUs as UsuarioModif, 
                                prod.FechaCrea, prod.FechaUltModif
                        FROM productos as prod
                        INNER JOIN rubros as r ON prod.Rubro = r.IdRubro
                        INNER JOIN productos_tipos as tp ON prod.TipoProd = tp.IdTipo
                        INNER JOIN proveedores as prov ON prod.Proveedor = prov.IdProveedor
                        INNER JOIN unidades_medida as um ON prod.UnidadMedida = um.IdUnidad
                        INNER JOIN usuarios as u ON prod.UsuarioCrea = u.IdUsuarioAct
                        INNER JOIN usuarios as u2 ON prod.UsuarioModif = u2.IdUsuarioAct
                        WHERE prod.Activo = 0
                        ORDER BY prod.NombreProd
                        ";

            //El metodo QueryAsync va a devolver un IEnumerable con los datos del modelo que pasamos por parametro
            var result = await db.QueryAsync<ProductoListado>(sql, new { });
            return result;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerUltimos5Productos()
        {
            var db = dbConnection();
            //Se coloca el @ para que se pueda usar multiples lineas en la misma cadena, sino habria que concatenarla con +
            var sql = @"
                        SELECT TOP 8 prod.IdProducto, prod.NombreProd, prod.PrecioFinal, prod.Bonificacion, prod.FechaUltModif, prod.ImagenDestacada
                        FROM productos as prod
                        WHERE prod.Activo = 1
                        ORDER BY prod.FechaUltModif DESC
                        ";

            //El metodo QueryAsync va a devolver un IEnumerable con los datos del modelo que pasamos por parametro
            var result = await db.QueryAsync<ProductoListado>(sql, new { });
            return result;
        }

        public async Task<ProductoListado> ObtenerProductoListado(int id)
        {
            var db = dbConnection();
            //Se coloca el @ para que se pueda usar multiples lineas en la misma cadena, sino habria que concatenarla con +
            var sql = @"
                        SELECT  prod.IdProducto, prod.NombreProd, prod.Descripcion, r.DescRubro,                    tp.DescripcionTipo, prod.PrecioCosto, 
                                prod.PrecioFinal, prov.NombreProv, um.DescripcionUnidad, prod.Bonificacion, prod.StockExistencia
                        FROM productos as prod
                        INNER JOIN rubros as r ON prod.Rubro = r.IdRubro
                        INNER JOIN productos_tipos as tp ON prod.TipoProd = tp.IdTipo
                        INNER JOIN proveedores as prov ON prod.Proveedor = prov.IdProveedor
                        INNER JOIN unidades_medida as um ON prod.UnidadMedida = um.IdUnidad
                        WHERE prod.IdProducto = @IdProducto
                        ";

            //El metodo QueryAsync va a devolver un IEnumerable con los datos del modelo que pasamos por parametro
            var result = await db.QueryFirstOrDefaultAsync<ProductoListado>(sql, new { IdProducto = id });   
            return result;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductosCarro(int idCarro)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT  prod.IdProducto, prod.NombreProd, prod.Descripcion, prod.PrecioFinal, um.DescripcionUnidad, prod.Bonificacion, prod.ImagenDestacada
                        FROM productos as prod
                        INNER JOIN unidades_medida as um ON prod.UnidadMedida = um.IdUnidad
                        INNER JOIN carros_compras_detalles as ccd ON ccd.Producto = prod.IdProducto
                        WHERE ccd.Carro = @idCarro
                        ";

            //El metodo QueryAsync va a devolver un IEnumerable con los datos del modelo que pasamos por parametro
            var result = await db.QueryAsync<ProductoListado>(sql, new { idCarro });
            return result;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductosPorTipoProducto(int idTipoProd)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT prod.IdProducto, prod.NombreProd, prod.Descripcion, prod.PrecioFinal, prod.Bonificacion, prod.ImagenDestacada
                        FROM productos as prod
                        WHERE prod.Activo = 1 AND prod.TipoProd = @idTipoProd
                        ORDER BY prod.NombreProd
                        ";

            //El metodo QueryAsync va a devolver un IEnumerable con los datos del modelo que pasamos por parametro
            var result = await db.QueryAsync<ProductoListado>(sql, new { idTipoProd });
            return result;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductosPorBusqueda(string busqueda)
        {
            var db = dbConnection();

            var sql = @"SELECT prod.IdProducto, prod.NombreProd, prod.Descripcion, prod.PrecioFinal, prod.Bonificacion,            prod.ImagenDestacada 
                        FROM productos as prod 
                        WHERE prod.Activo = 1 AND prod.NombreProd LIKE '%" + busqueda + @"%' 
                        ORDER BY prod.NombreProd";

            //El metodo QueryAsync va a devolver un IEnumerable con los datos del modelo que pasamos por parametro
            var result = await db.QueryAsync<ProductoListado>(sql, new { });
            return result;
        }

        public async Task<bool> ActualizarStockProductos(List<StockProducto> productos)
        {
            var db = dbConnection();

            var cantProd = productos.Count();
            var cantProdAct = 0;

            foreach (var prod in productos)
            {
                var sql = "UPDATE productos SET StockExistencia=@StockExistencia WHERE IdProducto=@IdProducto;";
                cantProdAct += await db.ExecuteAsync(sql, new { prod.StockExistencia, prod.IdProducto});
            }     

            return (cantProd == cantProdAct);
        }
    }
}