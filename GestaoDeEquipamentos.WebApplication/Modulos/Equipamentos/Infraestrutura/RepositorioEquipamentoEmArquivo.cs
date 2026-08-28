using GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Dominio;

namespace GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Infraestrutura;

public sealed class RepositorioEquipamentoEmArquivo : RepositorioBaseEmArquivo<Equipamento>
{
    public RepositorioEquipamentoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Equipamento> ObterRegistros()
    {
        return contexto.Equipamentos;
    }
}