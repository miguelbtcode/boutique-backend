namespace boutique.Application.DTOs.Pedido;

public class GetPedidoResponse
{
    public string? NroPedido { get; set; }
    public ICollection<Domain.Entities.Producto>? Productos { get; set; }
    public string? FechaPedido { get; set; }
    public string? FechaRecepcion { get; set; }
    public string? FechaDespacho { get; set; }
    public string? FechaEntrega { get; set; }
    public Domain.Entities.Usuario? Vendedor { get; set; }
    public Domain.Entities.Usuario? Repartidor { get; set; }
}