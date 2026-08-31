using GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Dominio;
namespace GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Infraestrutura;

public sealed class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>
{
    public RepositorioChamadoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Chamado> ObterRegistros()
    {
        return contexto.Chamados;
    }
}