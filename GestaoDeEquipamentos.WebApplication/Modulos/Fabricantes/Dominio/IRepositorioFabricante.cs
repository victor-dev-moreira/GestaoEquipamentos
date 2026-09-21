using GestaoDeEquipamentos.WebApplication.Compartilhado.Dominio;
namespace GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Dominio;

public interface IRepositorioFabricante
{
    void Cadastrar(Fabricante novoRegistro);
    bool Editar(int idSelecionado, Fabricante entidadeAtualizada);
    bool Excluir(int idSelecionado);
    Fabricante? SelecionarPorId(int idSelecionado);
    List<Fabricante> SelecionarTodos();
}
