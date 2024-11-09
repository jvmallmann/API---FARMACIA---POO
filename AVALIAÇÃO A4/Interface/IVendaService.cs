using AVALIAÇÃO_A4.DataBase.DTO;


namespace AVALIAÇÃO_A4.Interface
{
    public interface IVendaService
    {
        IEnumerable<VendaDTO> ListarTodas();
        VendaDTO ObterPorId(int id);
        void Adicionar(VendaDTO vendaDto);
        void Atualizar(VendaDTO vendaDto);
        void Remover(int id);
    }
}
