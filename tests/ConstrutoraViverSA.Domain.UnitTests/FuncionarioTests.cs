using AutoFixture;
using ConstrutoraViverSA.Domain.Enums;
using ConstrutoraViverSA.Domain.Exceptions;
using ConstrutoraViverSA.Domain.Tests.Stubs;
using FluentAssertions;
using Xunit;

namespace ConstrutoraViverSA.Domain.Tests;

public class FuncionarioTests
{
    private readonly Fixture _fixture = new Fixture();
    private const string CPF_VALIDO = "14533067573";
    private const string CPF_INVALIDO = "14536067573";
    
    [Fact]
    public void Construtor_ComDadosValidos_DeveConstruirCorretamente()
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = _fixture.Create<DateTime>();
        var genero = _fixture.Create<Genero>();
        var cpf = CPF_VALIDO;
        var numCtps = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();

        var result = new Funcionario(nome, dataNascimento, genero, cpf, numCtps, endereco, email, telefone, cargo);

        result.Should().NotBeNull();
        result.Nome.Should().Be(nome);
        result.DataNascimento.Should().Be(dataNascimento);
        result.Genero.Should().Be(genero);
        result.Cpf.Should().Be(cpf);
        result.NumCtps.Should().Be(numCtps);
        result.Endereco.Should().Be(endereco);
        result.Email.Should().Be(email);
        result.Telefone.Should().Be(telefone);
        result.Cargo.Should().Be(cargo);
    }

    [Fact]
    public void Construtor_ComNomeInvalido_DeveLancarExcecao()
    {
        var dataNascimento = _fixture.Create<DateTime>();
        var genero = _fixture.Create<Genero>();
        var cpf = CPF_VALIDO;
        var numCtps = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;

        var result = () =>
        {
            funcionario = new Funcionario(null, dataNascimento, genero, cpf, numCtps, endereco, email, telefone, cargo);
        };

        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComDataDeNascimentoNull_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var genero = _fixture.Create<Genero>();
        var cpf = CPF_VALIDO;
        var numCtps = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;
        
        var result = () =>
        {
            funcionario = new Funcionario(nome, null, genero, cpf, numCtps, endereco, email, telefone, cargo);
        };
        
        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
     
    [Fact]
    public void Construtor_ComDataDeNascimentoInvalida_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = DateTime.MinValue;
        var genero = _fixture.Create<Genero>();
        var cpf = CPF_VALIDO;
        var numCtps = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;
        
        var result = () =>
        {
            funcionario = new Funcionario(nome, dataNascimento, genero, cpf, numCtps, endereco, email, telefone, cargo);
        };

        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComGeneroInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = _fixture.Create<DateTime>();
        var cpf = CPF_VALIDO;
        var numCtps = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;
        
        var result = () =>
        {
            funcionario = new Funcionario(nome, dataNascimento, null, cpf, numCtps, endereco, email, telefone, cargo);
        };

        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(CPF_INVALIDO)]
    [InlineData("1234567890111")]
    public void Construtor_ComCpfInvalido_DeveLancarExcecao(string cpf)
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = _fixture.Create<DateTime>();
        var genero = _fixture.Create<Genero>();
        var numCtps = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;
        
        var result = () =>
        {
            funcionario = new Funcionario(nome, dataNascimento, genero, cpf, numCtps, endereco, email, telefone, cargo);
        };
        
        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComNumCtpsInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = _fixture.Create<DateTime>();
        var cpf = CPF_VALIDO;
        var genero = _fixture.Create<Genero>();
        var endereco = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;
        
        var result = () =>
        {
            funcionario = new Funcionario(nome, dataNascimento, genero, cpf, null, endereco, email, telefone, cargo);
        };
        
        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComEnderecoInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = _fixture.Create<DateTime>();
        var cpf = CPF_VALIDO;
        var genero = _fixture.Create<Genero>();
        var email = _fixture.Create<string>();
        var numCtps = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;
        
        var result = () =>
        {
            funcionario = new Funcionario(nome, dataNascimento, genero, cpf, numCtps, null, email, telefone, cargo);
        };
        
        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComEmailInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = _fixture.Create<DateTime>();
        var cpf = CPF_VALIDO;
        var genero = _fixture.Create<Genero>();
        var numCtps = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;
        
        var result = () =>
        {
            funcionario = new Funcionario(nome, dataNascimento, genero, cpf, numCtps, endereco, null, telefone, cargo);
        };
        
        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComTelefoneInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = _fixture.Create<DateTime>();
        var cpf = CPF_VALIDO;
        var genero = _fixture.Create<Genero>();
        var numCtps = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var cargo = _fixture.Create<Cargo>();
        Funcionario? funcionario = null;

        var result = () =>
        {
            funcionario = new Funcionario(nome, dataNascimento, genero, cpf, numCtps, endereco, email, null, cargo);
        };

        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }
    
    [Fact]
    public void Construtor_ComCargoInvalido_DeveLancarExcecao()
    {
        var nome = _fixture.Create<string>();
        var dataNascimento = _fixture.Create<DateTime>();
        var cpf = CPF_VALIDO;
        var genero = _fixture.Create<Genero>();
        var numCtps = _fixture.Create<string>();
        var email = _fixture.Create<string>();
        var endereco = _fixture.Create<string>();
        var telefone = _fixture.Create<string>();
        Funcionario? funcionario = null;
        
        var result = () =>
        {
            funcionario = new Funcionario(nome, dataNascimento, genero, cpf, numCtps, endereco, email, telefone, null);
        };

        funcionario.Should().BeNull();
        result.Should().Throw<FuncionarioInvalidoException>();
    }

    [Fact]
    public void SetNome_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var nomeNovo = _fixture.Create<string>();
        
        funcionario.SetNome(nomeNovo);

        funcionario.Nome.Should().Be(nomeNovo);
    }
    
    [Fact]
    public void SetNome_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var nomeAntigo = funcionario.Nome;
        
        funcionario.SetNome(null);

        funcionario.Nome.Should().Be(nomeAntigo);
    }
    
    [Fact]
    public void SetDataNascimento_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var dataNascimentoNova = DateTime.Now;
        
        funcionario.SetDataNascimento(dataNascimentoNova);

        funcionario.DataNascimento.Should().Be(dataNascimentoNova);
    }
    
    [Fact]
    public void SetDataNascimento_ComDadoNull_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var dataNascimentoAntiga = funcionario.DataNascimento;
        
        funcionario.SetDataNascimento(null);

        funcionario.DataNascimento.Should().Be(dataNascimentoAntiga);
    }
    
    [Fact]
    public void SetDataNascimento_ComDadoInvalido_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var dataNascimentoAntiga = funcionario.DataNascimento;
        
        funcionario.SetDataNascimento(DateTime.MinValue);

        funcionario.DataNascimento.Should().Be(dataNascimentoAntiga);
    }
    
    [Fact]
    public void SetGenero_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var generoNovo = Genero.Feminino;
        
        funcionario.SetGenero(generoNovo);

        funcionario.Genero.Should().Be(generoNovo);
    }
    
    [Fact]
    public void SetGenero_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var generoAntigo = funcionario.Genero;
        
        funcionario.SetGenero(null);

        funcionario.Genero.Should().Be(generoAntigo);
    }
    
    [Fact]
    public void SetCpf_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var cpfNovo = CPF_VALIDO;
        
        funcionario.SetCpf(cpfNovo);

        funcionario.Cpf.Should().Be(cpfNovo);
    }
    
    [Theory]
    [InlineData(CPF_INVALIDO)]
    [InlineData(null)]
    [InlineData("123456789111")]
    public void SetCpf_ComDadosInvalidos_NaoDeveRealizarAlteracao(string cpfNovo)
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var cpfAntigo = funcionario.Cpf;
        
        funcionario.SetCpf(cpfNovo);

        funcionario.Cpf.Should().Be(cpfAntigo);
    }
    
    [Fact]
    public void SetNumCtps_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var numCtpsNovo = _fixture.Create<string>();
        
        funcionario.SetNumCtps(numCtpsNovo);

        funcionario.NumCtps.Should().Be(numCtpsNovo);
    }
    
    [Fact]
    public void SetNumCtps_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var numCtpsAntigo = funcionario.NumCtps;
        
        funcionario.SetNumCtps(null);

        funcionario.NumCtps.Should().Be(numCtpsAntigo);
    }
    
    [Fact]
    public void SetEndereco_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var enderecoNovo = _fixture.Create<string>();
        
        funcionario.SetEndereco(enderecoNovo);

        funcionario.Endereco.Should().Be(enderecoNovo);
    }
    
    [Fact]
    public void SetEndereco_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var enderecoAntigo = funcionario.Endereco;
        
        funcionario.SetEndereco(null);

        funcionario.Endereco.Should().Be(enderecoAntigo);
    }
    
    [Fact]
    public void SetEmail_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var emailNovo = _fixture.Create<string>();
        
        funcionario.SetEmail(emailNovo);

        funcionario.Email.Should().Be(emailNovo);
    }
    
    [Fact]
    public void SetEmail_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var emailAntigo = funcionario.Email;
        
        funcionario.SetEmail(null);

        funcionario.Email.Should().Be(emailAntigo);
    }
    
    [Fact]
    public void SetTelefone_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var telefoneNovo = _fixture.Create<string>();
        
        funcionario.SetTelefone(telefoneNovo);

        funcionario.Telefone.Should().Be(telefoneNovo);
    }
    
    [Fact]
    public void SetTelefone_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var telefoneAntigo = funcionario.Telefone;
        
        funcionario.SetTelefone(null);

        funcionario.Telefone.Should().Be(telefoneAntigo);
    }
    
    [Fact]
    public void SetCargo_ComDadosValidos_DeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var cargoNovo = Cargo.Almoxarife;
        
        funcionario.SetCargo(cargoNovo);

        funcionario.Cargo.Should().Be(cargoNovo);
    }
    
    [Fact]
    public void SetCargo_ComDadosInvalidos_NaoDeveRealizarAlteracao()
    {
        var funcionario = FuncionarioStub.Valido(_fixture);
        var cargoAntigo = funcionario.Cargo;
        
        funcionario.SetCargo(null);

        funcionario.Cargo.Should().Be(cargoAntigo);
    }
}