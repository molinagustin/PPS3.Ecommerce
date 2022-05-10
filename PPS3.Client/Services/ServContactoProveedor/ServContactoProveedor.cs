namespace PPS3.Client.Services.ServContactoProveedor
{
    public class ServContactoProveedor : IServContactoProveedor
    {
        private readonly HttpClient _httpClient;

        public ServContactoProveedor(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> BorrarContacto(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ContactosProveedores/{id}");

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarContacto(ContactoProveedor contacto)
        {
            var contactoJson = new StringContent(JsonSerializer.Serialize(contacto), Encoding.UTF8, "applicattion/json");

            var response = new HttpResponseMessage();

            if (contacto.IdProvCont > 0)
                response = await _httpClient.PutAsync($"api/ContactosProveedores", contactoJson);
            else
                response = await _httpClient.PostAsync($"api/ContactosProveedores", contactoJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<ContactoProveedor> ObtenerConctacto(string nombreContacto)
        {
            var response = await _httpClient.GetStreamAsync($"api/ContactosProveedores/{nombreContacto}");

            var contacto = await JsonSerializer.DeserializeAsync<ContactoProveedor>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return contacto;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<ContactoProveedor> ObtenerContacto(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/ContactosProveedores/{id}");

            var contacto = await JsonSerializer.DeserializeAsync<ContactoProveedor>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return contacto;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<ContactoProveedor>> ObtenerContactos()
        {
            var response = await _httpClient.GetStreamAsync($"api/ContactosProveedores");

            var contactos = await JsonSerializer.DeserializeAsync<IEnumerable<ContactoProveedor>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return contactos;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
