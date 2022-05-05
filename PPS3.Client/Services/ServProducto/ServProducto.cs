using System.Text;
using System.Text.Json;

namespace PPS3.Client.Services.ServProducto
{
    public class ServProducto : IServProducto
    {
        //Se comienza por crear el campo que se utilizara para los HTTP request, el cual ya trae configurado .NET CORE
        private readonly HttpClient _httpClient;

        public ServProducto(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task BorrarProducto(int id)
        {
            //Se usa el metodo del objeto httpClient que directamente llama a una solicitud DELETE y se le pasa la ruta hacia la API, la accion y el ID
            await _httpClient.DeleteAsync($"api/Productos/BorrarProducto/{id}");
        }

        public async Task GuardarProducto(Producto producto)
        {
            //Se procede a Serializar el contenido del producto por parametro
            var productoJson = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json" );

            //Se verifica si es un INSERT o UPDATE
            if (producto.IdProducto > 0)
                await _httpClient.PutAsync($"api/Productos/ActualizarProducto", productoJson);
            else
                await _httpClient.PostAsync($"api/Productos/CrearProducto", productoJson);
        }

        public async Task<Producto> ObtenerProducto(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProducto/{id}");

            var producto = await JsonSerializer.DeserializeAsync<Producto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            #pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return producto;
            #pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<Producto> ObtenerProducto(string nombreProd)
        {
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProductoPorNombre/{nombreProd}");

            var producto = await JsonSerializer.DeserializeAsync<Producto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            #pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return producto;
            #pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<Producto>> ObtenerProductos()
        {
            //Se obtiene el resultado de los productos solicitados a la ruta de la API, el cual viene como cadena y deber ser deserializado
            var response = await _httpClient.GetStreamAsync($"api/Productos/ObtenerProductos");

            //Deserializo el objeto JSON a un Enumerable y se usa CASE INSENSITIVE dado que el frontend podria tener un modelo que no este respetando esta condicion, entonces es buena practica colocarlo (en este caso no haria falta porque es el mismo modelo para BACK y FRONT)
            var productos = await JsonSerializer.DeserializeAsync<IEnumerable<Producto>>(response, new JsonSerializerOptions () { PropertyNameCaseInsensitive = true });

            #pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return productos;
            #pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
