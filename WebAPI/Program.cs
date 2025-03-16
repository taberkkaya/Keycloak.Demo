using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using WebAPI.Options;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<KeycloakConfiguration>(builder.Configuration.GetSection("KeycloakConfiguration"));

builder.Services.AddScoped<KeycloakService>();

builder.Services.AddControllers();

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User_GetAll", builder =>
    {
        builder.RequireResourceRoles("User_GetAll");
    }); 
  
    options.AddPolicy("User_Create", builder =>
    {
        builder.RequireResourceRoles("User_Create");
    }); 

    options.AddPolicy("User_Update", builder =>
    {
        builder.RequireResourceRoles("User_Update");
    }); 

    options.AddPolicy("User_Delete", builder =>
    {
        builder.RequireResourceRoles("User_Delete");
    });
}).AddKeycloakAuthorization(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyOrigin()
                  .AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/get-access-token", async (KeycloakService keycloakService) =>
{
    string token = await keycloakService.GetAccessToken(default);
    return Results.Ok(new {AccessToken = token});
});

app.MapControllers();

app.Run();
