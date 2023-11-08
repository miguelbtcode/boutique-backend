using System.Text.Json;

namespace boutique.Application.Wrappers;

public class Response<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    
    public List<string> Errors { get; set; }
    public T Data { get; set; }

    public Response() { }

    public Response(T data = default, string message = null)
    {
        Data = data;
        Message = message;
        Succeeded = true;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true});
    }
}