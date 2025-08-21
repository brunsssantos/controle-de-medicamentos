using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
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
        builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>();          // Injeta uma inst�ncia do servi�o por requisi��o (a��o) HTTP, essa inst�ncia acompanha a requisi��o do cliente
        //builder.Services.AddSingleton<RepositorioFuncionarioEmArquivo>();     // Injeta uma inst�ncia �nica do servi�o globalmente
        //builder.Services.AddTransient<RepositorioFuncionarioEmArquivo>();     // Injeta uma inst�ncia nova do servi�o toda vez que houver uma depend�ncia ao longo de uma requisi��o

        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "ControleDeMedicamentos", "erro.logs");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console() // Loga no console
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error) // Loga em um arquivo de logs
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.eu.newrelic.com/log/v1",
                applicationName: "controle-de-medicamentos",
                licenseKey: "eu01xx82bbe544cf4a72136351f3b468FFFFNRAL"
            ) // Loga no New Relic
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
