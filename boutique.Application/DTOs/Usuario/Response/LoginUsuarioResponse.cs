namespace boutique.Application.DTOs.Usuario.Response;

public class LoginUsuarioResponse
{
    public string? Codigo { get; set; }
    public string? NombreCompleto { get; set; }
    public string? Correo { get; set; }
    public string? Token { get; set; }
}