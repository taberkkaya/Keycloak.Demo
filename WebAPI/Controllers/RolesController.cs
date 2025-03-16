using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Dtos;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class RolesController(
    KeycloakService keycloakService,
    IOptions<KeycloakConfiguration> options
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllForRealm(CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/roles";

        var response = await keycloakService
            .GetAsync<List<RoleDto>>(endpoint, true, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllForClient(CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUUID}/roles";

        var response = await keycloakService
            .GetAsync<List<RoleDto>>(endpoint, true, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUUID}/roles/{name}";

        var response = await keycloakService
            .GetAsync<RoleDto>(endpoint, true, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteByName(string name, CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUUID}/roles/{name}";

        var response = await keycloakService
            .DeleteAsync<string>(endpoint, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
            response.Data = "Role delete was succesful";

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateForRealm(CreateRoleDto request, CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/roles";

        var response = await keycloakService
            .PostAsync<string>(endpoint, request, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
            response.Data = "Role create was succesful";

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateForClient(CreateRoleDto request, CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUUID}/roles";

        var response = await keycloakService
            .PostAsync<string>(endpoint, request, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
            response.Data = "Role create was succesful";

        return StatusCode(response.StatusCode, response);
    }
}
