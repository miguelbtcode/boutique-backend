using boutique.Application.DTOs.Producto.Request;
using boutique.Application.DTOs.Producto.Response;
using boutique.Application.Wrappers;

namespace boutique.Application.Interfaces.Services;

public interface IProductoService
{
    Task<Response<IEnumerable<GetProductoResponse>>> GetAll();
    Task<Response<IEnumerable<GetProductoResponse>>> GetProduct(GetProductoRequest request);
}