﻿using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using WebAPI.Dtos;
using WebAPI.Options;

namespace WebAPI.Services;

public sealed class KeycloakService(
    IOptions<KeycloakConfiguration> options)
{
    public async Task<string> GetAccessToken(CancellationToken cancellationToken = default)
    {
        HttpClient client = new();

        string endpoint = $"{options.Value.Hostname}/realms/{options.Value.Realm}/protocol/openid-connect/token";

        List<KeyValuePair<string, string>> data = new();

        KeyValuePair<string, string> grantType = new("grant_type", "client_credentials");
        KeyValuePair<string, string> clientId = new("client_id", options.Value.ClientId);
        KeyValuePair<string, string> clientSecret = new("client_secret", options.Value.ClientSecret);

        data.Add(grantType);
        data.Add(clientId);
        data.Add(clientSecret);

        var message = await client.PostAsync(endpoint, new FormUrlEncodedContent(data), cancellationToken);
        
        var response = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {

            if (message.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);
                throw new ArgumentException(errorResultForBadRequest!.ErrorDescription);
            }

            var errorResultForOther = JsonSerializer.Deserialize<ErrorResponseDto>(response);
            throw new ArgumentException(errorResultForOther!.ErrorMessage);
        }

        var result = JsonSerializer.Deserialize<GetAccessTokenReponseDto>(response);

        return result!.AccessToken;
    }
}
