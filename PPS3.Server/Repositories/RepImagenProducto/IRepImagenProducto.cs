namespace PPS3.Server.Repositories.RepImagenProducto
{
    public interface IRepImagenProducto
    {
        //Traer todas las imagenes de una producto, traer una imagen, actualizar una imagen a la principal, borrar imagen, insertar imagen, actualizar imagen
        Task<IEnumerable<ImagenProducto>> ObtenerImagenes(int idProducto);
        Task<ImagenProducto> ObtenerImagen(int idImagen);
        Task<bool> InsertarImagen(ImagenProducto imagenProducto);
        Task<bool> ActualizarImagen(ImagenProducto imagenProducto);
        Task<bool> BorrarImagen(int id);
    }
}
