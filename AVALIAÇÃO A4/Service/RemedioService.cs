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

        public RemedioService(IRepository<Remedio> repository, RemedioValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public IEnumerable<RemedioDTO> ListarTodos()
        {
            var remedios = _repository.ListarTodos();
            return remedios.Select(r => new RemedioDTO
            {
                Id = r.Id,
                Nome = r.Nome,
                UnidadeMedida = r.UnidadeMedida,
                PrecisaReceita = r.PrecisaReceita
            });
        }

        public RemedioDTO ObterPorId(int id)
        {
            var remedio = _repository.ObterPorId(id);
            if (remedio == null) return null;

            return new RemedioDTO
            {
                Id = remedio.Id,
                Nome = remedio.Nome,
                UnidadeMedida = remedio.UnidadeMedida,
                PrecisaReceita = remedio.PrecisaReceita
            };
        }

        public bool Adicionar(RemedioDTO remedioDto)
        {
            try
            {
                _validator.Validar(remedioDto);

                var remedio = new Remedio
                {
                    Nome = remedioDto.Nome,
                    UnidadeMedida = remedioDto.UnidadeMedida,
                    PrecisaReceita = remedioDto.PrecisaReceita
                };

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

                remedio.Nome = remedioDto.Nome;
                remedio.UnidadeMedida = remedioDto.UnidadeMedida;
                remedio.PrecisaReceita = remedioDto.PrecisaReceita;

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
