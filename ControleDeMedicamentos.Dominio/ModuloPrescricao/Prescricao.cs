using ControleDeMedicamentos.Dominio.Compartilhado;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.Dominio.ModuloPrescricao;

public class Prescricao : EntidadeBase<Prescricao>
{
    public Guid Id { get; set; }

    public string Descricao { get; set; }
    public string CrmMedico { get; set; }
    public DateTime DataEmissao { get; set; }
    public DateTime DataValidade { get; set; }
    public Paciente Paciente { get; set; }

    public Prescricao() { }

    public Prescricao(string Descricao, DateTime dataValidade, string crmMedico, Paciente paciente)
    {
        Id = Guid.NewGuid();
        DataEmissao = DateTime.Now;
        DataValidade = DataValidade;
        CrmMedico = crmMedico;
        Paciente = paciente;
    }

    public override void AtualizarRegistro(Prescricao registroAtualizado)
    {
        Descricao = registroAtualizado.Descricao;
        CrmMedico = registroAtualizado.CrmMedico;
        DataEmissao = registroAtualizado.DataEmissao;
        DataValidade = registroAtualizado.DataValidade;
        Paciente = registroAtualizado.Paciente;
    }

    public override string Validar()
    {
        string erros = "";

        if(string.IsNullOrWhiteSpace(Descricao))
            erros += "O campo 'Descrição' é obrigatório.\n";

        if (!Regex.IsMatch(CrmMedico, @"^\d{4,7}-?[A-Z]{2}$"))
            erros += "O campo 'CRM do Médico' deve seguir o padrão 1111000-UF.\n";

        if(DataValidade < DateTime.Now)
            erros += "A data de validade não pode ser menor que a data atual.\n";

        if (Paciente == null)
            erros += "O campo 'Paciente' é obrigatório.\n";

        return erros;

    }
}