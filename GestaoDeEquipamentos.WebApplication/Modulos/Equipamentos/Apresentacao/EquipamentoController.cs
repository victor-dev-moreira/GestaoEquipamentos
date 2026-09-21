using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Infraestrutura;
using GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
namespace GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Apresentacao;

public sealed class EquipamentoController : Controller
{
    private readonly IRepositorioEquipamento repositorio;
    private readonly IRepositorioFabricante repositorioFabricante;
    public EquipamentoController(
        IRepositorioEquipamento repositorio,
        IRepositorioFabricante repositorioFabricante)
    {
        this.repositorio = repositorio;
        this.repositorioFabricante = repositorioFabricante;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Dominio.Equipamento> equipamentos = repositorio.SelecionarTodos();
        List<ListarEquipamentoViewModel> viewModels = new List<ListarEquipamentoViewModel>();

        foreach (Dominio.Equipamento equipamento in equipamentos)
        {
            ListarEquipamentoViewModel vm = new ListarEquipamentoViewModel(
                equipamento.Id,
                equipamento.Nome,
                equipamento.PrecoAquisicao,
                equipamento.Fabricante,
                equipamento.DataFabricacao

            );

            viewModels.Add(vm);
        }

        return View(viewModels);
    }

    [HttpGet]

    public ActionResult Cadastrar()
    {
        CadastrarEquipamentoViewModel viewModel = new CadastrarEquipamentoViewModel(
            null,
            null,
            null,
            0,
            ObterFabricantes()

        ) with
        { FabricantesDisponiveis = ObterFabricantes() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVm)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(cadastrarVm.FabricanteId);

        if (fabricante == null)
            ModelState.AddModelError(nameof(cadastrarVm.FabricanteId), "Selecione um fabricante válido");

        if (!ModelState.IsValid)
        {
            cadastrarVm = cadastrarVm with
            {
                FabricantesDisponiveis = ObterFabricantes()
            };
            return View(cadastrarVm);
        }

        Dominio.Equipamento equipamento = new Dominio.Equipamento(
            cadastrarVm.Nome ?? string.Empty,
            cadastrarVm.PrecoAquisicao.GetValueOrDefault(),
            cadastrarVm.DataFabricacao.GetValueOrDefault(),
            fabricante!
        );

        repositorio.Cadastrar(equipamento);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]

    public ActionResult Editar(int id)
    {
        Dominio.Equipamento? equipamento = repositorio.SelecionarPorId(id);

        if (equipamento == null)
            return NotFound();

        EditarEquipamentoViewModel vM = new(
            equipamento.Id,
            equipamento.Nome,
            equipamento.PrecoAquisicao,
            equipamento.DataFabricacao,
            equipamento.Fabricante.Id,
            ObterFabricantes()
        );

        return View(vM);
    }

    [HttpPost]

    public ActionResult Editar(int id, EditarEquipamentoViewModel viewModel)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(viewModel.FabricanteId);

        if (fabricante == null)
            ModelState.AddModelError(nameof(viewModel.FabricanteId), "Selecione um fabricante válido.");

        if (!ModelState.IsValid)
        {
            viewModel = viewModel with
            {
                FabricantesDisponiveis = ObterFabricantes()
            };

            return View(viewModel);
        }

        Dominio.Equipamento equipamentoAtualizado = new(
            viewModel.Nome ?? string.Empty,
            viewModel.PrecoAquisicao.GetValueOrDefault(),
            viewModel.DataFabricacao.GetValueOrDefault(),
            fabricante!
        );

        bool conseguiuEditar = repositorio.Editar(id, equipamentoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Dominio.Equipamento? equipamentoSelecionado = repositorio.SelecionarPorId(id);

        if (equipamentoSelecionado == null)
            return NotFound();

        ExcluirEquipamentoViewModel viewModel = new(
            equipamentoSelecionado.Id,
            equipamentoSelecionado.Nome
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirEquipamentoViewModel viewModel)
    {
        bool conseguiuExcluir = repositorio.Excluir(viewModel.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    private List<FabricanteEquipamentoViewModel> ObterFabricantes()
    {
        List<FabricanteEquipamentoViewModel> fabricantes = [];

        foreach (Fabricante fabricante in repositorioFabricante.SelecionarTodos())
        {
            FabricanteEquipamentoViewModel viewModelFabricante = new FabricanteEquipamentoViewModel(
                fabricante.Id,
                fabricante.Nome
            );

            fabricantes.Add(viewModelFabricante);
        }

        return fabricantes;
    }
}
