using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boutique.Domain.Entities;

public class Pedido
{
    [Key]
    public int NroPedido { get; set; }

    public ICollection<Producto>? Productos { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime? FechaPedido { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime? FechaRecepcion { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime? FechaDespacho { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime? FechaEntrega { get; set; }
    
    [Required]
    [ForeignKey("Usuario")]
    public string IdVendedor { get; set; }
    public Usuario? Vendedor { get; set; }
    
    [Required]
    [ForeignKey("Usuario")]
    public string IdRepartidor { get; set; }
    public Usuario? Repartidor { get; set; }
    
    [Column(TypeName = "varchar(30)")]
    public string? EstadoPedido { get; set; }
}