using RHex.Domain.Entities;
using RHex.Domain.Interfaces;
using RHex.Application.Features.Ferramenta.DTOs;
using RHex.Domain.Enums;

namespace RHex.Application.Features.Ferramenta;

public class FerramentaService(IFerramentaRepository FerramentaRepository)
{
    public async Task<IEnumerable<FerramentaDto>> GetAllFerramentas()
    {
        var Ferramentas = await FerramentaRepository.GetAll();
        return Ferramentas.Select(f => new FerramentaDto(f.Id, f.Endereco, f.Descricao, f.Diametro, f.Altura, Path(f.TipoFerramenta)));
    }

    public async Task<FerramentaDto?> GetFerramentaById(Guid id)
    {
        var Ferramenta = await FerramentaRepository.GetById(id);
        return Ferramenta is null
            ? null
            : new FerramentaDto(Ferramenta.Id, Ferramenta.Endereco, Ferramenta.Descricao, Ferramenta.Diametro, Ferramenta.Altura, Path(Ferramenta.TipoFerramenta));
    }

    public async Task<FerramentaDto> CreateFerramenta(CreateFerramentaDto dto)
    {
        var tipo = Enum.Parse<TipoFerramenta>(dto.TipoFerramenta);

        var Ferramenta = new Ferramentas
        {
            Endereco = dto.Endereco,
            Descricao = dto.Descricao,
            Diametro = dto.Diametro,
            Altura = dto.Altura,
            TipoFerramenta = tipo
        };
        await FerramentaRepository.Add(Ferramenta);
        return new FerramentaDto(Ferramenta.Id, Ferramenta.Endereco, Ferramenta.Descricao, Ferramenta.Diametro, Ferramenta.Altura, Path(Ferramenta.TipoFerramenta));
    }

    public async Task<FerramentaDto?> UpdateFerramenta(Guid id, UpdateFerramentaDto dto)
    {
        var Ferramenta = await FerramentaRepository.GetById(id);
        if (Ferramenta is null) return null;

        var tipo = Enum.Parse<TipoFerramenta>(dto.TipoFerramenta);

        Ferramenta.Endereco = dto.Endereco;
        Ferramenta.Descricao = dto.Descricao;
        Ferramenta.Diametro = dto.Diametro;
        Ferramenta.Altura = dto.Altura;
        Ferramenta.TipoFerramenta = tipo;

        await FerramentaRepository.Update(Ferramenta);
        return new FerramentaDto(Ferramenta.Id, Ferramenta.Endereco, Ferramenta.Descricao, Ferramenta.Diametro, Ferramenta.Altura, Path(Ferramenta.TipoFerramenta));
    }

    public async Task<bool> DeleteFerramenta(Guid id)
    {
        var Ferramenta = await FerramentaRepository.GetById(id);
        if (Ferramenta is null) return false;

        await FerramentaRepository.Delete(id);
        return true;
    }

    private static string Path(TipoFerramenta tpFerramenta)
    {
        if(tpFerramenta == TipoFerramenta.VBit)
            return "subir nos cantos";
        else if (tpFerramenta == TipoFerramenta.TopoRaso)
            return "caminho tradicional";
        else
            throw new ArgumentException("Tipo de ferramenta inválido");
    }
}