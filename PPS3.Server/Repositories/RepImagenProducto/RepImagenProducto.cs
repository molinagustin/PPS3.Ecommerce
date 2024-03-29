﻿using PPS3.Shared.Models;

namespace PPS3.Server.Repositories.RepImagenProducto
{
    public class RepImagenProducto : IRepImagenProducto
    {
        private string _connectionString;

        public RepImagenProducto(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        //Actualizar datos de una imagen, ya sea si es principal, si se cambia la imagen u otro.
        public async Task<bool> ActualizarImagen(ImagenProducto imagenProducto)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE productos_imagenes
                        SET
                            IdProducto = @IdProducto,
                            Contenido = @Contenido,
                            Principal = @Principal,
                            UsuarioModif = @UsuarioModif,
                            FechaUltModif = @FechaUltModif
                        WHERE IdImg = @IdImg
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        imagenProducto.IdProducto,
                                                        imagenProducto.Contenido,
                                                        imagenProducto.Principal,
                                                        imagenProducto.UsuarioModif,
                                                        FechaUltModif = DateTime.Now,
                                                        imagenProducto.IdImg
                                                        });
            return result > 0;
        }

        //Borrado definitivo de la imagen
        public async Task<bool> BorrarImagen(int id)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE 
                        FROM productos_imagenes 
                        WHERE IdImg = @IdImg
                        ";
            var result = await db.ExecuteAsync(sql, new { IdImg = id });
            return result > 0;
        }

        //Guardar una imagen en la base de datos
        public async Task<bool> InsertarImagen(ImagenProducto imagenProducto)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO productos_imagenes  (
                                                        IdProducto,
                                                        Contenido,
                                                        UsuarioCrea,
                                                        UsuarioModif
                                                        )
                        VALUES  (
                                @IdProducto,
                                @Contenido,
                                @UsuarioCrea,
                                @UsuarioModif
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        imagenProducto.IdProducto,
                                                        imagenProducto.Contenido,
                                                        imagenProducto.UsuarioCrea,
                                                        imagenProducto.UsuarioModif
                                                        });
            return result > 0;
        }

        //Obtener una imagen en particular
        public async Task<ImagenProducto> ObtenerImagen(int idImagen)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM productos_imagenes
                        WHERE IdImg = @IdImg
                        ";
            var result = await db.QueryFirstOrDefaultAsync<ImagenProducto>(sql, new { IdImg = idImagen });
            return result;
        }

        //Obtener todas las imagenes relacionadas a un producto
        public async Task<IEnumerable<ImagenProducto>> ObtenerImagenes(int idProducto)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM productos_imagenes
                        WHERE IdProducto = @IdProducto
                        ORDER BY Principal DESC
                        ";
            var result = await db.QueryAsync<ImagenProducto>(sql, new { IdProducto = idProducto });
            return result;
        }

        public async Task<IEnumerable<ImagenProducto>> ObtenerUltimasImagenes()
        {
            var db = dbConnection();

            //La consulta selecciona los ultimos 5 registros creados
            var sql = @"
                        SELECT TOP 5 *
                        FROM productos_imagenes
                        ORDER BY productos_imagenes.FechaUltModif DESC";

            var result = await db.QueryAsync<ImagenProducto>(sql, new { });
            return result;
        }

        public async Task<bool> ImagenFavorita(ImagenProducto imagenFav)
        {
            var db = dbConnection();
            
            //Primero quito cualquier imagen favorita que haya
            var sql = @"
                        UPDATE productos_imagenes
                        SET
                            Principal = 0
                        WHERE IdProducto = @IdProducto
                        ";
            var result = await db.ExecuteAsync(sql, new{ imagenFav.IdProducto });

            //Si es exitoso, actualizo la imagen que quiero que sea favorita
            if (result > 0)
            {
                var sqlFav = @"
                                UPDATE productos_imagenes
                                SET
                                    Principal = 1,
                                    UsuarioModif = @UsuarioModif,
                                    FechaUltModif = @FechaUltModif
                                WHERE IdImg = @IdImg
                                ";
                var resultFav = await db.ExecuteAsync(sqlFav, new { 
                                                                imagenFav.UsuarioModif,
                                                                FechaUltModif = DateTime.Now,
                                                                imagenFav.IdImg
                                                                });
                //Si se actualiza correctamente, añado dicha imagen al producto en cuestion para mostrarla en el inicio
                if (resultFav > 0)
                {
                    var sqlImgProd = @"
                                        UPDATE productos
                                        SET
                                            ImagenDestacada = @Contenido
                                        WHERE IdProducto = @IdProducto
                                        ";
                    var resultImgProd = await db.ExecuteAsync(sqlImgProd, new 
                    {
                        imagenFav.Contenido,
                        imagenFav.IdProducto
                    });

                    return resultImgProd > 0;
                }
                else
                    return false;
            }

            return false;
        }
    }
}