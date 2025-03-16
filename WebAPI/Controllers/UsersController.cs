﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Dtos;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public sealed class UsersController(
        KeycloakService keycloakService,
        IOptions<KeycloakConfiguration> options) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            string endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users";

            var response = await keycloakService
                .GetAsync<List<UserDto>>(endpoint, true, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email, CancellationToken cancellationToken)
        {
            string endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users?email={email}";

            var response = await keycloakService
                .GetAsync<List<UserDto>>(endpoint, true, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserName(string userName, CancellationToken cancellationToken)
        {
            string endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users?username={userName}";

            var response = await keycloakService
                .GetAsync<List<UserDto>>(endpoint, true, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            string endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users/{id}";

            var response = await keycloakService
                .GetAsync<UserDto>(endpoint, true, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto request, CancellationToken cancellationToken = default)
        {
            string endpoint = $"{options.Value.Hostname}/admin/realms/{options.Value.Realm}/users/{id}";

            var response = await keycloakService.PutAsync<string>(endpoint, request, true, cancellationToken);

            if (response.IsSuccessful && response.Data is null)
                response.Data = "User update is successful";

            return StatusCode(response.StatusCode, response);
        }
    }
}
