namespace PPS3.Server.Repositories.RepUsuario
{
    public interface IRepUsuario
    {
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
        Task<Usuario> ObtenerUsuario(int id);
        Task<Usuario> ObtenerUsuario(string nombreUsuario);
        Task<bool> CrearUsuario(UsuarioCliente usuarioCliente);
        Task<bool> ActualizarUsuario(Usuario usuario);
        Task<bool> BorrarUsuario(int id);
    }
}
