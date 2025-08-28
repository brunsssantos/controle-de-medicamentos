using Serilog;
using Serilog.Events;

namespace ControleDeMedicamentos.WebApp.DependencyInjection;

public static class SerilogConfig
{
    public static void AddSerilogConfig(this IServiceCollection services, ILoggingBuilder logging, IConfiguration configuration)
    {
        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "ControleDeMedicamentos", "erro.logs");

        // Variável de ambiente para a chave de licença do New Relic
        var licenseKey = configuration["NEWRELIC_LICENSE_KEY"];


        //Cria o Logger do Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console() // Loga no console
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error) // Loga em um arquivo de logs
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.eu.newrelic.com/log/v1",
                applicationName: "controle-de-medicamentos",
                licenseKey: licenseKey
            )
            .CreateLogger();

        logging.ClearProviders(); // Limpa os provedores de log padrão


        // Injeta o serviço do Serilog
        services.AddSerilog();
    }
}
