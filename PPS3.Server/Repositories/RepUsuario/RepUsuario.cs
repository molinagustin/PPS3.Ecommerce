﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PPS3.Shared.InternalModels;
using PPS3.Shared.Models;
using System.Security.Cryptography;
using System.Text;

namespace PPS3.Server.Repositories.RepUsuario
{
    public class RepUsuario : IRepUsuario
    {
        private string _connectionString;

        public RepUsuario(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE usuarios
                        SET
                            NombreCompleto = @NombreCompleto,
                            NombreUs = @NombreUs,
                            Privilegio = @Privilegio,
                            Email = @Email,
                            EmailVerificado = @EmailVerificado,
                            Activo = @Activo,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUsuarioAct = @IdUsuarioAct
                        ";

            var result = await db.ExecuteAsync(sql, new {
                usuario.NombreCompleto,
                usuario.NombreUs,
                usuario.Privilegio,
                usuario.Email,
                usuario.EmailVerificado,
                usuario.Activo,
                FechaUltModif = DateTime.Now,
                usuario.IdUsuarioAct
            });
            return result > 0;
        }

        public async Task<bool> BorrarUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE usuarios
                        SET
                            Activo = 0,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUsuarioAct = @IdUsuarioAct
                        ";

            var result = await db.ExecuteAsync(sql, new {
                FechaUltModif = DateTime.Now,
                IdUsuarioAct = id
            });
            return result > 0;
        }

