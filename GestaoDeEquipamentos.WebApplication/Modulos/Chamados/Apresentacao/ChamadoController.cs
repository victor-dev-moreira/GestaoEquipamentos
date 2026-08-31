using GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Infraestrutura;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
namespace GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Apresentacao;

public sealed class ChamadoController : Controller
{
    private readonly RepositorioChamadoEmArquivo repositorioChamado;
    private readonly RepositorioEquipamentoEmArquivo repositorioEquipamento;
    public ChamadoController(RepositorioChamadoEmArquivo repositorioChamado, RepositorioEquipamentoEmArquivo repositorioEquipamento)
    {
        this.repositorioChamado = repositorioChamado;
        this.repositorioEquipamento = repositorioEquipamento;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Chamado> chamados = repositorioChamado.SelecionarTodos();
        List<ListarChamadoViewModel> viewModels = new List<ListarChamadoViewModel>();

        foreach (Chamado chamado in chamados)
        {
            ListarChamadoViewModel vm = new ListarChamadoViewModel(
                chamado.Id,
                chamado.Titulo,
                chamado.Descricao,
                chamado.Equipamento,
                chamado.DataAbertura

            );

            viewModels.Add(vm);
        }

        return View(viewModels);
    }

    [HttpGet]

    public ActionResult Cadastrar()
    {
        CadastrarChamadoViewModel viewModel = new CadastrarChamadoViewModel(
            null,
            null,
            null,
            0,
            ObterEquipamento()

        ) with
        { EquipamentosDisponiveis = ObterEquipamento() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVm)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(cadastrarVm.EquipamentoId);

        if (equipamento == null)
            ModelState.AddModelError(nameof(cadastrarVm.EquipamentoId), "Selecione um equipamento válido");

        if (!ModelState.IsValid)
        {
            cadastrarVm = cadastrarVm with
            {
                EquipamentosDisponiveis = ObterEquipamento()
            };
            return View(cadastrarVm);
        }

        Chamado chamado = new Chamado(
            cadastrarVm.Titulo ?? string.Empty,
            cadastrarVm.Descricao,
            equipamento!,
            cadastrarVm.DataAbertura.GetValueOrDefault()

        );

        repositorioChamado.Cadastrar(chamado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]

    public ActionResult Editar(int id)
    {
        Chamado? chamado = repositorioChamado.SelecionarPorId(id);

        if (chamado == null)
            return NotFound();

        EditarChamadoViewModel vM = new(
            chamado.Id,
            chamado.Titulo,
            chamado.Descricao,
            chamado.DataAbertura,
            chamado.Equipamento.Id,
            ObterEquipamento()
        );

        return View(vM);
    }

    [HttpPost]

    public ActionResult Editar(int id, EditarChamadoViewModel viewModel)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(viewModel.EquipamentoId);

        if (equipamento == null)
            ModelState.AddModelError(nameof(viewModel.EquipamentoId), "Selecione um Equipamento válido.");

        if (!ModelState.IsValid)
        {
            viewModel = viewModel with
            {
                EquipamentosDisponiveis = ObterEquipamento()
            };

            return View(viewModel);
        }

        Chamado chamadoAtualizado = new(
            viewModel.Titulo ?? string.Empty,
            viewModel.Descricao,
            equipamento!,
            viewModel.DataAbertura.GetValueOrDefault()

        );

        bool conseguiuEditar = repositorioChamado.Editar(id, chamadoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }


    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Chamado? ChamadoSelecionado = repositorioChamado.SelecionarPorId(id);

        if (ChamadoSelecionado == null)
            return NotFound();

        ExcluirChamadoViewModel viewModel = new(
            ChamadoSelecionado.Id,
            ChamadoSelecionado.Titulo
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirChamadoViewModel viewModel)
    {
        bool conseguiuExcluir = repositorioChamado.Excluir(viewModel.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    private List<EquipamentoChamadoViewModel> ObterEquipamento()
    {
        List<EquipamentoChamadoViewModel> equipamentos = [];

        foreach (Equipamento equipamento in repositorioEquipamento.SelecionarTodos())
        {
            EquipamentoChamadoViewModel viewModelEquipamento = new EquipamentoChamadoViewModel(
                equipamento.Id,
                equipamento.Nome
            );

            equipamentos.Add(viewModelEquipamento);
        }

        return equipamentos;
    }
}