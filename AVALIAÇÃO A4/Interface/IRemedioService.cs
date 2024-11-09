using AVALIAÇÃO_A4.DataBase.DTO;


namespace AVALIAÇÃO_A4.Interface
{
    public interface IRemedioService
    {
        IEnumerable<RemedioDTO> ListarTodos();
        RemedioDTO ObterPorId(int id);
        bool Adicionar(RemedioDTO remedioDto);
        bool Atualizar(RemedioDTO remedioDto);
        bool Remover(int id);
    }
}
