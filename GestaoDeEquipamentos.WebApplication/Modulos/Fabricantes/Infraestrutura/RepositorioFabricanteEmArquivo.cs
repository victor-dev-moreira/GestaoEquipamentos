using GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Dominio;

namespace GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Infraestrutura;

public sealed class RepositorioFabricanteEmArquivo : RepositorioBaseEmArquivo<Equipamento>
{
    public RepositorioFabricanteEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Equipamento> ObterRegistros()
    {
        return contexto.Fabricantes;
    }
}