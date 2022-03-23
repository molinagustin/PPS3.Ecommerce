//Se colocan de forma global las referencias a usar en otros proyectos/carpetas, de esa forma todo el proyecto ya las tendra asignadas en un solo lugar
global using PPS3.Shared.Models;
global using System.Data.SqlClient; //Para la conexion a la base de datos
global using PPS3.Server.Data; //Para poder instanciar en todos los repositorios la clase SqlConfiguration
global using Dapper; //Para que los repositorios usen Dapper
global using PPS3.Server.Repositories.RepProducto;
global using PPS3.Server.Repositories.RepProveedor;
global using PPS3.Server.Repositories.RepRubro;
global using PPS3.Server.Repositories.RepTipoProducto;
global using PPS3.Server.Repositories.RepUnidadMedida;
global using PPS3.Server.Repositories.RepUsuario;

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

//Repositorios con acceso a datos
builder.Services.AddScoped<IRepProducto, RepProducto > ();
builder.Services.AddScoped<IRepProveedor, RepProveedor > ();
builder.Services.AddScoped<IRepRubro, RepRubro > ();
builder.Services.AddScoped<IRepTipoProducto, RepTipoProducto>();
builder.Services.AddScoped<IRepUnidadMedida, RepUnidadMedida>();
builder.Services.AddScoped<IRepUsuario, RepUsuario>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
