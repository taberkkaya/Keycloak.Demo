using System.Text.Json.Serialization;

namespace WebAPI.Dtos;

public sealed class GetAccessTokenReponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
    
}
