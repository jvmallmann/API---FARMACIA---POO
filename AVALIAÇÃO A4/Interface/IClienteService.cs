using AVALIAÇÃO_A4.DataBase.DTO;


namespace AVALIAÇÃO_A4.Interface
{
    public interface IClienteService
    {
        IEnumerable<ClienteDTO> ListarTodos();      // Retorna todos os clientes como DTOs
        ClienteDTO ObterPorId(int id);              // Retorna um cliente específico pelo ID como DTO
        void Adicionar(ClienteDTO clienteDto);      // Adiciona um novo cliente com base no DTO fornecido
        void Atualizar(ClienteDTO clienteDto);      // Atualiza um cliente existente com base no DTO fornecido
        void Remover(int id);                       // Remove um cliente específico com base no ID
    }
}
