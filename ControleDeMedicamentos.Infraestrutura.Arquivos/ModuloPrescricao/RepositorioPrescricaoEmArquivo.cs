using ControleDeMedicamentos.Dominio.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;

public class RepositorioPrescricaoEmArquivo : RepositorioBaseEmArquivo<Prescricao>
{
    private ContextoDados contexto;
    private List<Prescricao> registros = new List<Prescricao>();

    public RepositorioPrescricaoEmArquivo(ContextoDados contexto) : base(contexto)
    {
        this.contexto = contexto;
        registros = contexto.Prescricoes;
    }

    public void CadastrarRegistro(Prescricao novoRegistro)
    {
        registros.Add(novoRegistro);

        contexto.Salvar();
    }

    public List<Prescricao> SelecionarRegistros()
    {
        return registros;
    }

    protected override List<Prescricao> ObterRegistros()
    {
        return contexto.Prescricoes;
    }
}