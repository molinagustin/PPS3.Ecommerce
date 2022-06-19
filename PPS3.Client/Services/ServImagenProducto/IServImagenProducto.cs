namespace PPS3.Client.Services.ServImagenProducto
{
    public interface IServImagenProducto
    {
        Task<IEnumerable<ImagenProducto>> ObtenerImagenes(int idProducto);
        Task<IEnumerable<ImagenProducto>> ObtenerUltimasImagenes();
        Task<ImagenProducto> ObtenerImagen(int id);
        Task<bool> GuardarImagen(ImagenProducto imagen);
        Task<bool> BorrarImagen(int id);
        Task<bool> ImagenFavorita(ImagenProducto imagen);
    }
}
