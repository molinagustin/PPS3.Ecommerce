namespace PPS3.Client.Services.ServPrivilegio
{
    public class ServPrivilegio : IServPrivilegio
    {
        private readonly HttpClient _httpClient;
        private  readonly ISessionStorageService _sessionStorage;

        public ServPrivilegio(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<Privilegio> ObtenerPrivilegio(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Privilegios/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var priv = await JsonSerializer.DeserializeAsync<Privilegio>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return priv;
            }
            else
                return null;            
        }

        public async Task<IEnumerable<Privilegio>> ObtenerPrivilegios()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Privilegios");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var privs = await JsonSerializer.DeserializeAsync<IEnumerable<Privilegio>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return privs;
            }
            else
                return null;
        }
    }
}
