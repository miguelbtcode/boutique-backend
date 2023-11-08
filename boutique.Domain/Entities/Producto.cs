using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boutique.Domain.Entities;

public class Producto
{
    [Key]
    [Column(TypeName = "char(8)")]
    public string? Sku { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string? Nombre { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string? Tipo { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string? Etiqueta { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(9,6)")]
    public double Precio { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(6,3)")]
    public double UnidadMedida { get; set; }
    
    [ForeignKey("Pedido")]
    public int IdPedido { get; set; }
    public Pedido? Pedido { get; set; }
}