using Api.Handlers;
using Security.Interfaces;
using Security.Services;

namespace Api.Common.Api;

public static class BuilderExtension
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IUserHandler, UserHandler>();
        builder.Services.AddTransient<ISecurityHandler, SecurityHandler>();
    }
}