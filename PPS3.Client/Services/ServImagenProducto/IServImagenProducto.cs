namespace PPS3.Client.Services.ServImagenProducto
{
    public interface IServImagenProducto
    {
        Task<IEnumerable<ImagenProducto>> ObtenerImagenes();
        Task<ImagenProducto> ObtenerImagen(int id);
        Task<bool> GuardarImagen(ImagenProducto imagen);
        Task<bool> BorrarImagen(int id);
    }
}
