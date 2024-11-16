using AVALIAÇÃO_A4.DataBase.DTO;

namespace AVALIAÇÃO_A4.Interface
{
    // interface para definir o contrato do service de receitas
    public interface IRecipeService
    {
        
        RecipeDTO GetId(int id);// retorna uma receita específica pelo ID como DTO

        IEnumerable<RecipeDTO> GetAll();// retorna todas as receitas como DTOs

        void Add(RecipeDTO receitaDto);// adiciona uma nova receita com base no DTO fornecido

        void Update(int id, RecipeDTO receitaDto);// atualiza uma receita existente com base no DTO fornecido

        void Delete(int id); // remove uma receita específica com base no ID
    }
}
