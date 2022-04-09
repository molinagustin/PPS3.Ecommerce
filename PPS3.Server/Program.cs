#region Declaraciones Globales
//Se colocan de forma global las referencias a usar en otros proyectos/carpetas, de esa forma todo el proyecto ya las tendra asignadas en un solo lugar
global using System.Data.SqlClient; //Para la conexion a la base de datos
global using Dapper; //Para que los repositorios usen Dapper
global using Microsoft.AspNetCore.Authorization; //Libreria que permite decoradores [Authorize] y demas opciones de autorizacion
global using PPS3.Shared.Models;
global using PPS3.Shared.InternalModels;
global using PPS3.Server.Data; //Para poder instanciar en todos los repositorios la clase SqlConfiguration
global using PPS3.Server.Repositories.RepProducto;
global using PPS3.Server.Repositories.RepProveedor;
global using PPS3.Server.Repositories.RepRubro;
global using PPS3.Server.Repositories.RepTipoProducto;
global using PPS3.Server.Repositories.RepUnidadMedida;
global using PPS3.Server.Repositories.RepUsuario;
global using PPS3.Server.Repositories.RepPrivilegio;
global using PPS3.Server.Repositories.RepCliente;
global using PPS3.Server.Repositories.RepProvincia;
global using PPS3.Server.Repositories.RepLocalidad;
global using PPS3.Server.Repositories.RepCondicionIVA;
global using PPS3.Server.Repositories.RepTipoDocumento;
global using PPS3.Server.Repositories.RepCarroCompra;
global using PPS3.Server.Repositories.RepEstadoCarroCompra;
global using PPS3.Server.Repositories.RepDetalleCarroCompra;
global using PPS3.Server.Repositories.RepTipoVenta;
global using PPS3.Server.Repositories.RepTipoComprobante;
global using PPS3.Server.Repositories.RepTipoTarjeta;
global using PPS3.Server.Repositories.RepTarjeta;
global using PPS3.Server.Repositories.RepContactoProveedor;
global using PPS3.Server.Repositories.RepImagenProducto;
global using PPS3.Server.Repositories.RepEncabezadoPresupuesto;
global using PPS3.Server.Repositories.RepCuerpoPresupuesto;
global using PPS3.Server.Repositories.RepFormaPago;
global using PPS3.Server.Repositories.RepEncabezadoCuentaCorriente;
global using PPS3.Server.Repositories.RepEncabezadoComprobante;
global using PPS3.Server.Repositories.RepCuerpoComprobante;
global using PPS3.Server.Repositories.RepEncabezadoRecibo;
global using PPS3.Server.Repositories.RepDetalleRecibo;
#endregion
using Microsoft.AspNetCore.Authentication.JwtBearer; //Para establecer la comunicacion de autenticacion
using Microsoft.IdentityModel.Tokens; //Para las configuraciones de los JWT Bearers
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Servicios Propios
//Conexion a la BD
var sqlConnectionString = new SqlConfiguration(builder.Configuration.GetConnectionString("SqlConnection"));
builder.Services.AddSingleton(sqlConnectionString); //Para inyectarlo en los servicios, se usa Singleton para que se intancie solo 1 vez

#region Inyeccion Servicios Repositorios de Datos
//Repositorios con acceso a datos
builder.Services.AddScoped<IRepProducto, RepProducto>();
builder.Services.AddScoped<IRepProveedor, RepProveedor>();
builder.Services.AddScoped<IRepRubro, RepRubro>();
builder.Services.AddScoped<IRepTipoProducto, RepTipoProducto>();
builder.Services.AddScoped<IRepUnidadMedida, RepUnidadMedida>();
builder.Services.AddScoped<IRepUsuario, RepUsuario>();
builder.Services.AddScoped<IRepPrivilegio, RepPrivilegio>();
builder.Services.AddScoped<IRepCliente, RepCliente>();
builder.Services.AddScoped<IRepProvincia, RepProvincia>();
builder.Services.AddScoped<IRepLocalidad, RepLocalidad>();
builder.Services.AddScoped<IRepCondicionIVA, RepCondicionIVA>();
builder.Services.AddScoped<IRepTipoDocumento, RepTipoDocumento>();
builder.Services.AddScoped<IRepCarroCompra, RepCarroCompra>();
builder.Services.AddScoped<IRepEstadoCarroCompra, RepEstadoCarroCompra>();
builder.Services.AddScoped<IRepDetalleCarroCompra, RepDetalleCarroCompra>();
builder.Services.AddScoped<IRepTipoVenta, RepTipoVenta>();
builder.Services.AddScoped<IRepTipoComprobante, RepTipoComprobante>();
builder.Services.AddScoped<IRepTipoTarjeta, RepTipoTarjeta>();
builder.Services.AddScoped<IRepTarjeta, RepTarjeta>();
builder.Services.AddScoped<IRepContactoProveedor, RepContactoProveedor>();
builder.Services.AddScoped<IRepImagenProducto, RepImagenProducto>();
builder.Services.AddScoped<IRepEncabezadoPresupuesto, RepEncabezadoPresupuesto>();
builder.Services.AddScoped<IRepCuerpoPresupuesto, RepCuerpoPresupuesto>();
builder.Services.AddScoped<IRepFormaPago, RepFormaPago>();
builder.Services.AddScoped<IRepEncabezadoCuentaCorriente, RepEncabezadoCuentaCorriente>();
builder.Services.AddScoped<IRepEncabezadoComprobante, RepEncabezadoComprobante>();
builder.Services.AddScoped<IRepCuerpoComprobante, RepCuerpoComprobante>();
builder.Services.AddScoped<IRepEncabezadoRecibo, RepEncabezadoRecibo>();
builder.Services.AddScoped<IRepDetalleRecibo, RepDetalleRecibo>();
#endregion

#region Servicio de Autenticacion
//Se usa el servicio de autenticacion de .net
builder.Services.AddAuthentication(
                //Se define la forma en que se va a comunicar la autenticacion (HTTP) y por que medio se va a tratar de comparar dicha autenticacion. Ambos por medio de token JWT
                x => {                
                     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                     }
                                  )
                //Adicionalmente a las formas de comunicacion, se agregan configuraciones de los tokens, como cual es el metodo de cifrado, la clave simetrica, si se guarda el token, entre otros.
                .AddJwtBearer(
                x => {
                     x.RequireHttpsMetadata = false;
                     x.SaveToken = true; //Guardar token para compararlo luego
                     x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        //Se crea una nueva clave simetrica con la cadena especial guardada en los archivos de configuracion
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ClaveSimetrica:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                                                                                 };
                     }
                              );
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //Middleware que permite la autenticacion
app.UseAuthorization(); //Middleware que permite, una vez esté autenticado el usuario, autorizar el uso de determinados recursos en base a las rutas definidas

app.MapControllers();

app.Run();
