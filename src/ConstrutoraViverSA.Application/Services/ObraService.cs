using ConstrutoraViverSA.Domain;
using System.Collections.Generic;
using ConstrutoraViverSA.Application.Interfaces;
using ConstrutoraViverSA.Domain.Dtos;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Repository.Interfaces;

namespace ConstrutoraViverSA.Application.Services
{
    public class ObraService : IObraService
    {
        private readonly IObraRepository _repository;
        private readonly IOrcamentoService _orcamentoService;
        private readonly IFuncionarioService _funcionarioService;
        
        public ObraService(
            IObraRepository repository,
            IOrcamentoService orcamentoService,
            IFuncionarioService funcionarioService)
        {
            _repository = repository;
            _orcamentoService = orcamentoService;
            _funcionarioService = funcionarioService;
        }

        public List<Obra> BuscarTodos()
        {
            return _repository.BuscarTodos();
        }

        public Obra BuscarPorId(long buscaId)
        {
            var obra = _repository.BuscarPorId(buscaId);
            
            if (obra is null)
            {
                throw new NaoEncontradoException("Obra não encontrada");
            }

            return obra;
        }

        public void Adicionar(ObraDto dto)
        {
            var orcamento = _orcamentoService.BuscarPorId((long)dto.OrcamentoId);
            var obra = dto.DtoParaDominio();

            obra.Orcamento = orcamento;
            
            _repository.Adicionar(obra);
        }
        public void Excluir(long idExcluir)
        {
            var obra = BuscarPorId(idExcluir);

            _repository.Excluir(obra);
        }

        public void Editar(long id, ObraDto obralAtualizado)
        {
            var obra = BuscarPorId(id);

            if (obralAtualizado.OrcamentoId != null && obralAtualizado.OrcamentoId != obra.OrcamentoId)
            {
                var orcamento = _orcamentoService.BuscarPorId((long)obralAtualizado.OrcamentoId);
                obra.Orcamento = orcamento;
            }

            obra.Nome = obralAtualizado.Nome ?? obra.Nome;
            obra.Descricao = obralAtualizado.Descricao ?? obra.Descricao;
            obra.Endereco = obralAtualizado.Endereco ?? obra.Endereco;
            obra.TipoObra = obralAtualizado.TipoObra ?? obra.TipoObra;
            obra.Valor = obralAtualizado.Valor ?? obra.Valor;
            obra.PrazoConclusao = obralAtualizado.PrazoConclusao ?? obra.PrazoConclusao;
            obra.OrcamentoId = obralAtualizado.OrcamentoId ?? obra.OrcamentoId;
            
            _repository.Editar(obra);
        }

        public void AlocarFuncionario(long id, long funcionarioId)
        {
            var funcionario = _funcionarioService.BuscarPorId(funcionarioId);

            var obra = BuscarPorId(id);

            if (obra.Funcionarios.Contains(funcionario))
            {
                throw new OperacaoInvalidaException(
                    $"Funcionário {funcionario.Nome} já está alocado na obra {obra.Nome}");
            }
            
            obra.Funcionarios.Add(funcionario);
            
            _repository.Editar(obra);
        }
        
        public void DesalocarFuncionario(long id, long funcionarioId)
        {
            var funcionario = _funcionarioService.BuscarPorId(funcionarioId);

            var obra = BuscarPorId(id);
            
            if (!obra.Funcionarios.Contains(funcionario))
            {
                throw new OperacaoInvalidaException(
                    $"Funcionário {funcionario.Nome} não está alocado na obra {obra.Nome}");
            }
            
            obra.Funcionarios.Remove(funcionario);
            
            _repository.Editar(obra);
        }
    }
}