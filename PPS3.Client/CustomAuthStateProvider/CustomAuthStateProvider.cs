using System.Security.Claims;
//using System.Net.Http.Headers;

namespace PPS3.Client.CustomAuthStateProvider
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        //Para crear el objeto de autenticacion personalizado se debe heredar de la Clase Abstracta implementada desde la libreria Microsoft.AspNetCore.Components.Authorization e implementar su metodo obligatorio
        //Es necesario obtener el token guardado en la Session Storage para confirmar si el usuario esta o no logueado con lo que inyectamos el servicio en el Constructor de la clase
        private readonly ISessionStorageService _sessionStorage;
        //private readonly HttpClient _httpClient; //Se puede usar este objeto para colocar en el encabezado de la solicitud HTTP el TOKEN BEARER

        public CustomAuthStateProvider(ISessionStorageService sessionStorage, HttpClient httpClient)
        {
            _sessionStorage = sessionStorage;
            //_httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //Se obtiene el token que deberia estar guardado de cuando se logueo el usuario
            var token = await _sessionStorage.GetItemAsStringAsync("token");

            //Genero una Claim de identidad que tiene los valores apropiados de cada caracteristica al momento de crearse la Claim en el JWT
            var identity = new ClaimsIdentity();
            

            //Si el Token obtenido es valido, se procede a llenar las Claims contenidas
            if (!string.IsNullOrEmpty(token))
            {
                //Se guardan todas las Claims particulares del JWT en un solo objeto Claim Principal
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                //Tambien se puede colocar aca el token en el encabezado de las solicitudes http
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"",""));
            }

            //Se crea un objeto que contendra todas las Claims que identifican al usuario (Nombre, Rol, Id, etc.)
            var user = new ClaimsPrincipal(identity);
            //Se crea un objeto que tiene el estado de autorizacion en base a las Claims obtenidas
            var state = new AuthenticationState(user);

            //Metodo importante que va a permitir que los componentes que estan bajo la caracteristica Autorizado, sean notificados y re renderizados en caso apropiado para mostrarle o no al usuario el contenido de cada componente.
            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        //Metodo que permite Parsear un JWT y obtener los Claims apropiados del mismo (CODIGO COPIADO)
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}