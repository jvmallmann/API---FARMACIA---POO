
namespace AVALIAÇÃO_A4.Interface
{
    public interface IRepository<T>
    {
        IEnumerable<T> ListarTodos(); // Recupera todos os registros
        T ObterPorId(int id); // Recupera um registro pelo ID
        void Adicionar(T entidade); // Adiciona um novo registro
        void Atualizar(T entidade); // Atualiza um registro existente
        void Remover(int id); // Remove um registro pelo ID
    }
}
