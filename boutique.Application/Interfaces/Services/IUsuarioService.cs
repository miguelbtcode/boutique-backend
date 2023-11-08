using boutique.Application.DTOs.Usuario.Request;
using boutique.Application.DTOs.Usuario.Response;
using boutique.Application.Wrappers;

namespace boutique.Application.Interfaces.Services;

public interface IUsuarioService
{
    public Task<Response<LoginUsuarioResponse>> Register(RegisterUsuarioRequest request);

    public Task<Response<LoginUsuarioResponse>> Login(LoginUsuarioRequest request);
        
    public Task<Response<LoginUsuarioResponse>> GetByToken();
}