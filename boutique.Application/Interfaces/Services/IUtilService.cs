namespace boutique.Application.Interfaces.Services;

public interface IUtilService
{
    Task<string> GenerateRandomString(int size, bool lowerCase = false);
}