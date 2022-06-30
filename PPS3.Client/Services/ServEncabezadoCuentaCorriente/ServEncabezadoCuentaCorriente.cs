using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServEncabezadoCuentaCorriente
{
    public class ServEncabezadoCuentaCorriente : IServEncabezadoCuentaCorriente
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServEncabezadoCuentaCorriente(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> BorrarEncabCC(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/EncabezadosCuentasCorrientes/BorrarCuentaCorriente/{id}");
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

        public async Task<bool> GuardarEncabCC(EncabezadoCuentaCorriente encabezadoCC)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var encabJson = new StringContent(JsonSerializer.Serialize(encabezadoCC), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (encabezadoCC.NumCC > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/EncabezadosCuentasCorrientes/ActualizarCuentaCorriente");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = encabJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/EncabezadosCuentasCorrientes/CrearCuentaCorriente");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = encabJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<EncabezadoCuentaCorriente> ObtenerCCCliente(int idCliente)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/EncabezadosCuentasCorrientes/ObtenerCCCliente?idCliente={idCliente}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var encab = await JsonSerializer.DeserializeAsync<EncabezadoCuentaCorriente>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return encab;
            }
            else
                return null;
        }

        public async Task<IEnumerable<CuentasCorrientesListado>> ObtenerCCListado()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/EncabezadosCuentasCorrientes/ObtenerCCListado");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var cuentas = await JsonSerializer.DeserializeAsync<IEnumerable<CuentasCorrientesListado>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return cuentas;
            }
            else
                return null;
        }

        public async Task<EncabezadoCuentaCorriente> ObtenerCuentaCorriente(int numCC)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/EncabezadosCuentasCorrientes/ObtenerCuentaCorriente?numCC={numCC}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var encab = await JsonSerializer.DeserializeAsync<EncabezadoCuentaCorriente>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return encab;
            }
            else
                return null;
        }

        public async Task<IEnumerable<EncabezadoCuentaCorriente>> ObtenerCuentasCorrientes()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/EncabezadosCuentasCorrientes/ObtenerCuentasCorrientes");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var encabs = await JsonSerializer.DeserializeAsync<IEnumerable<EncabezadoCuentaCorriente>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return encabs;
            }
            else
                return null;
        }
    }
}
