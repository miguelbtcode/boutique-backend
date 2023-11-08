using boutique.Application.DTOs.Pedido.Request;
using boutique.Application.DTOs.Pedido.Response;
using boutique.Application.Wrappers;
using boutique.Domain.Entities;

namespace boutique.Application.Interfaces.Services;

public interface IPedidoService
{
    Task<Response<CreatePedidoResponse>> CreatePedido(CreatePedidoRequest request);
    Task<Response<CambioEstadoPedidoResponse>> CambioEstadoPedido(CambioEstadoPedidoRequest request);
    Task<Response<Pedido>> GetPedido(int idPedido);
}