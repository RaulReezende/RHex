using FluentAssertions;
using Moq;
using RHex.Application.Features.Ferramenta;
using RHex.Application.Features.Ferramenta.DTOs;
using RHex.Domain.Entities;
using RHex.Domain.Enums;
using RHex.Domain.Interfaces;
using System;


namespace RHex.Tests.Services;

public class FerramentaServiceTest
{
    private readonly Mock<IFerramentaRepository> _mockRepo;
    private readonly FerramentaService _service;

    public FerramentaServiceTest()
    {
        _mockRepo = new Mock<IFerramentaRepository>();
        _service = new FerramentaService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllFerramentas_DeveRetornarListaVazia_QuandoNaoHouverFerramentas()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAll()).ReturnsAsync(new List<Ferramentas>());

        // Act
        var result = await _service.GetAllFerramentas();

        // Assert
        result.Should().BeEmpty();
        _mockRepo.Verify(r => r.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetFerramentaId_DeveRetornarNull_QuandoFerramentaNaoExiste()
    {
        // Arrange
        var ferramentaId = Guid.NewGuid();
        _mockRepo.Setup(r => r.GetById(ferramentaId)).ReturnsAsync((Ferramentas?)null);

        // Act
        var result = await _service.GetFerramentaById(ferramentaId);

        // Assert
        result.Should().BeNull();
        _mockRepo.Verify(r => r.GetById(ferramentaId), Times.Once);
    }

    [Fact]
    public async Task CreateFerramenta_DeveMapearDtoParaEntidadeCorretamente()
    {
        // Arrange
        var dto = new CreateFerramentaDto("Endereco 1", "Ripa de madeira", 1m, 1m, "VBit");
        _mockRepo.Setup(r => r.Add(It.IsAny<Ferramentas>())).Callback<Ferramentas>(p =>
        {
            p.Endereco.Should().Be(dto.Endereco);
            p.Descricao.Should().Be(dto.Descricao);
            p.Diametro.Should().Be(dto.Diametro);
            p.Altura.Should().Be(dto.Altura);
            p.TipoFerramenta.Should().Be(Enum.Parse<TipoFerramenta>(dto.TipoFerramenta));
        });

        // Act
        await _service.CreateFerramenta(dto);

        // Assert
        _mockRepo.Verify(r => r.Add(It.IsAny<Ferramentas>()), Times.Once);
    }

    [Fact]
    public async Task DeleteFerramenta_DeveRetornarFalse_QuandoFerramentaNaoExiste()
    {
        // Arrange
        var ferramentaId = Guid.NewGuid();
        _mockRepo.Setup(r => r.GetById(ferramentaId)).ReturnsAsync((Ferramentas?)null);

        // Act
        var result = await _service.DeleteFerramenta(ferramentaId);

        // Assert
        result.Should().BeFalse();
        _mockRepo.Verify(r => r.Delete(It.IsAny<Guid>()), Times.Never);
    }
}
