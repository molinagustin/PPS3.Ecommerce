namespace PPS3.Client.Services.ServUsuario
{
    public interface IServUsuario
    {
        Task<bool> Login(UsuarioRequest usuarioReq);
        Task<bool> Logout();
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
        Task<Usuario> ObtenerUsuario(int id);
        Task<Usuario> ObtenerUsuario(string nombreUsuario);
        Task<bool> ActualizarUsuario(Usuario usuario);
        Task<bool> BorrarUsuario(int id);
    }
}
