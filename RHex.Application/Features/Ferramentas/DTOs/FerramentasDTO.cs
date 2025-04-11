using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace RHex.Application.Features.Ferramenta.DTOs;

public record FerramentaDto(
    Guid Id,
    string Endereco,
    string Descricao,
    decimal Diametro,
    decimal Altura,
    string TipoFerramenta
);

public record CreateFerramentaDto(
    string Endereco,
    string Descricao,
    decimal Diametro,
    decimal Altura,
    string TipoFerramenta
);

public class CreateFerramentaDtoValidator : AbstractValidator<CreateFerramentaDto>
{
    public CreateFerramentaDtoValidator()
    {
        RuleFor(x => x.Endereco)
            .NotEmpty().WithMessage("Endereço é obrigatório")
            .Length(1, 50).WithMessage("Endereço deve ter entre 1 e 50 caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .MinimumLength(3).WithMessage("Descrição deve ter no mínimo 3 caracteres");

        RuleFor(x => x.Diametro)
            .GreaterThan(0).WithMessage("Diâmetro deve ser maior que 0")
            .LessThan(100).WithMessage("Diâmetro deve ser menor que 100");

        RuleFor(x => x.Altura)
            .GreaterThan(0).WithMessage("Altura deve ser maior que 0")
            .LessThan(100).WithMessage("Altura deve ser menor que 100");

        RuleFor(x => x.TipoFerramenta)
            .Matches("VBit|TopoRaso").WithMessage("Tipo inválido. Use 'VBit' ou 'TopoRaso'");
    }
}

public record UpdateFerramentaDto(
    string Endereco,
    string Descricao,
    decimal Diametro,
    decimal Altura,
    string TipoFerramenta
);


public class UpdateFerramentaDtoValidator : AbstractValidator<UpdateFerramentaDto>
{
    public UpdateFerramentaDtoValidator()
    {
        RuleFor(x => x.Endereco)
            .NotEmpty().WithMessage("Endereço é obrigatório")
            .Length(1, 50).WithMessage("Endereço deve ter entre 1 e 50 caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .MinimumLength(3).WithMessage("Descrição deve ter no mínimo 3 caracteres");

        RuleFor(x => x.Diametro)
            .GreaterThan(0).WithMessage("Diâmetro deve ser maior que 0")
            .LessThan(100).WithMessage("Diâmetro deve ser menor que 100");

        RuleFor(x => x.Altura)
            .GreaterThan(0).WithMessage("Altura deve ser maior que 0")
            .LessThan(100).WithMessage("Altura deve ser menor que 100");

        RuleFor(x => x.TipoFerramenta)
            .Matches("VBit|TopoRaso").WithMessage("Tipo inválido. Use 'VBit' ou 'TopoRaso'");
    }
}