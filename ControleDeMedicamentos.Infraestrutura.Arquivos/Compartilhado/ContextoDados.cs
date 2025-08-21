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
    private string pastaArmazenamento = "C:\\temp";
    private string arquivoArmazenamento = "dados-controle-medicamento.json";

    public List<Fornecedor> Fornecedores { get; set; }
    public List<Paciente> Pacientes { get; set; }
    public List<Funcionario> Funcionarios { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    public List<RequisicaoEntrada> RequisicoesEntrada { get; set; }
    public List<RequisicaoSaida> RequisicoesSaida { get; set; }
    public List<Prescricao> Prescricoes { get; set; }

    public ContextoDados()
    {
        Fornecedores = new List<Fornecedor>();
        Pacientes = new List<Paciente>();
        Funcionarios = new List<Funcionario>();
        Medicamentos = new List<Medicamento>();
        RequisicoesEntrada = new List<RequisicaoEntrada>();
        RequisicoesSaida = new List<RequisicaoSaida>();
        Prescricoes = new List<Prescricao>();
    }

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