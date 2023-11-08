using boutique.Domain.Extensions;

namespace boutique.Domain.Enumerators;

public class Rol : Enumeration
{
    public static Rol Encargado => new(1, "Encargado");
    public static Rol Vendedor => new(2, "Vendedor");
    public static Rol Delivery => new(3, "Delivery");
    public static Rol Repartidor => new(4, "Repartidor");

    private Rol(int id, string description) : base(id, description)
    {
        
    }
}