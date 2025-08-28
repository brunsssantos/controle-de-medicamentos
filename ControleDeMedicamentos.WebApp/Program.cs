using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloRequisicaoMedicamento;
using ControleDeMedicamentos.WebApp.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace ControleDeMedicamentos.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Injeção de dependências criadas por nós
        builder.Services.AddCamadaInfraestutura();

        //Static + Extension Method
        builder.Services.AddSerilogConfig(builder.Logging, builder.Configuration);

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



    private static void AddCamadaInfraestutura(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped((_) => new ContextoDados(true)); //delegate / lambda expression
        builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>(); // Injeta uma instância do serviço por requisição (ação) HTTP, essa instância acompanha a requisição do cliente
        builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();
        builder.Services.AddScoped<RepositorioMedicamentoEmArquivo>();
        builder.Services.AddScoped<RepositorioPacienteEmArquivo>();
        builder.Services.AddScoped<RepositorioPrescricaoEmArquivo>();
        builder.Services.AddScoped<RepositorioRequisicaoMedicamentoEmArquivo>();
    }
}
