namespace PPS3.Server.Data
{
    public class SqlConfiguration
    {
        //Clase destinada a encapsular la cadena de conexion hacia la base de datos, la cual sera asignada al momento en que ésta clase es inyectada en los servicios del arranque.
        public SqlConfiguration(string connectionString) => ConnectionString = connectionString;
        public string ConnectionString { get; set; }
    }
}