using RHex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHex.Domain.Interfaces;

public interface IFerramentaRepository
{
    Task<IEnumerable<Ferramentas>> GetAll();
    Task<Ferramentas?> GetById(Guid id);
    Task Add(Ferramentas product);
    Task Update(Ferramentas product);
    Task Delete(Guid id);
}
