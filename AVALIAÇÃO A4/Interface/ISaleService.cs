using AVALIAÇÃO_A4.DataBase.DTO;

namespace AVALIAÇÃO_A4.Interface
{
    // interface para gerenciar vendas 
    public interface ISaleService
    {
        // útil para listar todas as vendas realizadas no sistema
        IEnumerable<SaleDTO> GetAll();// retorna todas as vendas como DTOs
        SaleDTO GetId(int id);// retorna uma venda específica pelo ID como DTO
        void Add(SaleDTO vendaDto);// adiciona uma nova venda com base no DTO fornecido
        void Update(SaleDTO vendaDto);// atualiza uma venda existente com base no DTO fornecido
        void Delete(int id);// remove uma venda específica com base no ID
    }
}
