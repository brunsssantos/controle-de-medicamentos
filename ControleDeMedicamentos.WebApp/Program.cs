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

        // Injeção de dependências criadas por nós
        builder.Services.AddScoped((_) => new ContextoDados(true)); //delegate / lambda expression
        builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>();          // Injeta uma instância do serviço por requisição (ação) HTTP, essa instância acompanha a requisição do cliente
        //builder.Services.AddSingleton<RepositorioFuncionarioEmArquivo>();     // Injeta uma instância única do serviço globalmente
        //builder.Services.AddTransient<RepositorioFuncionarioEmArquivo>();     // Injeta uma instância nova do serviço toda vez que houver uma dependência ao longo de uma requisição

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

        builder.Logging.ClearProviders(); // Limpa os provedores de log padrão

        builder.Services.AddSerilog(); // Adiciona o Serilog como provedor de log

        // Injeção de dependências da Microsoft.
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
