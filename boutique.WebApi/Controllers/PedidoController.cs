using boutique.Application.DTOs.Pedido.Request;
using boutique.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace boutique.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePedido([FromBody] CreatePedidoRequest request)
    {
        var response = await _pedidoService.CreatePedido(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> CambioEstadoPedido([FromBody] CambioEstadoPedidoRequest request)
    {
        var response = await _pedidoService.CambioEstadoPedido(request);
        return Ok(response);
    }
    
    [HttpGet("{id:int}", Name = "GetPedido")]
    public async Task<IActionResult> GetPedido(int id)
    {
        var response = await _pedidoService.GetPedido(id);
        return Ok(response);
    }
}