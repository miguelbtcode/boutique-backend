using boutique.Application.DTOs.Usuario.Request;
using FluentValidation;

namespace boutique.Application.DTOs.Pedido.Request;

public class CambioEstadoPedidoRequest
{
    public string? NroPedido { get; set; }
    public string? EstadoPedido { get; set; }
    public string? CodigoUsuario { get; set; }
}

public class CambioEstadoPedidoRequestValidator : AbstractValidator<CambioEstadoPedidoRequest>
{
    public CambioEstadoPedidoRequestValidator()
    {

        RuleFor(p => p.NroPedido)
            .NotNull().WithMessage("El nro de pedido no debe ser vacio");

        RuleFor(p => p.EstadoPedido)
            .NotNull().WithMessage("El estado del pedido no debe ser vacio");
    
        RuleFor(p => p.CodigoUsuario)
            .NotNull().WithMessage("El codigo de usuario no debe ser vacio");
    }
}