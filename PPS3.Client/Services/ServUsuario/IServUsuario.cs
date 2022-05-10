namespace PPS3.Client.Services.ServUsuario
{
    public interface IServUsuario
    {
        Task<bool> Login(UsuarioRequest usuarioReq);
        Task<bool> Logout();
    }
}
