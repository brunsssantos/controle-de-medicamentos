using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPaciente;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

public class PacienteController : Controller
{
    private readonly RepositorioPacienteEmSql repositorioPaciente; // dependência
    private readonly ILogger<PacienteController> logger;

    // Inversão de controle
    public PacienteController(
        RepositorioPacienteEmSql repositorioPaciente,
        ILogger<PacienteController> logger
    )
    {
        this.repositorioPaciente = repositorioPaciente;
        this.logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var pacientes = repositorioPaciente.SelecionarRegistros();

        logger.LogInformation("{QuantidadeRegistros} registros foram selecionados.", pacientes.Count);

        var visualizarVm = new VisualizarPacientesViewModel(pacientes);

        return View(visualizarVm);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var cadastrarVm = new CadastrarPacienteViewModel();

        return View(cadastrarVm);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        var entidade = new Paciente(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.Cpf,
            cadastrarVm.CartaoSus

        );

        repositorioPaciente.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var registro = repositorioPaciente.SelecionarRegistroPorId(id);

        var editarVm = new EditarPacienteViewModel(
            registro.Id,
            registro.Nome,
            registro.Telefone,
            registro.Cpf,
            registro.CartaoSus

        );

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarPacienteViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var pacienteEditado = new Paciente(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.Cpf,
            editarVm.CartaoSus

        );

        repositorioPaciente.EditarRegistro(editarVm.Id, pacienteEditado);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var registro = repositorioPaciente.SelecionarRegistroPorId(id);

        var excluirVm = new ExcluirPacienteViewModel(
            registro.Id,
            registro.Nome
        );

        return View(excluirVm);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirPacienteViewModel excluirVm)
    {
        repositorioPaciente.ExcluirRegistro(excluirVm.Id);

        return RedirectToAction(nameof(Index));
    }
}
