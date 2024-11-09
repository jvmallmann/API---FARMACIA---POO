using AVALIAÇÃO_A4.DataBase.DTO;

namespace AVALIAÇÃO_A4.Interface
{
    public interface IReceitaService
    {
        ReceitaDTO ObterPorId(int id);                   // Retorna uma receita específica pelo ID como DTO
        IEnumerable<ReceitaDTO> ListarTodas();           // Retorna todas as receitas como DTOs
        void Adicionar(ReceitaDTO receitaDto);           // Adiciona uma nova receita com base no DTO fornecido
        void Atualizar(ReceitaDTO receitaDto);           // Atualiza uma receita existente com base no DTO fornecido
        void Remover(int id);                            // Remove uma receita específica com base no ID
    }
}
