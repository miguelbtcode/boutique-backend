using System.Globalization;
using AutoMapper;
using boutique.Application.DTOs.Pedido.Request;
using boutique.Application.DTOs.Pedido.Response;
using boutique.Application.DTOs.Producto.Response;
using boutique.Application.DTOs.Usuario.Request;
using boutique.Domain.Entities;

namespace boutique.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Pedido, CambioEstadoPedidoResponse>()
            .ForMember(p => p.NroPedido, opt => opt.MapFrom(p => p.NroPedido.ToString()))
            .ForMember(p => p.EstadoPedido, opt => opt.MapFrom(p => p.EstadoPedido!.ToString()));

        CreateMap<Producto, GetProductoResponse>()
            .ForMember(p => p.Precio, opt => opt.MapFrom(p => p.Precio!.ToString(CultureInfo.CurrentCulture)))
            .ForMember(p => p.UnidadMedida,
                opt => opt.MapFrom(p => p.UnidadMedida.ToString(CultureInfo.CurrentCulture)))
            .ForMember(p => p.Pedido, opt => opt.Ignore());
        
        CreateMap<CreatePedidoRequest, Pedido>()
            .ForMember(p => p.IdVendedor, opt => opt.MapFrom(p => p.IdVendedor))
            .ForMember(p => p.IdRepartidor, opt => opt.MapFrom(p => p.IdRepartidor!))
            .ForMember(p => p.Productos, opt => opt.Ignore());
        
        CreateMap<Pedido, CreatePedidoResponse>()
            .ForMember(p => p.NroPedido, opt => opt.MapFrom(p => p.NroPedido.ToString()))
            .ForMember(p => p.FechaPedido, opt => opt.MapFrom(p => p.FechaPedido.ToString()))
            .ForMember(p => p.Vendedor,
                opt => opt.Ignore())
            .ForMember(p => p.Repartidor,
                opt => opt.Ignore());

        CreateMap<RegisterUsuarioRequest, Usuario>()
            .ForMember(p => p.Nombre, opt => opt.MapFrom(p => p.Nombre))
            .ForMember(p => p.CorreoElectronico, opt => opt.MapFrom(p => p.CorreoElectronico))
            .ForMember(p => p.Telefono, opt => opt.MapFrom(p => p.Telefono))
            .ForMember(p => p.Rol, opt => opt.MapFrom(p => p.Rol))
            .ForMember(p => p.Puesto, opt => opt.MapFrom(p => p.Puesto));
    }
}