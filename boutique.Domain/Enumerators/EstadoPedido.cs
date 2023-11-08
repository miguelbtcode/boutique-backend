using boutique.Domain.Extensions;

namespace boutique.Domain.Enumerators;

public class EstadoPedido : Enumeration
{
    public static EstadoPedido PorAtender => new(1, "Por atender");
    public static EstadoPedido EnProceso => new(2, "En proceso");
    public static EstadoPedido EnDelivery => new(3, "En delivery");
    public static EstadoPedido Recibido => new(4, "Recibido");
    
    private EstadoPedido(int id, string description) : base(id, description)
    {
        
    }
}