using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloRequisicaoMedicamento;
using Serilog;
using Serilog.Events;

namespace ControleDeMedicamentos.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Inje��o de depend�ncias criadas por n�s
        builder.Services.AddScoped((_) => new ContextoDados(true)); //delegate / lambda expression
        builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>(); // Injeta uma inst�ncia do servi�o por requisi��o (a��o) HTTP, essa inst�ncia acompanha a requisi��o do cliente
        builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();
        builder.Services.AddScoped<RepositorioMedicamentoEmArquivo>();
        builder.Services.AddScoped<RepositorioPacienteEmArquivo>();
        builder.Services.AddScoped<RepositorioPrescricaoEmArquivo>();
        builder.Services.AddScoped<RepositorioRequisicaoMedicamentoEmArquivo>();

        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "ControleDeMedicamentos", "erro.logs");

        // Vari�vel de ambiente para a chave de licen�a do New Relic
        var licenseKey = builder.Configuration["NEWRELIC_LICENSE_KEY"];

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console() // Loga no console
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error) // Loga em um arquivo de logs
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.eu.newrelic.com/log/v1",
                applicationName: "controle-de-medicamentos",
                licenseKey: licenseKey
            ) 
            .CreateLogger();

        builder.Logging.ClearProviders(); // Limpa os provedores de log padr�o

        builder.Services.AddSerilog(); // Adiciona o Serilog como provedor de log

        // Inje��o de depend�ncias da Microsoft.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
