namespace AVALIAÇÃO_A4.Interface
{
    // Interface genérica para definição de um repositório 
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();// recupera todos os registros do tipo T
        T GetId(int id);// recupera um registro pelo ID
        void Add(T entidade);// adiciona um novo registro do tipo T
        void Update(T entidade);// atualiza um registro existente do tipo T
        void Delete(int id); // remove um registro pelo ID
    }
}
