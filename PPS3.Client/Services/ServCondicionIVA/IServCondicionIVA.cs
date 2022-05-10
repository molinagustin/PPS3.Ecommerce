using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServCondicionIVA
{
    public interface IServCondicionIVA
    {
        Task<IEnumerable<CondicionIVA>> ObtenerCondiciones();
        Task<CondicionIVA> ObtenerCondicion(int id);
    }
}
