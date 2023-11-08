using AutoMapper;

namespace boutique.Application.DTOs.Producto.Response;

public class GetProductoResponse
{
    public string? Sku { get; set; }
    
    public string? Nombre { get; set; }

    public string? Tipo { get; set; }
    
    public string? Etiqueta { get; set; }
    
    public string? Precio { get; set; }

    public string? UnidadMedida { get; set; }
    
    public Domain.Entities.Pedido? Pedido { get; set; }
}