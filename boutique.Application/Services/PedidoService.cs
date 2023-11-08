using AutoMapper;
using boutique.Application.DTOs.Pedido.Request;
using boutique.Application.DTOs.Pedido.Response;
using boutique.Application.Exceptions;
using boutique.Application.Interfaces.Common;
using boutique.Application.Interfaces.Services;
using boutique.Application.Wrappers;
using boutique.Domain.Entities;
using boutique.Domain.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace boutique.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PedidoService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<CreatePedidoResponse>> CreatePedido(CreatePedidoRequest request)
    {
        var validator = new CreatePedidoRequestValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
            throw new ValidationException(validatorResult.Errors);

        ICollection<Producto> productList = new List<Producto>();
        
        // convertir todos los skus a productos
        foreach (var sku in request.Productos!)
        {
            var producto = await _context.Productos!.FirstOrDefaultAsync(x => x.Sku != null && x.Sku.Equals(sku))!;
            productList.Add(producto!);
        }

        var pedido = _mapper.Map<Pedido>(request);
        pedido.Productos = productList;

        _context.Pedidos!.Add(pedido);
        await _context.SaveChangesAsync(default);
        
        var response = _mapper.Map<CreatePedidoResponse>(pedido);
        
        return new Response<CreatePedidoResponse>(response,"Pedido registrado correctamente");
    }

    public async Task<Response<CambioEstadoPedidoResponse>> CambioEstadoPedido(CambioEstadoPedidoRequest request)
    {
        var validator = new CambioEstadoPedidoRequestValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
            throw new ValidationException(validatorResult.Errors);
        
        // validate exist pedido
        var pedido = await _context.Pedidos!.FirstOrDefaultAsync(x => x.NroPedido == int.Parse(request.NroPedido!))!;
        
        if (pedido is null)
            throw new KeyNotFoundException("Pedido no existe en la base de datos");
        
        // Logic
        var usuario = await _context.Usuarios!.FirstOrDefaultAsync(x => x.Codigo!.Equals(request.CodigoUsuario!));
        
        if (usuario is null)
            throw new KeyNotFoundException("Usuario no existe en la base de datos");
        
        if (pedido.FechaPedido == DateTime.MinValue)
        {
            throw new ApiException("El pedido no cuenta con una fecha de registro");
        }

        switch (request.EstadoPedido)
        {
            case "Por atender":
                if (!usuario.Rol!.Equals("Vendedor"))
                    throw new ApiException($"Para proceder a solicitar la atencion del pedido, el rol del usuario debe ser vendedor, rol de usuario actual: {usuario.Rol}");

                if (pedido.FechaPedido.HasValue)
                    throw new ApiException($"El pedido ya se registro por atender en la fecha: {pedido.FechaPedido}");
                
                pedido.FechaPedido = DateTime.Now;
                pedido.EstadoPedido = request.EstadoPedido;
                break;
            
            case "En proceso":
                if (!usuario.Rol!.Equals("Encargado"))
                    throw new ApiException($"Para proceder a procesar el pedido, el rol del usuario debe ser Encargado, rol de usuario actual: {usuario.Rol}");
                
                if (pedido.FechaRecepcion.HasValue)
                    throw new ApiException($"El pedido ya esta en proceso desde la fecha: {pedido.FechaRecepcion}");
                
                pedido.FechaRecepcion = DateTime.Now;
                pedido.EstadoPedido = request.EstadoPedido;
                break;
            
            case "En delivery":
                if (!usuario.Rol!.Equals("Delivery"))
                    throw new ApiException($"Para proceder a despachar el pedido, el rol del usuario debe ser Delivery, rol de usuario actual: {usuario.Rol}");
                
                if (pedido.FechaDespacho.HasValue)
                    throw new ApiException($"El pedido ya se encuentra en despacho desde la fecha: {pedido.FechaDespacho}");
                
                pedido.FechaDespacho = DateTime.Now;
                pedido.EstadoPedido = request.EstadoPedido;
                break;
            
            case "Recibido":
                if (!usuario.Rol!.Equals("Repartidor"))
                    throw new ApiException($"Para proceder a recibir el pedido, el rol del usuario debe ser Repartidor, rol de usuario actual: {usuario.Rol}");
                
                if (pedido.FechaEntrega.HasValue)
                    throw new ApiException($"El pedido ya se recibido desde la fecha: {pedido.FechaEntrega}");
                
                pedido.FechaEntrega = DateTime.Now;
                pedido.EstadoPedido = request.EstadoPedido;
                break;
            
            default:
                throw new ApiException($"El pedido ya se recibido desde la fecha: {pedido.FechaEntrega}");
                break;
        }

        _context.Pedidos!.Update(pedido);
        await _context.SaveChangesAsync(default);
        
        var response = _mapper.Map<CambioEstadoPedidoResponse>(pedido);
        
        return new Response<CambioEstadoPedidoResponse>(response,"Estado de pedido cambiado correctamente");
    }
    
    public async Task<Response<Pedido>> GetPedido(int id)
    {
        // validate exist pedido
        var pedido = await _context.Pedidos!.FirstOrDefaultAsync(x => x.NroPedido == id);
        
        if (pedido is null)
            throw new KeyNotFoundException("Pedido no existe en la base de datos");
        
        return new Response<Pedido>(pedido);
    }
}