using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Validate;

namespace AVALIAÇÃO_A4.Service
{
    public class ReceitaService : IReceitaService
    {
        private readonly IRepository<Receita> _receitaRepository;
        private readonly ReceitaValidator _receitaValidator;

        public ReceitaService(IRepository<Receita> receitaRepository, ReceitaValidator receitaValidator)
        {
            _receitaRepository = receitaRepository;
            _receitaValidator = receitaValidator;
        }

        public ReceitaDTO ObterPorId(int id)
        {
            var receita = _receitaRepository.ObterPorId(id);
            if (receita == null) return null;

            return new ReceitaDTO
            {
                Id = receita.Id,
                Numero = receita.Numero,
                Medico = receita.Medico
            };
        }

        public IEnumerable<ReceitaDTO> ListarTodas()
        {
            return _receitaRepository.ListarTodos().Select(r => new ReceitaDTO
            {
                Id = r.Id,
                Numero = r.Numero,
                Medico = r.Medico
            });
        }

        public void Adicionar(ReceitaDTO receitaDto)
        {
            if (!_receitaValidator.Validar(receitaDto));

            var receita = new Receita
            {
                Numero = receitaDto.Numero,
                Medico = receitaDto.Medico
            };

            _receitaRepository.Adicionar(receita);
        }

        public void Atualizar(ReceitaDTO receitaDto)
        {
            var receita = _receitaRepository.ObterPorId(receitaDto.Id);
            if (receita == null || !_receitaValidator.Validar(receitaDto)) ;

            receita.Numero = receitaDto.Numero;
            receita.Medico = receitaDto.Medico;

            _receitaRepository.Atualizar(receita);
        }

        public void Remover(int id)
        {
            var receita = _receitaRepository.ObterPorId(id);
            if (receita == null)

            _receitaRepository.Remover(id);
        }
    }
}
