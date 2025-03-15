﻿using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Dtos;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class AuthController(
    IOptions<KeycloakConfiguration> options,
    KeycloakService keycloakService
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto request, CancellationToken cancellationToken = default)
    {
        string endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users";

        object data = new
        {
            username = request.UserName,
            firstName = request.FirstName,
            lastName = request.LastName,
            email = request.Email,
            enabled = true,
            emailVerified = true,
            credentials = new List<object>
            {
                new
                {
                    type = "password",
                    temporary = false,
                    value = request.Password
                }
            }
        };

        var stringData = JsonSerializer.Serialize(data);
        var content = new StringContent(stringData, Encoding.UTF8, "application/json");

        HttpClient httpClient = new();

        string token = await keycloakService.GetAccessToken();

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var message = await httpClient.PostAsync(endpoint, content, cancellationToken);

        if (!message.IsSuccessStatusCode)
        {
            var response = await message.Content.ReadAsStringAsync();

            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);
                return BadRequest(new { ErrorMessage = errorResultForBadRequest!.ErrorDescription });
            }

            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);
           return BadRequest( new { ErrorMessage =  errorResultForOther!.ErrorMessage });
        }

        return Ok(new { Message = "User create was succesful" });
    }
}
