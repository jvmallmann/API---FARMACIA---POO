using AutoMapper;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Validate;

namespace AVALIAÇÃO_A4.Service
{
    public class RemedioService : IRemedioService
    {
        private readonly IRepository<Remedio> _repository;
        private readonly RemedioValidator _validator;
        private readonly IMapper _mapper;

        public RemedioService(IRepository<Remedio> repository, RemedioValidator validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public IEnumerable<RemedioDTO> ListarTodos()
        {
            var remedios = _repository.ListarTodos();
            return _mapper.Map<IEnumerable<RemedioDTO>>(remedios); // Mapeamento automático de Remedio para RemedioDTO
        }

        public RemedioDTO ObterPorId(int id)
        {
            var remedio = _repository.ObterPorId(id);
            return remedio == null ? null : _mapper.Map<RemedioDTO>(remedio); // Mapeamento automático de Remedio para RemedioDTO
        }

        public bool Adicionar(RemedioDTO remedioDto)
        {
            try
            {
                _validator.Validar(remedioDto);

                // Mapeamento de RemedioDTO para Remedio
                var remedio = _mapper.Map<Remedio>(remedioDto);

                _repository.Adicionar(remedio);
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao adicionar remédio: {ex.Message}");
                return false;
            }
        }

        public bool Atualizar(RemedioDTO remedioDto)
        {
            try
            {
                var remedio = _repository.ObterPorId(remedioDto.Id);
                if (remedio == null) return false;

                _validator.Validar(remedioDto);

                // Atualiza o remédio usando o mapeamento do DTO
                _mapper.Map(remedioDto, remedio);

                _repository.Atualizar(remedio);
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao atualizar remédio: {ex.Message}");
                return false;
            }
        }

        public bool Remover(int id)
        {
            var remedio = _repository.ObterPorId(id);
            if (remedio == null) return false;

            _repository.Remover(id);
            return true;
        }
    }
}
