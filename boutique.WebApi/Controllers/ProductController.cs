using boutique.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace boutique.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductController(IProductoService productoService)
    {
        _productoService = productoService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _productoService.GetAll();
        return Ok(response);
    }
}