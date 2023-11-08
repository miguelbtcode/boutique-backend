namespace boutique.Application.DTOs.Pedido.Response;

public class CreatePedidoResponse
{
    public string? NroPedido { get; set; }
    public ICollection<Domain.Entities.Producto>? Productos { get; set; }
    public DateTime FechaPedido { get; set; }
    public DateTime FechaRecepcion { get; set; }
    public DateTime FechaDespacho { get; set; }
    public DateTime FechaEntrega { get; set; }
    public Domain.Entities.Usuario? Vendedor { get; set; }
    public Domain.Entities.Usuario? Repartidor { get; set; }
    public string? EstadoPedido { get; set; }
}