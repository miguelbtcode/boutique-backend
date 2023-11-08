using FluentValidation;

namespace boutique.Application.DTOs.Usuario.Request;

public class RegisterUsuarioRequest
{
    public string? Nombre { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? Password { get; set; }
    public string? Telefono { get; set; }
    public string? Puesto { get; set; }
    public string? Rol { get; set; }
}

public class RegisterUsuarioValidator : AbstractValidator<RegisterUsuarioRequest>
{
    public RegisterUsuarioValidator()
    {
        RuleFor(p => p.Nombre)
            .NotNull().WithMessage("NombreCompleto no puede ser nulo")
            .Length(8, 50).WithMessage("NombreCompleto debe tener una longitud entre 15 a 50 caracteres");

        RuleFor(p => p.CorreoElectronico)
            .NotNull().WithMessage("Correo no puede ser nulo")
            .Length(10,50).WithMessage("Correo debe tener una longitud entre 10 a 50 caracteres")
            .EmailAddress().WithMessage("Correo debe ser un email válido");

        RuleFor(p => p.Password)
            .NotNull().WithMessage("Password no puede ser nulo")
            .Length(8, 32).WithMessage("Password debe tener una longitud entre 8 y 32 caracteres");
        
        RuleFor(p => p.Telefono)
            .NotNull().WithMessage("Telefono no puede ser nulo")
            .Length(9).WithMessage("El telefono debe tener una longitud de 9 caracteres");

        RuleFor(p => p.Puesto)
            .NotNull().WithMessage("El puesto no puede ser nulo");

        RuleFor(p => p.Rol)
            .NotNull().WithMessage("El Rol no puede ser nulo");
    }
}