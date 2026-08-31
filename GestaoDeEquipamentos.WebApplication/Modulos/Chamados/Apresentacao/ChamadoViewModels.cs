using System.ComponentModel.DataAnnotations;
using GestaoDeEquipamentos.WebApplication.Modulos.Equipamentos.Dominio;
namespace GestaoDeEquipamentos.WebApplication.Modulos.Chamados.Apresentacao;

public record ListarChamadoViewModel
(
    int Id,
    string Titulo,
    string Descricao,
    Equipamento Equipamento,
    DateTime DataAbertura
);

public record EquipamentoChamadoViewModel(int Id, string Titulo);

public record CadastrarChamadoViewModel
(
    [Required(ErrorMessage = "O campo \"Titulo\" é obrigatório.")]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "O campo \"Titulo\" deve conter entre 6 e 100 caracteres.")]
    string? Titulo,

    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "O campo \"Descrição\" deve conter entre 6 e 100 caracteres.")]
    string Descricao,

    [Required(ErrorMessage = "O campo \"Data de fabricação\" é obrigatório.")]
    [DataType(DataType.Date)]
    DateTime? DataAbertura,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Equipamento\" é obrigatório.")]
    int EquipamentoId,

    List<EquipamentoChamadoViewModel>? EquipamentosDisponiveis
);

public record EditarChamadoViewModel
(
    int Id,

    [Required(ErrorMessage = "O campo \"Titulo\" é obrigatório.")]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "O campo \"Titulo\" deve conter entre 6 e 100 caracteres.")]
    string? Titulo,

    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "O campo \"Preço de aquisição\" deve ser maior que zero.")]
    string Descricao,

    [Required(ErrorMessage = "O campo \"Data de fabricação\" é obrigatório.")]
    [DataType(DataType.Date)]
    DateTime? DataAbertura,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Equipamento\" é obrigatório.")]
    int EquipamentoId,

    List<EquipamentoChamadoViewModel>? EquipamentosDisponiveis
);

public record ExcluirChamadoViewModel(
    int Id,
    string Titulo
);