using AVALIAÇÃO_A4.DataBase.DTO;

namespace AVALIAÇÃO_A4.Interface
{
    // interface para definir o contrato do service de remédios
    public interface IRemedyService
    {
        
        IEnumerable<RemedyDTO> GetAll();// retorna uma lista de todos os remédios

        
        RemedyDTO GetId(int id);// retorna um remédio específico pelo ID

        
        void Add(RemedyDTO remedioDto);// adiciona um novo remédio

      
        void Update(int id, RemedyDTO remedioDto);  // atualiza os dados de um remédio existente

        
        void Delete(int id);// remove um remédio pelo ID
    }
}
