using AVALIAÇÃO_A4.DataBase.DTO;


namespace AVALIAÇÃO_A4.Interface
{
    public interface IRemedioService
    {
        IEnumerable<RemedioDTO> ListarTodos();
        RemedioDTO ObterPorId(int id);
        void Adicionar(RemedioDTO remedioDto);
        void Atualizar(RemedioDTO remedioDto);
        void Remover(int id);
    }
}
