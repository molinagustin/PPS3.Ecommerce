using PPS3.Shared.Models;
using System;

namespace PPS3.Client.Services.ServImagenProducto
{
    public class ServImagenProducto : IServImagenProducto
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;
        private readonly string format = "image/png";

        public ServImagenProducto(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> BorrarImagen(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/ImagenesProductos/BorrarImagenProducto/{id}");
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

        public async Task<bool> GuardarImagen(ImagenProducto imagen)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var imgJson = new StringContent(JsonSerializer.Serialize(imagen), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (imagen.IdImg > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/ImagenesProductos/ActualizarImagenProducto");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = imgJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/ImagenesProductos/CrearImagenProducto");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = imgJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> ImagenFavorita(ImagenProducto imagen)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var imgJson = new StringContent(JsonSerializer.Serialize(imagen), Encoding.UTF8, "application/json");

            //Creo una solicitud Http de tipo PUT
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/ImagenesProductos/ImagenFavorita");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            //Agrego el JSON al BODY
            request.Content = imgJson;
            //Envio la solicitud HTTP
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<ImagenProducto> ObtenerImagen(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/ImagenesProductos/ObtenerImagen/{id}");

            var img = await JsonSerializer.DeserializeAsync<ImagenProducto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (img != null)
            {
                img.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(img.Contenido)}";
                return img;
            }
            else
                return null;
        }

        public async Task<IEnumerable<ImagenProducto>> ObtenerImagenes(int idProducto)
        {
            var response = await _httpClient.GetStreamAsync($"api/ImagenesProductos/ObtenerImagenes?idProducto={idProducto}");

            var imgs = await JsonSerializer.DeserializeAsync<IEnumerable<ImagenProducto>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (imgs != null)
            {
                foreach (var img in imgs)
                {
                    img.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(img.Contenido)}";
                }
                return imgs;
            }
            else
                return null;
        }

        public async Task<IEnumerable<ImagenProducto>> ObtenerUltimasImagenes()
        {
            var response = await _httpClient.GetStreamAsync($"api/ImagenesProductos/ObtenerUltimasImagenes");

            var imgs = await JsonSerializer.DeserializeAsync<IEnumerable<ImagenProducto>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (imgs != null)
            {
                foreach (var img in imgs)
                {
                    img.UrlImagen = $"data:{format};base64,{Convert.ToBase64String(img.Contenido)}";
                }

                return imgs;
            }
            else
                return null;
        }
    }
}
