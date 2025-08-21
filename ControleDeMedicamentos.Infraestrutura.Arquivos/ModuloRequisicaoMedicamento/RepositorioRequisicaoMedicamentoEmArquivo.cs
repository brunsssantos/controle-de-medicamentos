using ControleDeMedicamentos.Dominio.ModuloRequisicaoMedicamentos;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloRequisicaoMedicamento;

public class RepositorioRequisicaoMedicamentoEmArquivo
{
    private ContextoDados contexto;
    private List<RequisicaoEntrada> requisicoesEntrada;
    private List<RequisicaoSaida> requisicoesSaida;

    public RepositorioRequisicaoMedicamentoEmArquivo(ContextoDados contexto)
    {
        this.contexto = contexto;

        requisicoesEntrada = contexto.RequisicoesEntrada;
        requisicoesSaida = contexto.RequisicoesSaida;
    }

    public void CadastrarRequisicaoEntrada(RequisicaoEntrada requisicao)
    {
        requisicoesEntrada.Add(requisicao);

        contexto.Salvar();
    }

    public void CadastrarRequisicaoSaida(RequisicaoSaida requisicao)
    {
        requisicoesSaida.Add(requisicao);

        contexto.Salvar();
    }

    public List<RequisicaoSaida> SelecionarRequisicoesSaida()
    {
        return requisicoesSaida;
    }
}