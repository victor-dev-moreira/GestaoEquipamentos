using GestaoDeEquipamentos.WebApplication.Compartilhado.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Dominio;
namespace GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Dominio;

public class Chamado : EntidadeBase
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Equipamento Equipamento { get; set; }
    public DateTime DataAbertura { get; set; }
    public Chamado()
    {
    }

    public Chamado(string titulo, string descricao, Equipamento equipamento, DateTime dataAbertura)
    {
        Titulo = titulo;
        Descricao = descricao;
        Equipamento = equipamento;
        DataAbertura = dataAbertura;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Chamado chamadoAtualizado = (Chamado)entidadeAtualizada;

        Titulo = chamadoAtualizado.Titulo;
        Descricao = chamadoAtualizado.Descricao;
        Equipamento = chamadoAtualizado.Equipamento;
        DataAbertura = chamadoAtualizado.DataAbertura;
    }
}