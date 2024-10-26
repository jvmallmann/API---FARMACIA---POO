using AVALIAÇÃO_A4.Classes;

namespace AVALIAÇÃO_A4.Interface
{
    public interface IReceitaService
    {
        IEnumerable<Receita> ListarTodos(); 
        Receita ObterPorId(int id); 
        void Adicionar(Receita receita); 
        void Atualizar(int id, Receita receita); 
        void Remover(int id); 
    }
}
