using RHex.Domain.Interfaces;
using RHex.Domain.Entities;
using RHex.Infrastructure.Data;
namespace RHex.Infrastructure.Repositories;

public class FerramentaRepository(AppDbContext context) : IFerramentaRepository
{
    public async Task<IEnumerable<Ferramentas>> GetAll() =>
        context.Ferramentas.ToList();

    public async Task<Ferramentas?> GetById(Guid id) =>
        await context.Ferramentas.FindAsync(id);

    public async Task Add(Ferramentas product)
    {
        await context.Ferramentas.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task Update(Ferramentas product)
    {
        context.Ferramentas.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var product = await GetById(id);
        if (product is not null)
        {
            context.Ferramentas.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
