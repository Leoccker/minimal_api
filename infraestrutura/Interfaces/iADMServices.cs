using minimal_api.Dominio.Entidades;
using minimal_api.DTOs;

namespace minimal_api.Dominio.Interfaces
{
    public interface IADMService
    {
        ADM? Login(LoginDTO loginDTO);
    }
}