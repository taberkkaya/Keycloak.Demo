using System.Text.Json.Serialization;

namespace WebAPI.Dtos;

public sealed class ErrorResponseDto
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = default!;
    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; } = default!;
}