#region Declaraciones Globales
global using System.Net.Http.Json;
global using PPS3.Shared.Models;
global using PPS3.Shared.InternalModels;
global using PPS3.Client.Services;
global using PPS3.Client.Services.ServProducto;
#endregion

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Servicios Propios
//Se inyectan los servicios HTTP del servicio creado (Producto) y se asigna la direccion URL a la que debe solicitar los request
builder.Services.AddHttpClient<IServProducto, ServProducto>( 
    client => { client.BaseAddress = new Uri("https://localhost:7022"); } 
                                                           );

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
