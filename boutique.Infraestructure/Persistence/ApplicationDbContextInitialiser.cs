using boutique.Domain.Entities;
using boutique.Domain.Enumerators;

namespace boutique.Infraestructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception e)
        {
            throw new Exception("An error ocurred while seeding database");
        }
    }
    
    private async Task TrySeedAsync()
    {
        var usuarios = new List<Usuario>()
        {
            new Usuario
            {
                Codigo = "AAA111", Nombre = "Miguel Barreto", CorreoElectronico = "mabt2206@gmail.com",
                Telefono = "928799438", Puesto = "Developer", Rol = Rol.Vendedor.ToString(), Password = "111111112222"
            },
            new Usuario
            {
                Codigo = "BBB222", Nombre = "Eduardo Sanchez", CorreoElectronico = "eduardo@gmail.com",
                Telefono = "984656321", Puesto = "Product Owner", Rol = Rol.Repartidor.ToString(), Password = "33334545454"
            },
            new Usuario
            {
                Codigo = "CCC333", Nombre = "Jimena Campos", CorreoElectronico = "jimena@gmail.com",
                Telefono = "967523412", Puesto = "Chapter Leader", Rol = Rol.Encargado.ToString(), Password = "565473sdfdsf"
            },
            new Usuario
            {
                Codigo = "DDD444", Nombre = "Rachel Espinoza", CorreoElectronico = "rachel@gmail.com",
                Telefono = "982674512", Puesto = "Tech Lead", Rol = Rol.Delivery.ToString(), Password = "676523fdxzfdsfs"
            },
            new Usuario
            {
                Codigo = "EEE555", Nombre = "Melisa Solis", CorreoElectronico = "melisa@gmail.com",
                Telefono = "985675126", Puesto = "Quality Engineer", Rol = Rol.Vendedor.ToString(), Password = "23423r45dsfvdszf"
            },
        };

        var products = new List<Producto>()
        {
            new Producto()
            {
                Sku = "AAABBB11",
                Nombre = "Camiseta",
                Tipo = "Prenda",
                Etiqueta = "Camisetas,hombres",
                Precio = 20.5,
                UnidadMedida = 1
            },
            new Producto()
            {
                Sku = "DDDCCC22",
                Nombre = "Zapatillas AK2",
                Tipo = "Calzado",
                Etiqueta = "Calzado,hombres",
                Precio = 20.5,
                UnidadMedida = 2
            }
        };
        
        
        _context.Usuarios!.AddRange(usuarios);
        _context.Productos!.AddRange(products);
        
        await _context.SaveChangesAsync();
    }
}