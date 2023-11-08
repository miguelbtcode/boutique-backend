using AutoMapper;
using boutique.Application.DTOs.Producto.Request;
using boutique.Application.DTOs.Producto.Response;
using boutique.Application.Interfaces.Common;
using boutique.Application.Interfaces.Services;
using boutique.Application.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace boutique.Application.Services;

public class ProductoService : IProductoService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductoService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<GetProductoResponse>>> GetAll()
    {
        var products = await _context.Productos!.ToListAsync();
        var productsResponse = _mapper.Map<IEnumerable<GetProductoResponse>>(products);
        return new Response<IEnumerable<GetProductoResponse>>(productsResponse);
    }

    public Task<Response<IEnumerable<GetProductoResponse>>> GetProduct(GetProductoRequest request)
    {
        throw new NotImplementedException();
    }
}