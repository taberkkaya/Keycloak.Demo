﻿using System.Text.Json.Serialization;

namespace WebAPI.Dtos;

public sealed record RoleDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}