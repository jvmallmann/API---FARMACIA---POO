using AVALIAÇÃO_A4.DataBase.DTO;


namespace AVALIAÇÃO_A4.Interface
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAll(); // Retorna todos os clientes como DTOs
        CustomerDTO GetId(int id); // Retorna um cliente específico pelo ID como DTO
        void Add (CustomerDTO clienteDto); // Adiciona um novo cliente com base no DTO fornecido
        void Update (CustomerDTO clienteDto); // Atualiza um cliente existente com base no DTO fornecido
        void Delete (int id); // Remove um cliente específico com base no ID
    }
}
