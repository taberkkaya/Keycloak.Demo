using System.Text.Json.Serialization;

namespace WebAPI.Dtos;

public sealed record CreateRoleDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
}