        public async Task<int> CrearUsuario(UsuarioCliente usuarioCliente)
        {
            var db = dbConnection();

            //Uso los metodos internos para crear un nuevo salt y su hash correspondiente
            var salt = CrearSalt();
            var hash = CrearHash(usuarioCliente.Password, salt);

            //El privilegio esta por defecto en la base de datos que sea para cliente, si se quiere un usuario administrador, lo tiene que modificar un usuario con privilegios adecuados
            var sql = @"
                        INSERT INTO usuarios(
                                            NombreCompleto,
                                            NombreUs,
                                            SaltCont,
                                            HashCont,
                                            IdCliente,
                                            Email
                                            )
                        VALUES (
                                @NombreCompleto,
                                @NombreUs,
                                @SaltCont,
                                @HashCont,
                                @IdCliente,
                                @Email
                                )
                        ";

            var result = await db.ExecuteAsync(sql, new {
                usuarioCliente.NombreCompleto,
                usuarioCliente.NombreUs,
                SaltCont = salt,
                HashCont = hash,
                usuarioCliente.IdCliente,
                usuarioCliente.Email
            });

            if (result > 0)
            {
                //Obtengo el ID del nuevo usuario creado
                var nuevoId = await ObtenerUltimoIdCreado(usuarioCliente.NombreUs);
                if (nuevoId > 0)
                {
                    //Le creo un carro de compras nuevo a ese usuario
                    var carroCreado = await CrearCarroNuevoUsuario(nuevoId);
                    //Si se creo correctamente el carro, devuelvo el Id del Usuario para que pueda utilizarlo en otras llamadas
                    if (carroCreado)
                        return nuevoId;
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        public async Task<Usuario> ObtenerUsuario(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios
                        WHERE IdUsuarioAct = @IdUsuarioAct
                        ";

            var result = await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { IdUsuarioAct = id });
            return result;
        }

        public async Task<Usuario> ObtenerUsuario(string nombreUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios
                        WHERE NombreUs = @NombreUs
                        ";

            var result = await db.QueryFirstOrDefaultAsync<Usuario>(sql, new { NombreUs = nombreUsuario });
            return result;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios
                        ";

            var result = await db.QueryAsync<Usuario>(sql, new { });
            return result;
        }

        private static string CrearSalt()
        {
            //Creo un array de bytes, es lo mismo 128/8 que 16 bytes
            byte[] randomBytes = new byte[128 / 8];
            //Creo un RNG de la libreria Cryptography para que llene de Bytes el array creado anteriormente. Luego devuelvo el array lleno de bytes al azar convertido en string para que pueda utlizarlo o almancenarlo en la base de datos
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        private static string CrearHash(string pass, string salt)
        {
            //Genero un nuevo array de Bytes que seran los correspondientes al Hasheo de la contraseña y el salt del usuario
            var valueBytes = KeyDerivation.Pbkdf2(
                                                    password: pass,
                                                    //Vuelvo a convertir a Bytes el Salt correspondiente.
                                                    salt: Encoding.UTF8.GetBytes(salt),
                                                    //El metodo de Hasheo
                                                    prf: KeyDerivationPrf.HMACSHA512,
                                                    iterationCount: 10000,
                                                    //La cantidad de bytes que va a tener el array devuelto por la funcion. Es lo mismo 256/8 que 32 bytes
                                                    numBytesRequested: 256 / 8
                                                  );

            return Convert.ToBase64String(valueBytes);
        }

        public bool ValidarHash(string pass, string salt, string hash)
        {
            //Comparo si el hash del usuario es igual al hash generado con la contraseña de la persona que trata de iniciar sesion
            return CrearHash(pass, salt) == hash;
        }

        /// <summary>
        /// Metodo que verifica si ya existe un usuario con el Nombre de Usuario que se esta intentando utilizar
        /// </summary>
        /// <param name="nombreUsuario">Nombre Usuario a crear</param>
        /// <returns></returns>
        public async Task<bool> UsuarioExistente(string nombreUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios
                        WHERE NombreUs = @NombreUs
                        ";

            var result = await db.ExecuteScalarAsync<int>(sql, new { NombreUs = nombreUsuario });
            return result > 0;
        }

        public async Task<int> ObtenerUltimoIdCreado(string NombreUs)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT MAX(IdUsuarioAct)
                        FROM usuarios
                        WHERE NombreUs = @NombreUs
                        ";
            //Uso ExecuteScalar para obtener solo el ID del cliente. Tambien le especifico de que tipo es el dato que quiero obtener
            var result = await db.ExecuteScalarAsync<int>(sql, new { NombreUs });
            return result;
        }

        public async Task<bool> CrearCarroNuevoUsuario(int idUsuario)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO carros_compras(
                                            UsuarioCarro
                                            )
                        VALUES (
                                @UsuarioCarro
                                )
                        ";

            var result = await db.ExecuteAsync(sql, new{ UsuarioCarro = idUsuario });

            return result > 0;
        }

        public async Task<bool> ActualizarPerfilUsuario(UsuarioCliente usuario)
        {
            //Actualizo los datos del usuario
            var db = dbConnection();

            var sql = @"
                        UPDATE usuarios
                        SET
                            NombreCompleto = @NombreCompleto,
                            Email = @Email,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUsuarioAct = @IdUsuarioAct
                        ";

            var result = await db.ExecuteAsync(sql, new
            {
                usuario.NombreCompleto,
                usuario.Email,
                FechaUltModif = DateTime.Now,
                usuario.IdUsuarioAct
            });

            //Si se actualizaron correctamente, actualizo los datos del cliente
            if (result > 0)
            {
                sql = "";
                sql = @"
                        UPDATE clientes
                        SET
                            TipoCliente=@TipoCliente,
                            Genero=@Genero,
                            TipoDocumento=@TipoDocumento,
                            NumDocumento=@NumDocumento,
                            NombreCompleto=@NombreCompleto,
                            CondIva=@CondIva,
                            DomicilioC=@DomicilioC,
                            Telefono=@Telefono,
                            LocalidadC=@LocalidadC,
                            FechaUltModif=@FechaUltModif
                        WHERE IdCliente=@IdCliente
                        ";
                
                var resultCl = await db.ExecuteAsync(sql, new
                {
                    usuario.TipoCliente,
                    usuario.Genero,
                    usuario.TipoDocumento,
                    usuario.NumDocumento,
                    usuario.NombreCompleto,
                    usuario.CondIva,
                    usuario.DomicilioC,
                    usuario.Telefono,
                    usuario.LocalidadC,
                    FechaUltModif = DateTime.Now,
                    usuario.IdCliente
                });

                //Devuelvo la comprobacion de si fueron actualizados los datos
                return resultCl > 0;
            }
            else
                return false;
        }

        public async Task<bool> CambiarPassword(UsuarioCliente usuarioCliente)
        {
            var nuevoHash = CrearHash(usuarioCliente.CambioPassword, usuarioCliente.SaltCont);

            if (string.IsNullOrEmpty(nuevoHash))
                return false;

            usuarioCliente.HashCont = nuevoHash;

            var db = dbConnection();

            var sql = @"
                        UPDATE usuarios
                        SET
                            HashCont = @HashCont,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUsuarioAct = @IdUsuarioAct
                        ";

            var result = await db.ExecuteAsync(sql, new
            {
                usuarioCliente.HashCont,
                FechaUltModif = DateTime.Now,
                usuarioCliente.IdUsuarioAct
            });
            return result > 0;
        }

        public async Task<bool> ValidarEmailUsuario(int id)
        {
            //Actualizo los datos del usuario
            var db = dbConnection();

            var sql = @"
                        UPDATE usuarios
                        SET
                            EmailVerificado = 1,
                            FechaUltModif = @FechaUltModif
                        WHERE IdUsuarioAct = @id
                        ";

            var result = await db.ExecuteAsync(sql, new
            {
                FechaUltModif = DateTime.Now,
                id
            });

            return result > 0;
        }
    }
}