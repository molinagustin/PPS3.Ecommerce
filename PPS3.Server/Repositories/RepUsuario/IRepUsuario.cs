namespace PPS3.Server.Repositories.RepUsuario
{
    public interface IRepUsuario
    {
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
        Task<Usuario> ObtenerUsuario(int id);
        Task<Usuario> ObtenerUsuario(string nombreUsuario);
        Task<int> ObtenerUltimoIdCreado(string NombreUs);
        Task<bool> CrearCarroNuevoUsuario(int idUsuario);
        Task<int> CrearUsuario(UsuarioCliente usuarioCliente);
        Task<bool> ActualizarUsuario(Usuario usuario);
        Task<bool> ActualizarPerfilUsuario(UsuarioCliente usuario);
        Task<bool> BorrarUsuario(int id);
        bool ValidarHash(string pass, string salt, string hash);
        Task<bool> UsuarioExistente(string nombreUsuario);
        Task<bool> CambiarPassword(UsuarioCliente usuarioCliente);
    }
}
