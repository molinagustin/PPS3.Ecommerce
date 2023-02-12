namespace PPS3.Client.Services.ServUsuario
{
    public interface IServUsuario
    {
        Task<bool> Login(UsuarioRequest usuarioReq);
        Task<bool> Logout();
        Task<bool> Registrarse(UsuarioCliente usuarioCl);
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
        Task<Usuario> ObtenerUsuario(int id);
        Task<Usuario> ObtenerUsuario(string nombreUsuario);
        Task<bool> ActualizarUsuario(Usuario usuario);
        Task<bool> ActualizarPerfilUsuario(UsuarioCliente usuarioCl);
        Task<bool> BorrarUsuario(int id);
        Task<bool> CambiarPassword(UsuarioCliente usuarioCliente);
        Task<bool> ValidarEmailUsuario(int id);
    }
}
