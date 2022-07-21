using PPS3.Shared.Models;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace PPS3.Client.Services.ServCarroCompra
{
    public class ServCarroCompra : IServCarroCompra
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServCarroCompra(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> GuardarCarro(CarroCompra carro)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var carroJson = new StringContent(JsonSerializer.Serialize(carro), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (carro.IdCarro > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/CarrosCompras/ActualizarCarroCompra");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = carroJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/CarrosCompras/CrearCarroCompra");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = carroJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<CarroCompra> ObtenerCarro(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CarrosCompras/ObtenerCarroCompra/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var carro = await JsonSerializer.DeserializeAsync<CarroCompra>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return carro;
            }
            else
                return null;
        }

        public async Task<CarroCompra> ObtenerCarroActivoUsuario(int idUsuario)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CarrosCompras/ObtenerCarroActivoUsuario/{idUsuario}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var carro = await JsonSerializer.DeserializeAsync<CarroCompra>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return carro;
            }
            else
                return null;
        }

        public async Task<IEnumerable<CarroCompra>> ObtenerCarros()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CarrosCompras/ObtenerCarrosCompras");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var carros = await JsonSerializer.DeserializeAsync<IEnumerable<CarroCompra>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return carros;
            }
            else
                return null;
        }

        public async Task<OrdenesCompraListado> ObtenerOCDetalle(int NumOrden)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CarrosCompras/ObtenerOCDetalle/{NumOrden}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var orden = await JsonSerializer.DeserializeAsync<OrdenesCompraListado>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (orden != null)
                {
                    var detalles = await ObtenerOCDetalles(orden.IdCarro);

                    if (detalles != null)
                    {
                        orden.DetallesCarro = detalles.ToList();
                        
                        return orden;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<IEnumerable<DetalleCarroCompra>> ObtenerOCDetalles(int NumOrden)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CarrosCompras/ObtenerOCDetalles/{NumOrden}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleCarroCompra>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return detalles;
            }
            else
                return null;
        }

        public async Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompra()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CarrosCompras/ObtenerOrdenesCompra");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var ordenes = await JsonSerializer.DeserializeAsync<IEnumerable<OrdenesCompraListado>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return ordenes;
            }
            else
                return null;
        }
    }
}
