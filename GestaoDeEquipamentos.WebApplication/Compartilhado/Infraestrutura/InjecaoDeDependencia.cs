using GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Infraestrutura;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Infraestrutura;
using GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Infraestrutura;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura;

public static class InjecaoDeDependencia
{
    public static void AdicionarCamadaDeInfraestrutura(this IServiceCollection services)
    {
        services.AddScoped(services =>
        {
            ContextoJson contexto = new ContextoJson();

            contexto.Carregar();

            return contexto;
        });

        // Configurar Repositorios
        services.AddScoped<RepositorioFabricanteEmArquivo>();
        services.AddScoped<RepositorioEquipamentoEmArquivo>();
        services.AddScoped<RepositorioChamadoEmArquivo>();
    }
}