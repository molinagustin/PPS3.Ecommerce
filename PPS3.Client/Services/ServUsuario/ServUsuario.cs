using System.Text.Json;
using System.Text;

namespace PPS3.Client.Services.ServUsuario
{
    public class ServUsuario : IServUsuario
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        //Se inyecta el servicio HTTP y SESSION STORAGE
        public ServUsuario(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> Login(UsuarioRequest usuarioReq)
        {
            //Serializo el objeto
            var usuarioJson = new StringContent(JsonSerializer.Serialize(usuarioReq),
                Encoding.UTF8, "application/json");

            //Guardo el token devuelto en caso exitoso
            var token = await _httpClient.PostAsync("api/Usuarios/Login", usuarioJson);

            //Si hubo una respuesta exitosa, guardo en el SESSION STORAGE los datos
            if (token.IsSuccessStatusCode)
            {
                await _sessionStorage.SetItemAsync("token", await token.Content.ReadAsStringAsync());
                return true;
            }
            else            
                return false;            
        }

        public async Task<bool> Logout()
        {
            await _sessionStorage.RemoveItemAsync("token");
            return true;
        }
    }
}
