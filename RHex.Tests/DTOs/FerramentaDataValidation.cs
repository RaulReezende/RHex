using FluentAssertions;
using FluentValidation;
using RHex.Application.Features.Ferramenta.DTOs;


namespace RHex.Tests.DTOs;

public class FerramentaDataValidation
{
    private readonly IValidator<CreateFerramentaDto> _validator;

    public FerramentaDataValidation()
    {
        _validator = new CreateFerramentaDtoValidator();
    }

    [Theory]
    [InlineData("", "Descricao valida", 10.99, 10.99, "VBit", false)] // Endereço vazio
    [InlineData("Endereço Válido", "Ab", 10.99, 10.99, "VBit", false)] // Descrição com 2 caracteres
    [InlineData("Endereço Válido", "Descricao valida", 0, 10.99, "VBit", false)] // Diametro zero
    [InlineData("Endereço Válido", "Descricao valida", -5.99, 10.99, "VBit", false)] // Diametro negativa
    [InlineData("Endereço Válido", "Descricao valida", 10.99, 0, "VBit", false)] // Altura zero
    [InlineData("Endereço Válido", "Descricao valida", 10.99, -5.99, "VBit", false)] // Altura negativa
    [InlineData("Endereço Válido", "", 5.99, 10.99, "VBit", false)] // Descrição vazia
    [InlineData("Endereço Válido", "Descricao valida", 5.99, 10.99, "VBit", true)] // Dados válidos

    public async Task CreateProductDto_ValidaCamposCorretamente(
       string endereco,
       string descricao,
       decimal diametro,
       decimal altura, 
       string tipoFerramenta,
       bool isValidExpected
    )
    {
        // Arrange
        var dto = new CreateFerramentaDto(endereco, descricao, diametro, altura, tipoFerramenta);

        var result = await _validator.ValidateAsync(dto);

        // Assert
        result.IsValid.Should().Be(isValidExpected);
        if (!isValidExpected)
        {
            result.Errors.Should().NotBeEmpty();
        }
    }
}
