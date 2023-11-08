using boutique.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace boutique.Application.Interfaces.Common;

public interface IApplicationDbContext
{
    public DbSet<Pedido>? Pedidos { get; set; }
    public DbSet<Producto>? Productos { get; set; }
    public DbSet<Usuario>? Usuarios { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}