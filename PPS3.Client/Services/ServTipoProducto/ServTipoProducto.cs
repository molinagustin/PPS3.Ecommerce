using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServTipoProducto
{
    public class ServTipoProducto : IServTipoProducto
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServTipoProducto(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }        
        
        public async Task<bool> BorrarTipoProd(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/TiposProductos/{id}");
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

        public async Task<bool> GuardarTipoProd(TipoProducto tipoProd)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del objeto por parametro
            var tipoProdJson = new StringContent(JsonSerializer.Serialize(tipoProd), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (tipoProd.IdTipo > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/TiposProductos");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = tipoProdJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/TiposProductos");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = tipoProdJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;            
        }

        public async Task<TipoProducto> ObtenerTipoProd(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/TiposProductos/{id}");

            var tipoProd = await JsonSerializer.DeserializeAsync<TipoProducto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return tipoProd;
        }

        public async Task<IEnumerable<TipoProducto>> ObtenerTiposProd()
        {
            var response = await _httpClient.GetStreamAsync("api/TiposProductos");

            var tiposProd = await JsonSerializer.DeserializeAsync<IEnumerable<TipoProducto>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return tiposProd;
        }
    }
}
