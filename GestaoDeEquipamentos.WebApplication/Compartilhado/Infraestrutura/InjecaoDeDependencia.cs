using GestaoDeEquipamentos.WebApp.Compartilhado.Infraestrutura.Arquivos;
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
    }
}