﻿using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServProveedor
{
    public class ServProveedor : IServProveedor
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServProveedor(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }
        
        public async Task<bool> BorrarProveedor(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Proveedores/BorrarProveedor/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Verifico que la respuesta sea exitosa
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<int> GuardarProveedor(Proveedor proveedor)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return 0;

            //Se procede a Serializar el contenido del producto por parametro
            var provJson = new StringContent(JsonSerializer.Serialize(proveedor), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (proveedor.IdProveedor > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/Proveedores/ActualizarProveedor");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = provJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/Proveedores/CrearProveedor");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = provJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var id = await JsonSerializer.DeserializeAsync<int>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return id;
            }
            else
                return 0;
        }

        public async Task<Proveedor> ObtenerProveedor(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Proveedores/ObtenerProveedor/{id}");

            var proveedor = await JsonSerializer.DeserializeAsync<Proveedor>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return proveedor;
        }

        public async Task<IEnumerable<Proveedor>> ObtenerProveedores()
        {
            var response = await _httpClient.GetStreamAsync($"api/Proveedores/ObtenerProveedores");

            var proveedores = await JsonSerializer.DeserializeAsync<IEnumerable<Proveedor>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return proveedores;
        }

        public async Task<IEnumerable<ProveedorListado>> ObtenerProveedoresListado()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Proveedores/ObtenerProveedoresListado");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bites) y lo deserializo en un objeto apropiado (Enumerable de ProveedoresListado)
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var provs = await JsonSerializer.DeserializeAsync<IEnumerable<ProveedorListado>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return provs;
            }
            else
                return null;
        }
    }
}