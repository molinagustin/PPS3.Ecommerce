using System;

namespace PPS3.Client.Services.ServImagenProducto
{
    public class ServImagenProducto : IServImagenProducto
    {
        private readonly HttpClient _httpClient;

        public ServImagenProducto(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> BorrarImagen(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ImagenesProductos/BorrarImagenProducto/{id}");

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarImagen(ImagenProducto imagen)
        {
            var imgJson = new StringContent(JsonSerializer.Serialize(imagen), Encoding.UTF8, "application/json");

            HttpResponseMessage response = new HttpResponseMessage();

            if (imagen.IdImg > 0)
                response = await _httpClient.PutAsync($"api/ImagenesProductos/ActualizarImagenProducto", imgJson);
            else
                response = await _httpClient.PostAsync($"api/ImagenesProductos/CrearImagenProducto", imgJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<ImagenProducto> ObtenerImagen(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/ImagenesProductos/ObtenerImagen/{id}");

            var img = await JsonSerializer.DeserializeAsync<ImagenProducto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            var format = "image/png";
            img.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(img.Contenido)}";

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return img;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<ImagenProducto>> ObtenerImagenes(int idProducto)
        {
            var response = await _httpClient.GetStreamAsync($"api/ImagenesProductos/ObtenerImagenes?idProducto={idProducto}");

            var imgs = await JsonSerializer.DeserializeAsync<IEnumerable<ImagenProducto>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            var format = "image/png";

            foreach (var img in imgs)
            {
                img.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(img.Contenido)}";
            }

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return imgs;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<ImagenProducto>> ObtenerUltimasImagenes()
        {
            var response = await _httpClient.GetStreamAsync($"api/ImagenesProductos/ObtenerUltimasImagenes");

            var imgs = await JsonSerializer.DeserializeAsync<IEnumerable<ImagenProducto>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            var format = "image/png";

            foreach (var img in imgs)
            {
                img.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(img.Contenido)}";
            }

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return imgs;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
