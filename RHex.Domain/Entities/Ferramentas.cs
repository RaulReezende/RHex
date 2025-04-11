using Microsoft.EntityFrameworkCore;
using RHex.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHex.Domain.Entities;

public class Ferramentas
{
    public Guid Id { get; set; }

    [MinLength(1, ErrorMessage = "Endereço deve ter no mínimo 1 caractere")]
    [MaxLength(50, ErrorMessage = "Endereço deve ter no máximo 50 caracteres")]
    public string Endereco { get; set; } = string.Empty;

    [MinLength(3, ErrorMessage = "A descrição deve ter pelo menos 3 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, 99.99, ErrorMessage = "O diâmetro deve ser maior que 0 e menor que 100")]
    [Precision(4, 2)] 
    [Column(TypeName = "decimal(5,2)")]
    public decimal Diametro { get; set; }

    [Range(0.01, 99.99, ErrorMessage = "A altura deve ser maior que 0 e menor que 100")]
    [Precision(4, 2)]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Altura { get; set; }

    public TipoFerramenta TipoFerramenta { get; set; }
}
