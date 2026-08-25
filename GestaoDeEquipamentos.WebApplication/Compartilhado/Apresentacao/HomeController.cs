using Microsoft.AspNetCore.Mvc;
namespace GestaoDeEquipamentos.WebApplication.Compartilhado.Apresentacao;

public sealed class HomeController : Controller
{
    [HttpGet]

    public ActionResult Index()
    {
        return View();
    }
}