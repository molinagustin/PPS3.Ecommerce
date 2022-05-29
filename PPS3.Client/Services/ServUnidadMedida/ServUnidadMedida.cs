﻿namespace PPS3.Client.Services.ServUnidadMedida
{
    public class ServUnidadMedida : IServUnidadMedida
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServUnidadMedida(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        } 
        
        public async Task<bool> BorrarUnidad(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/UnidadesMedida/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Verifico que la respuesta sea exitosa
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;

            //var result = await _httpClient.DeleteAsync($"api/UnidadesMedida/{id}");

            //if (result.IsSuccessStatusCode)
            //    return true;
            //else
            //    return false;
        }

        public async Task<bool> GuardarUnidad(UnidadMedida unidad)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            var uniJson = new StringContent(JsonSerializer.Serialize(unidad), Encoding.UTF8, "application/json");

            var response = new HttpResponseMessage();

            if(unidad.IdUnidad > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/UnidadesMedida");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = uniJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/UnidadesMedida");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = uniJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;

            //if (unidad.IdUnidad > 0)
            //    response = await _httpClient.PutAsync($"api/UnidadesMedida", uniJson);
            //else
            //    response = await _httpClient.PostAsync($"api/UnidadesMedida", uniJson);
        }

        public async Task<UnidadMedida> ObtenerUnidad(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/UnidadesMedida/{id}");

            var unidad = await JsonSerializer.DeserializeAsync<UnidadMedida>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return unidad;
        }

        public async Task<IEnumerable<UnidadMedida>> ObtenerUnidades()
        {
            var response = await _httpClient.GetStreamAsync($"api/UnidadesMedida");

            var unidades = await JsonSerializer.DeserializeAsync<IEnumerable<UnidadMedida>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return unidades;
        }
    }
}
