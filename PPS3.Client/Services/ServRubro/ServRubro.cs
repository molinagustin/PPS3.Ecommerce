using PPS3.Client.Pages.Presupuestos;

namespace PPS3.Client.Services.ServRubro
{
    public class ServRubro : IServRubro
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServRubro(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }
                
        public async Task<bool> BorrarRubro(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Rubros/BorrarRubro/{id}");
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

        public async Task<int> GuardarRubro(Rubro rubro)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return 0;

            //Se procede a Serializar el contenido del producto por parametro
            var rubJson = new StringContent(JsonSerializer.Serialize(rubro), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (rubro.IdRubro > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/Rubros/ActualizarRubro");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = rubJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/Rubros/CrearRubro");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = rubJson;
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

        public async Task<Rubro> ObtenerRubro(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Rubros/ObtenerRubro/{id}");

            var rubro = await JsonSerializer.DeserializeAsync<Rubro>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return rubro;
        }

        public async Task<IEnumerable<Rubro>> ObtenerRubros()
        {
            var response = await _httpClient.GetStreamAsync($"api/Rubros/ObtenerRubros");

            var rubros = await JsonSerializer.DeserializeAsync<IEnumerable<Rubro>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return rubros;
        }

        public async Task<IEnumerable<RubroCategoria>> ObtenerRubrosCategorias()
        {
            var response = await _httpClient.GetStreamAsync($"api/Rubros/ObtenerRubrosCategorias");

            var rubros = await JsonSerializer.DeserializeAsync<IEnumerable<RubroCategoria>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (rubros != null)
            {
                var categorias = await ObtenerTiposProductosCategorias();

                if (categorias != null)
                {
                    //Creo la lista donde se guardaran los rubros
                    var listaRubros = new List<RubroCategoria>();

                    foreach (var rub in rubros)
                    {
                        rub.Categorias = categorias.Where(x => x.Rubro == rub.IdRubro).ToList();
                        listaRubros.Add(rub);
                    }

                    return listaRubros;
                }
                else
                    return null;
            }
            else
                return null;            
        }

        public async Task<IEnumerable<RubroListado>> ObtenerRubrosListado()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Rubros/ObtenerRubrosListado");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bites) y lo deserializo en un objeto apropiado (Enumerable de rubros)
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var rubros = await JsonSerializer.DeserializeAsync<IEnumerable<RubroListado>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return rubros;
            }
            else
                return null;
        }

        public async Task<IEnumerable<TipoProductoCategoria>> ObtenerTiposProductosCategorias()
        {
            var response = await _httpClient.GetStreamAsync($"api/Rubros/ObtenerTiposProductosCategorias");

            var categorias = await JsonSerializer.DeserializeAsync<IEnumerable<TipoProductoCategoria>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return categorias;
        }
    }
}
