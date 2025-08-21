using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeMedicamentos.Dominio.ModuloRequisicaoMedicamentos;

public class RequisicaoEntrada
{
    public Guid Id { get; set; }
    public DateTime DataOcorrencia { get; set; }
    public Funcionario Funcionario { get; set; }
    public Medicamento Medicamento { get; set; }
    public int QuantidadeRequisitada { get; set; }

    [ExcludeFromCodeCoverage]
    public RequisicaoEntrada() { }

    public RequisicaoEntrada(
        Funcionario funcionario,
        Medicamento medicamento,
        int quantidadeRequisitada
    )
    {
        Id = Guid.NewGuid();
        DataOcorrencia = DateTime.Now;
        Funcionario = funcionario;
        Medicamento = medicamento;
        QuantidadeRequisitada = quantidadeRequisitada;
    }

    public string Validar()
    {
        string erros = string.Empty;

        if (Funcionario == null)
            erros += "O campo \"Funcionário\" é obrigatório.";

        if (Medicamento == null)
            erros += "O campo \"Medicamento\" é obrigatório.";

        if (QuantidadeRequisitada < 1)
            erros += "O campo \"Quantidade Requisitada\" necessita conter um valor positivo.";

        return erros;
    }
}