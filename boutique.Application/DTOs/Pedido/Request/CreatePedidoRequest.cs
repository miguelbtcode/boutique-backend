using FluentValidation;

namespace boutique.Application.DTOs.Pedido.Request;

public class CreatePedidoRequest
{
    public ICollection<string>? Productos { get; set; }
    public string? IdVendedor { get; set; }
    public string? IdRepartidor { get; set; }
}

public class CreatePedidoRequestValidator : AbstractValidator<CreatePedidoRequest>
{
    public CreatePedidoRequestValidator()
    {
        RuleFor(p => p.Productos)
            .NotNull().WithMessage("La lista de productos no puede ser vacia")
            .NotEmpty().WithMessage("La lista de productos no puede ser vacia")
            .WithMessage("La lista de productos no puede estar vacia");
    }
}
