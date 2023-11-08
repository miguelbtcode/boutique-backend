using boutique.Application.Interfaces.Common;
using boutique.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace boutique.Infraestructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Pedido>? Pedidos { get; set; }
    public DbSet<Producto>? Productos { get; set; }
    public DbSet<Usuario>? Usuarios { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}