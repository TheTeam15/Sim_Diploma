using System;
using System.Collections.Generic;
using System.Linq;

// Delegados do Model
public delegate void ResultadoOperacaoEventHandler(string mensagem);
public delegate void InstituicaoGuardadaEventHandler(Instituicao instituicao);
public delegate void CursoCriadoEventHandler(Curso curso);
public delegate void EdicaoCriadaEventHandler(Edicao edicao);
public delegate void EstadoEdicaoAlteradoEventHandler(Edicao edicao);

// Responsável pela lógica de negócio da aplicação
// Valida dados, aplica regras de consistência e mantém o estado interno
public class CursosModel
{
    // Estruturas internas do Model para Instituicao, Curso e Edicao
    private Instituicao? instituicao;
    private readonly List<Curso> cursos;
    private readonly List<Edicao> edicoes;

    // Eventos do Model
    public event ResultadoOperacaoEventHandler? ResultadoOperacao;
    public event InstituicaoGuardadaEventHandler? InstituicaoGuardada;
    public event CursoCriadoEventHandler? CursoCriado;
    public event EdicaoCriadaEventHandler? EdicaoCriada;
    public event EstadoEdicaoAlteradoEventHandler? EstadoEdicaoAlterado;

    public CursosModel()
    {
        cursos = new List<Curso>();
        edicoes = new List<Edicao>();
    }

    // Método auxiliar para validação antecipada
    public bool ExisteCurso(string codigoCurso)
    {
        return cursos.Any(c =>
            c.Codigo.Equals(codigoCurso, StringComparison.OrdinalIgnoreCase));
    }

    // Método separado para verificar existência de edição
    public bool ExisteEdicao(string codigoCurso, string codigoEdicao)
    {
        return edicoes.Any(e =>
            e.CodigoCurso.Equals(codigoCurso, StringComparison.OrdinalIgnoreCase) &&
            e.CodigoEdicao.Equals(codigoEdicao, StringComparison.OrdinalIgnoreCase));
    }


    // GUARDAR INSTITUIÇÃO

    public void GuardarInstituicao(string nomeInstituicao)
    {
        try
        {
            instituicao = new Instituicao(nomeInstituicao);

            ResultadoOperacao?.Invoke($"Instituição '{instituicao.Nome}' guardada com sucesso.");
            InstituicaoGuardada?.Invoke(instituicao);
        }
        catch (Exception ex)
        {
            ResultadoOperacao?.Invoke($"Erro ao guardar instituição: {ex.Message}");
        }
    }

    // CRIAR CURSO

    public void CriarCurso(string codigoCurso)
    {
        try
        {
            if (ExisteCurso(codigoCurso))
                throw new CursoDuplicadoException();

            Curso curso = new Curso(codigoCurso);
            cursos.Add(curso);

            ResultadoOperacao?.Invoke($"Curso '{curso.Codigo}' criado com sucesso.");
            CursoCriado?.Invoke(curso);
        }
        catch (Exception ex)
        {
            ResultadoOperacao?.Invoke($"Erro ao criar curso: {ex.Message}");
        }
    }


    // CRIAR EDIÇÃO

    public void CriarEdicao(string codigoCurso, string codigoEdicao)
    {
        try
        {
            if (!ExisteCurso(codigoCurso))
                throw new CursoNaoExisteException("O curso indicado não existe.");

            if (ExisteEdicao(codigoCurso, codigoEdicao))
                throw new EdicaoDuplicadaException("Já existe uma edição com esse código nesse curso.");

            Edicao edicao = new Edicao(codigoCurso, codigoEdicao);
            edicoes.Add(edicao);

            ResultadoOperacao?.Invoke(
                $"Edição '{edicao.CodigoEdicao}' criada com sucesso no curso '{edicao.CodigoCurso}'.");
            EdicaoCriada?.Invoke(edicao);
        }
        catch (Exception ex)
        {
            ResultadoOperacao?.Invoke($"Erro ao criar edição: {ex.Message}");
        }
    }


    // ALTERAR ESTADO DA EDIÇÃO

    public void AlterarEstadoEdicao(string codigoCurso, string codigoEdicao, EstadoEdicao novoEstado)
    {
        try
        {
            Edicao? edicao = edicoes.FirstOrDefault(e =>
                e.CodigoCurso.Equals(codigoCurso, StringComparison.OrdinalIgnoreCase) &&
                e.CodigoEdicao.Equals(codigoEdicao, StringComparison.OrdinalIgnoreCase));

            if (edicao == null)
                throw new EdicaoNaoExisteException();

            edicao.AlterarEstado(novoEstado);

            ResultadoOperacao?.Invoke(
                $"Estado da edição '{edicao.CodigoEdicao}' alterado para '{edicao.Estado}'.");
            EstadoEdicaoAlterado?.Invoke(edicao);
        }
        catch (Exception ex)
        {
            ResultadoOperacao?.Invoke($"Erro ao alterar estado da edição: {ex.Message}");
        }
    }
}