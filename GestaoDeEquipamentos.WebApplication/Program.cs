using GestaoDeEquipamentos.WebApplication.Compartilhado.Apresentacao;
using GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura;

var builder = WebApplication.CreateBuilder(args);

// Config. a infra. (Aquivo, Banco de dados, Logs, Cachês, etc...)
builder.Services.AdicionarCamadaDeInfraestrutura(builder.Configuration);

// Config. o MVC / Apresentação
builder.Services.AdicionarCamadaDeApresentacao();

var app = builder.Build();

// Middlewares
app.UseRouting();
app.MapDefaultControllerRoute();

// Permite app ler wwwroot
app.UseStaticFiles();

// Executa o servidor
app.Run();
