using AutoMapper;
using boutique.Application.DTOs.Usuario.Request;
using boutique.Application.DTOs.Usuario.Response;
using boutique.Application.Exceptions;
using boutique.Application.Interfaces.Authentication;
using boutique.Application.Interfaces.Common;
using boutique.Application.Interfaces.Services;
using boutique.Application.Wrappers;
using Microsoft.EntityFrameworkCore;

using BC = BCrypt.Net.BCrypt;

namespace boutique.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUsuarioSession _usuarioSession;
    private readonly IUtilService _utilService;

    public UsuarioService(IApplicationDbContext context, IMapper mapper, IJwtGenerator jwtGenerator, IUsuarioSession usuarioSession, IUtilService utilService)
    {
        _context = context;
        _mapper = mapper;
        _jwtGenerator = jwtGenerator;
        _usuarioSession = usuarioSession;
        _utilService = utilService;
    }

    public async Task<Response<LoginUsuarioResponse>> Register(RegisterUsuarioRequest request)
    {
        var validator = new RegisterUsuarioValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
            throw new ValidationException(validatorResult.Errors);

        var existUserWithEmail = await _context.Usuarios!.AnyAsync(u => u.CorreoElectronico == request.CorreoElectronico);
        if (existUserWithEmail)
            throw new ApiException("Ya existe un usuario registrado con ese email");

        var usuario = _mapper.Map<Domain.Entities.Usuario>(request);
        usuario.Codigo = await _utilService.GenerateRandomString(6, true);
        usuario.Password = BC.HashPassword(request.Password);

        _context.Usuarios!.Add(usuario);
        await _context.SaveChangesAsync(default);

        var loginResponse = new LoginUsuarioResponse
        {
            Codigo = usuario.Codigo,
            NombreCompleto = usuario.Nombre,
            Correo = usuario.CorreoElectronico,
            Token = _jwtGenerator.GenerateToken(usuario)
        };
            
        return new Response<LoginUsuarioResponse>(loginResponse, "Te has registrado exitosamente");
    }

    public async Task<Response<LoginUsuarioResponse>> Login(LoginUsuarioRequest request)
    {
        var validator = new LoginUsuarioValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
            throw new ValidationException(validatorResult.Errors);

        var usuario = await _context.Usuarios!.FirstOrDefaultAsync(u => u.CorreoElectronico == request.Correo);
        if (usuario is null || !BC.Verify(request.Password, usuario.Password))
            throw new ApiException("Credenciales incorrectas");

        var loginResponse = new LoginUsuarioResponse
        {
            Codigo = usuario.Codigo,
            NombreCompleto = usuario.Nombre,
            Correo = usuario.CorreoElectronico,
            Token = _jwtGenerator.GenerateToken(usuario)
        };
            
        return new Response<LoginUsuarioResponse>(loginResponse);
    }

    public async Task<Response<LoginUsuarioResponse>> GetByToken()
    {
        var usuario = await _context.Usuarios!.FirstOrDefaultAsync(u => u.CorreoElectronico == _usuarioSession.GetUsuarioSession());
        if (usuario is null)
            throw new ApiException("Usuario no existe");
                
        var loginResponse = new LoginUsuarioResponse
        {
            NombreCompleto = usuario.Nombre,
            Correo = usuario.CorreoElectronico,
            Token = _jwtGenerator.GenerateToken(usuario)
        };
            
        return new Response<LoginUsuarioResponse>(loginResponse);
    }
}