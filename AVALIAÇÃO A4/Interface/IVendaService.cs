using AVALIAÇÃO_A4.DataBase.DTO;


namespace AVALIAÇÃO_A4.Interface
{
    public interface IVendaService
    {
        IEnumerable<VendaDTO> ListarTodas();
        VendaDTO ObterPorId(int id);
        bool Adicionar(VendaDTO vendaDto);
        bool Atualizar(VendaDTO vendaDto);
        bool Remover(int id);
    }
}
