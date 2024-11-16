using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;

namespace AVALIAÇÃO_A4.Repository
{
    // repositório para sale (venda)
    // implementa a interface genérica irepository para gerenciar operações relacionadas a vendas no banco de dados
    public class SaleRepository : IRepository<Sale>
    {
        private readonly DbContextToMemory _context;

        // construtor que utiliza injeção de dependência para acessar o banco de dados em memória
        public SaleRepository(DbContextToMemory context)
        {
            _context = context;
        }

        // método para listar todas as vendas registradas no banco
        public IEnumerable<Sale> GetAll()
        {
            // retorna todas as vendas registradas no dbset sale
            return _context.Sale.ToList();
        }

        // método para obter uma venda específica pelo id
        public Sale GetId(int id)
        {
            // busca uma venda específica utilizando o método find
            return _context.Sale.Find(id);
        }

        // método para adicionar uma nova venda ao banco de dados
        public void Add(Sale venda)
        {
            // adiciona uma nova venda ao dbset sale e salva as alterações no banco
            _context.Sale.Add(venda);
            _context.SaveChanges();
        }

        // método para atualizar uma venda existente
        public void Update(Sale venda)
        {
            // atualiza os dados de uma venda existente e persiste as alterações
            _context.Sale.Update(venda);
            _context.SaveChanges();
        }

        // método para remover uma venda específica pelo id
        public void Delete(int id)
        {
            // busca a venda no banco utilizando o id fornecido
            var venda = _context.Sale.Find(id);

            // verifica se a venda existe antes de removê-la
            if (venda != null)
            {
                _context.Sale.Remove(venda);
                _context.SaveChanges();
            }
        }
    }
}
