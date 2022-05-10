#region Declaraciones Globales
global using System.Net.Http.Json;
global using System.Text;
global using System.Text.Json;
global using Blazored.SessionStorage;
global using PPS3.Shared.Models;
global using PPS3.Shared.InternalModels;
global using PPS3.Client.Services;
global using PPS3.Client.Services.ServProducto;
global using PPS3.Client.Services.ServUsuario;
global using PPS3.Client.Services.ServProveedor;
global using PPS3.Client.Services.ServUnidadMedida;
global using PPS3.Client.Services.ServRubro;
global using PPS3.Client.Services.ServTipoProducto;
global using PPS3.Client.Services.ServTipoVenta;
global using PPS3.Client.Services.ServTipoTarjeta;
global using PPS3.Client.Services.ServTipoDocumento;
global using PPS3.Client.Services.ServTipoComprobante;
global using PPS3.Client.Services.ServTarjeta;
global using PPS3.Client.Services.ServProvincia;
global using PPS3.Client.Services.ServPrivilegio;
global using PPS3.Client.Services.ServLocalidad;
global using PPS3.Client.Services.ServImagenProducto;
global using PPS3.Client.Services.ServFormaPago;
global using PPS3.Client.Services.ServEstadoCarroCompra;
global using PPS3.Client.Services.ServEncabezadoRecibo;
global using PPS3.Client.Services.ServEncabezadoPresupuesto;
global using PPS3.Client.Services.ServEncabezadoCuentaCorriente;
global using PPS3.Client.Services.ServEncabezadoComprobante;
global using PPS3.Client.Services.ServDetalleRecibo;
global using PPS3.Client.Services.ServDetalleCarroCompra;
global using PPS3.Client.Services.ServCuerpoPresupuesto;
global using PPS3.Client.Services.ServCuerpoComprobante;
global using PPS3.Client.Services.ServContactoProveedor;
global using PPS3.Client.Services.ServCondicionIVA;
global using PPS3.Client.Services.ServCliente;
global using PPS3.Client.Services.ServCarroCompra;
#endregion

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Servicios Propios
//Para las session storage
builder.Services.AddBlazoredSessionStorage();

//URL de la API para la inyeccion de los servicios HTTPS
var API_URL = new Uri("https://localhost:7022");

//Se inyectan los servicios HTTP del servicio creado (Producto) y se asigna la direccion URL a la que debe solicitar los request
builder.Services.AddHttpClient<IServProducto, ServProducto>( client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServUsuario, ServUsuario>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServProveedor, ServProveedor>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServUnidadMedida, ServUnidadMedida>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServRubro, ServRubro>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServTipoProducto, ServTipoProducto>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServTipoVenta, ServTipoVenta>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServTipoTarjeta, ServTipoTarjeta>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServTipoDocumento, ServTipoDocumento>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServTipoComprobante, ServTipoComprobante>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServTarjeta, ServTarjeta>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServProvincia, ServProvincia>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServPrivilegio, ServPrivilegio>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServLocalidad, ServLocalidad>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServImagenProducto, ServImagenProducto>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServFormaPago, ServFormaPago>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServEstadoCarroCompra, ServEstadoCarroCompra>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServEncabezadoRecibo, ServEncabezadoRecibo>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServEncabezadoPresupuesto, ServEncabezadoPresupuesto>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServEncabezadoCuentaCorriente, ServEncabezadoCuentaCorriente>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServEncabezadoComprobante, ServEncabezadoComprobante>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServDetalleRecibo, ServDetalleRecibo>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServDetalleCarroCompra, ServDetalleCarroCompra>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServCuerpoPresupuesto, ServCuerpoPresupuesto>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServCuerpoComprobante, ServCuerpoComprobante>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServContactoProveedor, ServContactoProveedor>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServCondicionIVA, ServCondicionIVA>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServCliente, ServCliente>(client => { client.BaseAddress = API_URL; });
builder.Services.AddHttpClient<IServCarroCompra, ServCarroCompra>(client => { client.BaseAddress = API_URL; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
