using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.DTOs;
using minimal_api.Infraestrutura.DB;

namespace minimal_api.Dominio.Serviços
{
    public class ADMService : IADMService
    {
        private readonly DBContext _context;
        public ADMService(DBContext db)
        {
            _context = db;
        }
        public ADM? Login(LoginDTO loginDTO)
        {
            var adm = _context.ADMs.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Password).FirstOrDefault();
            return adm;
        }

        }
    }
