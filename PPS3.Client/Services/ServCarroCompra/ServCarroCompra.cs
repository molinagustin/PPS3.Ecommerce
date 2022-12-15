
using static MudBlazor.CategoryTypes;

namespace PPS3.Client.Services.ServCarroCompra
{
    public class ServCarroCompra : IServCarroCompra
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;
        private readonly string format = "image/png";

        public ServCarroCompra(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> BajaComprobanteCarro(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (string.IsNullOrEmpty(token)) return false;

            var request = new HttpRequestMessage(HttpMethod.Put, $"api/CarrosCompras/BajaComprobanteCarro/{id}");
            request.Headers.Add("Authorization", "Bearer " + token);

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<bool> GuardarCarro(CarroCompra carro)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (string.IsNullOrEmpty(token)) return false;

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
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/CarrosCompras/CrearCarroCompra?idUsuario={carro.UsuarioCarro}");
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

                if (detalles != null)
                {
                    foreach (var det in detalles)
                    {
                        if (det.ImagenDestacada != null)
                            det.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(det.ImagenDestacada)}";
                    }

                    return detalles;
                }
                else
                    return null;                
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

        public async Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompraComprobantes()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CarrosCompras/ObtenerOrdenesCompraComprobantes");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var ordenes = await JsonSerializer.DeserializeAsync<IEnumerable<OrdenesCompraListado>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (ordenes != null && ordenes.Count() > 0)
                {
                    //Voy a realizar una consulta para traer solo los detalles de los carros devueltos
                    var carros = new List<int>();
                    foreach (var od in ordenes)
                        carros.Add(od.IdCarro);

                    //Solicito los detalles al controlador
                    var carrosJson = new StringContent(JsonSerializer.Serialize(carros), Encoding.UTF8, "application/json");

                    var requestPost = new HttpRequestMessage(HttpMethod.Post, $"api/CarrosCompras/ObtenerOCDetallesComprobantes"); 
                    requestPost.Headers.Add("Authorization", "Bearer " + token);
                    requestPost.Content = carrosJson;

                    var responsePost = await _httpClient.SendAsync(requestPost, HttpCompletionOption.ResponseContentRead);

                    if (responsePost.IsSuccessStatusCode)
                    {
                        //Formateo si es posible cada imagen destacada de los detalles
                        var streamDetalles = await responsePost.Content.ReadAsStreamAsync();

                        var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleCarroCompra>>(streamDetalles, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                        if (detalles != null)
                        {
                            foreach (var det in detalles)
                            {
                                if (det.ImagenDestacada != null)
                                    det.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(det.ImagenDestacada)}";
                            }

                            //Guardo cada detalle en la orden correspondiente
                            foreach (var ord in ordenes)
                                ord.DetallesCarro = detalles.Where(x => x.Carro == ord.IdCarro).ToList();

                            return ordenes;
                        }
                        else return null;
                    }
                    else return null;
                }
                else return new List<OrdenesCompraListado>();
            }
            else return null;
        }

        public async Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompraUsuario(int idUsuario)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CarrosCompras/ObtenerOrdenesCompraUsuario/{idUsuario}");
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
