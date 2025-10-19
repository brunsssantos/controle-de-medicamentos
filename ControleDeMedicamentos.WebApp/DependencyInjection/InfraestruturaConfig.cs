using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloPrescricao;
using ControleDeMedicamentos.Infraestrutura.SqlServer.ModuloRequisicaoMedicamento;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ControleDeMedicamentos.WebApp.DependencyInjection;

public static class InfraestruturaConfig
{
    public static void AddCamadaInfraestutura(this IServiceCollection services, IConfiguration configuracao)
    {
        services.AddScoped<IDbConnection>(_ => 
        {
            var connectionString = configuracao["SQL_CONNECTION_STRING"]; 
            return new SqlConnection(connectionString);
        });

        services.AddScoped<RepositorioPacienteEmSql>();
        services.AddScoped<RepositorioFornecedorEmSql>();
        services.AddScoped<RepositorioFuncionarioEmSql>();
        services.AddScoped<RepositorioMedicamentoEmSql>();
        services.AddScoped<RepositorioPrescricaoEmSql>();
        services.AddScoped<RepositorioRequisicaoMedicamentoEmSql>();


        services.AddScoped((_) => new ContextoDados(true)); //delegate / lambda expression
       // services.AddScoped<RepositorioFuncionarioEmArquivo>(); // Injeta uma instância do serviço por requisição (ação) HTTP, essa instância acompanha a requisição do cliente
       // services.AddScoped<RepositorioFornecedorEmArquivo>();
       // services.AddScoped<RepositorioMedicamentoEmArquivo>();
       // services.AddScoped<RepositorioPacienteEmArquivo>();
       // services.AddScoped<RepositorioPrescricaoEmArquivo>();
       // services.AddScoped<RepositorioRequisicaoMedicamentoEmArquivo>();
    }
}
