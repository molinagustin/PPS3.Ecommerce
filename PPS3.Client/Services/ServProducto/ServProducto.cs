using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServProducto
{
    public class ServProducto : IServProducto
    {
        //Se comienza por crear el campo que se utilizara para los HTTP request, el cual ya trae configurado .NET CORE
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;
        private readonly string format = "image/png";

        public ServProducto(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> ActualizarStockProductos(List<StockProducto> productos)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (string.IsNullOrEmpty(token)) return false;

            //Se procede a Serializar el contenido del producto por parametro
            var productosJson = new StringContent(JsonSerializer.Serialize(productos), Encoding.UTF8, "application/json");
                        
            //Creo una solicitud Http de tipo POST
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Productos/ActualizarStockProductos");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            //Agrego el JSON al BODY
            request.Content = productosJson;
            //Envio la solicitud HTTP
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<bool> BorrarProducto(int id, int idUsu)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (string.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Productos/BorrarProducto/{id}?idUsu={idUsu}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            
            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Verifico que la respuesta sea exitosa
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;

            /*
            //Se usa el metodo del objeto httpClient que directamente llama a una solicitud DELETE y se le pasa la ruta hacia la API, la accion y el ID
            var result = await _httpClient.DeleteAsync($"api/Productos/BorrarProducto/{id}");
            */
        }

        public async Task<bool> GuardarProducto(Producto producto)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var productoJson = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json" );

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if(producto.IdProducto > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/Productos/ActualizarProducto");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = productoJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/Productos/CrearProducto");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = productoJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);  
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;

            /*
            HttpResponseMessage result = new HttpResponseMessage();

            //Se verifica si es un INSERT o UPDATE
            if (producto.IdProducto > 0)
                result = await _httpClient.PutAsync($"api/Productos/ActualizarProducto", productoJson); 
            else            
                result = await _httpClient.PostAsync($"api/Productos/CrearProducto", productoJson);           
            */
        }

        public async Task<Producto> ObtenerProducto(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProducto/{id}");

            var producto = await JsonSerializer.DeserializeAsync<Producto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return producto;
        }

        public async Task<Producto> ObtenerProducto(string nombreProd)
        {
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProductoPorNombre/{nombreProd}");

            var producto = await JsonSerializer.DeserializeAsync<Producto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return producto;
        }

        public async Task<ProductoListado> ObtenerProductoListado(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProductoListado/{id}");

            var producto = await JsonSerializer.DeserializeAsync<ProductoListado>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return producto;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductos()
        {
            //Se obtiene el resultado de los productos solicitados a la ruta de la API, el cual viene como cadena y deber ser deserializado
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProductos");

            //Deserializo el objeto JSON a un Enumerable y se usa CASE INSENSITIVE dado que el frontend podria tener un modelo que no este respetando esta condicion, entonces es buena practica colocarlo (en este caso no haria falta porque es el mismo modelo para BACK y FRONT)
            var productos = await JsonSerializer.DeserializeAsync<IEnumerable<ProductoListado>>(response, new JsonSerializerOptions () { PropertyNameCaseInsensitive = true });

            return productos;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductosCarro(int idCarro)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Productos/ObtenerProductosCarro/{idCarro}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var productos = await JsonSerializer.DeserializeAsync<IEnumerable<ProductoListado>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (productos != null)
                {
                    foreach (var prod in productos)
                    {
                        if (prod.ImagenDestacada != null)
                            prod.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(prod.ImagenDestacada)}";
                    }
                    return productos;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductosInactivos()
        {
            //Se obtiene el resultado de los productos solicitados a la ruta de la API, el cual viene como cadena y deber ser deserializado
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProductosInactivos");

            //Deserializo el objeto JSON a un Enumerable y se usa CASE INSENSITIVE dado que el frontend podria tener un modelo que no este respetando esta condicion, entonces es buena practica colocarlo (en este caso no haria falta porque es el mismo modelo para BACK y FRONT)
            var productos = await JsonSerializer.DeserializeAsync<IEnumerable<ProductoListado>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return productos;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductosPorBusqueda(string busqueda)
        {
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProductosPorBusqueda/{busqueda}");

            var productos = await JsonSerializer.DeserializeAsync<IEnumerable<ProductoListado>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (productos != null)
            {
                foreach (var prod in productos)
                {
                    if (prod.ImagenDestacada != null)
                        prod.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(prod.ImagenDestacada)}";
                }
                return productos;
            }
            else
                return null;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerProductosPorTipoProducto(int idTipoProd)
        {
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProductosPorTipoProducto/{idTipoProd}");

            var productos = await JsonSerializer.DeserializeAsync<IEnumerable<ProductoListado>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (productos != null)
            {
                foreach (var prod in productos)
                {
                    if (prod.ImagenDestacada != null)
                        prod.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(prod.ImagenDestacada)}";
                }
                return productos;
            }
            else
                return null;
        }

        public async Task<IEnumerable<ProductoListado>> ObtenerUltimos5Productos()
        {
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerUltimos5Productos");

            var productos = await JsonSerializer.DeserializeAsync<IEnumerable<ProductoListado>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (productos != null)
            {
                foreach (var prod in productos)
                {
                    if(prod.ImagenDestacada != null)
                        prod.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(prod.ImagenDestacada)}";
                }
                return productos;
            }
            else
                return null;
        }

        public async Task<int> UltimoProductoCreado(int idUsuario)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return 0;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Productos/UltimoProductoCreado/{idUsuario}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var ultimoId = await JsonSerializer.DeserializeAsync<int>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return ultimoId;
            }
            else
                return 0;
        }
    }
}
