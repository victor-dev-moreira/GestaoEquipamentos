using GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Infraestrutura;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Infraestrutura;
using GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Infraestrutura;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura;

public static class InjecaoDeDependencia
{
    public static void AdicionarCamadaDeInfraestrutura(
        this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddScoped(services =>
        {
            ContextoJson contexto = new ContextoJson();

            contexto.Carregar();

            return contexto;
        });

        string connectionString = configuration.GetConnectionString("SqlServerDocker")
        ?? throw new InvalidOperationException("A String de conexão \"SqlServerDocker\" não foi configurada!");

        // Configurar Repositorios
        services.AddScoped<IRepositorioFabricante>(_ =>
        {
            return new RepositorioFabricanteEmSql(connectionString);
        });

        services.AddScoped<IRepositorioEquipamento>(_ =>
        {
            return new RepositorioEquipamentoEmSql(connectionString);
        });

        services.AddScoped<IRepositorioChamado>(_ =>
        {
            return new RepositorioChamadoEmSql(connectionString);
        });
    }
}