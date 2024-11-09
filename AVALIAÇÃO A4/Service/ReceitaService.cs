using AutoMapper;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Validate;

namespace AVALIAÇÃO_A4.Service
{
    public class ReceitaService : IReceitaService
    {
        private readonly IRepository<Receita> _repository;
        private readonly ReceitaValidator _validator;
        private readonly IMapper _mapper;

        public ReceitaService(IRepository<Receita> repository, ReceitaValidator validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public IEnumerable<ReceitaDTO> ListarTodas()
        {
            var receitas = _repository.ListarTodos();
            return _mapper.Map<IEnumerable<ReceitaDTO>>(receitas); // Mapeamento automático de Receita para ReceitaDTO
        }

        public ReceitaDTO ObterPorId(int id)
        {
            var receita = _repository.ObterPorId(id);
            return receita == null ? null : _mapper.Map<ReceitaDTO>(receita); // Mapeamento automático de Receita para ReceitaDTO
        }

        public void Adicionar(ReceitaDTO receitaDto)
        {
            try
            {
                _validator.Validar(receitaDto);

                // Mapeamento de ReceitaDTO para Receita
                var receita = _mapper.Map<Receita>(receitaDto);

                _repository.Adicionar(receita);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao adicionar receita: {ex.Message}");
            }
        }

        public void Atualizar(ReceitaDTO receitaDto)
        {
            try
            {
                var receita = _repository.ObterPorId(receitaDto.Id);
                if (receita == null)

                _validator.Validar(receitaDto);

                // Atualiza a receita usando o mapeamento do DTO
                _mapper.Map(receitaDto, receita);

                _repository.Atualizar(receita);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao atualizar receita: {ex.Message}");
            }
        }

        public void Remover(int id)
        {
            var receita = _repository.ObterPorId(id);
            if (receita == null)

            _repository.Remover(id);
        }
    }
}
