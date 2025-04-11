using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RHex.Domain.Entities;

namespace RHex.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ferramentas> Ferramentas => Set<Ferramentas>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ferramentas>()
            .Property(f => f.TipoFerramenta)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}