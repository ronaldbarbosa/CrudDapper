using Api.Handlers;

namespace Api.Common.Api;

public static class BuilderExtension
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IUserHandler, UserHandler>();
    }
}