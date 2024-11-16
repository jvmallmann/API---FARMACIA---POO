using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Repository
{
    // repositório para remedy
    // implementa a interface genérica irepository para realizar operações no banco de dados
    public class RemedyRepository : IRepository<Remedy>
    {
        private readonly DbContextToMemory _context;

        public RemedyRepository(DbContextToMemory context)
        {
            _context = context;
        }

        // método para buscar todos os remédios no banco
        public IEnumerable<Remedy> GetAll()
        {
            // retorna todos os remédios disponíveis no dbset remedy
            return _context.Remedy.ToList();
        }

        // método para buscar um remédio pelo id
        public Remedy GetId(int id)
        {
            // busca o remédio pelo id utilizando o método find
            return _context.Remedy.Find(id);
        }

        // método para adicionar um novo remédio
        public void Add(Remedy remedio)
        {
            // adiciona o remédio ao dbset e salva as alterações no banco
            _context.Remedy.Add(remedio);
            _context.SaveChanges();
        }

        // método para atualizar um remédio existente
        public void Update(Remedy remedio)
        {
            // atualiza os campos do remédio e persiste as mudanças no banco
            _context.Remedy.Update(remedio);
            _context.SaveChanges();
        }

        // método para remover um remédio pelo id
        public void Delete(int id)
        {
            // busca o remédio no banco pelo id
            var remedio = _context.Remedy.Find(id);

            // verifica se o remédio existe antes de remover
            if (remedio != null)
            {
                _context.Remedy.Remove(remedio);
                _context.SaveChanges();
            }
        }
    }
}
