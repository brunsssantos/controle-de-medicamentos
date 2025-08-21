using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>
{
    public RepositorioPacienteEmArquivo(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Paciente> ObterRegistros()
    {
        return contextoDados.Pacientes;
    }
}