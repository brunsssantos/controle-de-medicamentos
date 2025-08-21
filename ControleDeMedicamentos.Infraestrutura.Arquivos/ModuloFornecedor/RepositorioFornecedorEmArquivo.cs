using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;

public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>
{
    public RepositorioFornecedorEmArquivo(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> ObterRegistros()
    {
        return contextoDados.Fornecedores;
    }
}