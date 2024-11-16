using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.Exceptions;
using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Repository;

namespace AVALIAÇÃO_A4.Validate
{
    public class EstoqueRemedioValidator
    {
        private readonly IRepository<EstoqueRemedio> _estoqueRepository;

        public EstoqueRemedioValidator(IRepository<EstoqueRemedio> estoqueRepository)
        {
            _estoqueRepository = estoqueRepository ?? throw new ArgumentNullException(nameof(estoqueRepository));
        }

        public void ValidarAdicionar(EstoqueRemedioDTO estoqueDto)
        {
            if (estoqueDto == null)
                throw new EstoqueValidationException("Os dados do estoque não podem ser nulos.");

            if (estoqueDto.RemedioId <= 0)
                throw new EstoqueValidationException("O ID do remédio deve ser maior que zero.");

            if (estoqueDto.Quantidade < 0)
                throw new EstoqueValidationException("A quantidade no estoque não pode ser negativa.");

            if (string.IsNullOrWhiteSpace(estoqueDto.Lote))
                throw new EstoqueValidationException("O lote do remédio é obrigatório.");
        }

        public void ValidarAtualizar(int remedioId, int quantidade)
        {
            if (remedioId <= 0)
                throw new EstoqueValidationException("O ID do remédio deve ser maior que zero.");

            if (quantidade < 0)
                throw new EstoqueValidationException("A quantidade no estoque não pode ser negativa.");

            var estoque = _estoqueRepository.ObterPorId(remedioId);
            if (estoque == null)
                throw new EstoqueValidationException($"Remédio com ID {remedioId} não encontrado no estoque.");
        }

        public void ValidarDisponibilidade(int remedioId, int quantidadeRequerida)
        {
            if (remedioId <= 0)
                throw new EstoqueValidationException("O ID do remédio deve ser maior que zero.");

            if (quantidadeRequerida <= 0)
                throw new EstoqueValidationException("A quantidade requerida deve ser maior que zero.");

            var estoque = _estoqueRepository.ObterPorId(remedioId);
            if (estoque == null)
                throw new EstoqueValidationException($"Remédio com ID {remedioId} não encontrado no estoque.");

            if (estoque.Quantidade < quantidadeRequerida)
                throw new EstoqueValidationException($"Estoque insuficiente para o remédio com ID {remedioId}. Disponível: {estoque.Quantidade}, Requerido: {quantidadeRequerida}.");
        }
    }
}
