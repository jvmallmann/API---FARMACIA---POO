using System.Collections.Generic;
using AVALIAÇÃO_A4.Classes;
using AVALIAÇÃO_A4.Interface;

public class ReceitaService : IReceitaService
{
    private readonly IRepository<Receita> _repository;

    public ReceitaService(IRepository<Receita> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Receita> ListarTodos()
    {
        return _repository.ListarTodos();
    }

    public Receita ObterPorId(int id)
    {
        return _repository.ObterPorId(id);
    }

    public void Adicionar(Receita receita)
    {
        _repository.Adicionar(receita);
    }

    public void Atualizar(int id, Receita receita)
    {
        receita.Id = id;
        _repository.Atualizar(receita);
    }

    public void Remover(int id)
    {
        _repository.Remover(id);
    }
}
