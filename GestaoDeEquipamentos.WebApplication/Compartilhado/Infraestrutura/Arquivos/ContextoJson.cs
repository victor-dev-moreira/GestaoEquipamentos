using System.Text.Json;
using System.Text.Json.Serialization;
using GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApplication.Modulos.Fabricantes.Dominio;

namespace GestaoDeEquipamentos.WebApplication.Compartilhado.Infraestrutura.Arquivos;

public class ContextoJson
{
    private readonly string caminhoArquivoDados;
    public List<Fabricante> Fabricantes { get; set; } = new List<Fabricante>();
    public List<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();
    public List<Chamado> Chamados { get; set; } = new List<Chamado>();

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorioAplicativo = Path.Join(caminhoAppData, "GestaoDeEquipamentos-Backend");

        Directory.CreateDirectory(caminhoDiretorioAplicativo);

        caminhoArquivoDados = Path.Join(caminhoDiretorioAplicativo, "dados.json");
    }
    public void Salvar()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, options);

        File.WriteAllText(caminhoArquivoDados, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivoDados))
            return;

        string jsonString = File.ReadAllText(caminhoArquivoDados);

        if (string.IsNullOrWhiteSpace(jsonString))
        {
            Carregar(CarregarDadosPredefinidos());
            return;
        }

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo =
            JsonSerializer.Deserialize<ContextoJson>(jsonString, options);

        if (contextoSalvo == null || !contextoSalvo.PossuiDados())
            contextoSalvo = CarregarDadosPredefinidos();

        Carregar(contextoSalvo);
    }

    private void Carregar(ContextoJson contexto)
    {
        Fabricantes = contexto.Fabricantes;
        Equipamentos = contexto.Equipamentos;
        Chamados = contexto.Chamados;
    }

    public ContextoJson CarregarDadosPredefinidos()
    {
        ContextoJson contextoPredefinido = new ContextoJson();

        contextoPredefinido.Fabricantes.AddRange(new List<Fabricante>
        {
            new Fabricante("CR Vasco Da Gama", "contato@vascodagama.com.br", "(11) 1111-1111") {Id = 1}
        });

        return contextoPredefinido;
    }

    private bool PossuiDados()
    {
        return Fabricantes.Count > 0 || Equipamentos.Count > 0 || Chamados.Count > 0;
    }
}
