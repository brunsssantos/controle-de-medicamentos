using System.Text.Json.Serialization;
using System.Text.Json;
using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloRequisicaoMedicamentos;
using ControleDeMedicamentos.Dominio.ModuloPrescricao;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

public class ContextoDados
{
    public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
    public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
    public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    public List<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
    public List<RequisicaoEntrada> RequisicoesEntrada { get; set; } = new List<RequisicaoEntrada>();
    public List<RequisicaoSaida> RequisicoesSaida { get; set; } = new List<RequisicaoSaida>();
    public List<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();

    private string pastaArmazenamento = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "ControleDeMedicamentos"
    );

    private string arquivoArmazenamento = "dados.json";

    public ContextoDados() { }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento))
            Directory.CreateDirectory(pastaArmazenamento);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(
            json,
            jsonOptions
        )!;

        if (contextoArmazenado == null) return;

        Fornecedores = contextoArmazenado.Fornecedores;
        Pacientes = contextoArmazenado.Pacientes;
        Funcionarios = contextoArmazenado.Funcionarios;

        Medicamentos = contextoArmazenado.Medicamentos;
        RequisicoesEntrada = contextoArmazenado.RequisicoesEntrada;
        RequisicoesSaida = contextoArmazenado.RequisicoesSaida;
        Prescricoes = contextoArmazenado.Prescricoes;
    }
}