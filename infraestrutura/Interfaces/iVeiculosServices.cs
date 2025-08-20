using minimal_api.Dominio.Entidades;
using minimal_api.DTOs;

namespace minimal_api.Dominio.Interfaces
{
    public interface IVeiculosService
    {
        List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null);
        Veiculo? BuscaPorId(int id);

        void Incluir(Veiculo veiculo);

        void Atualizar(int id, Veiculo veiculo);

        void Apagar(Veiculo veiculo);
    }
}