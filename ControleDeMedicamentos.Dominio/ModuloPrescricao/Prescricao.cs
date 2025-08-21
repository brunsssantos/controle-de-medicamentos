using ControleDeMedicamentos.Dominio.ModuloPaciente;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeMedicamentos.Dominio.ModuloPrescricao;

public class Prescricao
{
    public Guid Id { get; set; }

    public string CrmMedico { get; set; }
    public DateTime DataEmissao { get; set; }
    public Paciente Paciente { get; set; }

    public List<MedicamentoPrescrito> MedicamentoPrescritos { get; set; }

    [ExcludeFromCodeCoverage]
    public Prescricao() { }

    public Prescricao(string crmMedico, Paciente paciente, List<MedicamentoPrescrito> medicamentoPrescritos)
    {
        Id = Guid.NewGuid();
        DataEmissao = DateTime.Now;
        CrmMedico = crmMedico;

        Paciente = paciente;
        MedicamentoPrescritos = medicamentoPrescritos;
    }
}