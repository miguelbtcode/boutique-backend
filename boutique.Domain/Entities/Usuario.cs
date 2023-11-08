using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using boutique.Domain.Enumerators;

namespace boutique.Domain.Entities;

public class Usuario
{
    [Key]
    [Column(TypeName = "char(6)")]
    public string? Codigo { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string? Nombre { get; set; }
    
    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string? CorreoElectronico { get; set; }
    
    [Required]
    [Column(TypeName = "nvarchar(32)")]
    public string? Password { get; set; }
    
    [Required]
    [Column(TypeName = "char(9)")]
    public string? Telefono { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(30)")]
    public string? Puesto { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(30)")]
    public string? Rol { get; set; }
}