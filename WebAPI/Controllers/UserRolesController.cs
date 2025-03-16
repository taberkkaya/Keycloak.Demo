using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Dtos;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
//[Authorize]
public class UserRolesController(
    KeycloakService keycloakService,
    IOptions<KeycloakConfiguration> options
    ) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllByUserId(Guid id, CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users/{id}/role-mappings/clients/{options.Value.ClientUUID}";

        var response = await keycloakService
            .GetAsync<List<RoleDto>>(endpoint, true, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> AssignmentRolesByUserId(Guid userId, List<RoleDto> request, CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users/{userId}/role-mappings/clients/{options.Value.ClientUUID}";

        var response = await keycloakService.PostAsync<string>(endpoint, request, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
            response.Data = "Role assignment was succesful";

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete]
    public async Task<IActionResult> UnassignmentRolesByUserId(Guid userId, List<RoleDto> request, CancellationToken cancellationToken)
    {
        var endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users/{userId}/role-mappings/clients/{options.Value.ClientUUID}";

        var response = await keycloakService.DeleteAsync<string>(endpoint, request, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
            response.Data = "Role assignment removal successful";

        return StatusCode(response.StatusCode, response);
    }
}
