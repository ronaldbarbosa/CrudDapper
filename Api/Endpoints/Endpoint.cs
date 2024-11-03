using Api.Common.Api;
using Api.Endpoints.Users;

namespace Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("users")
            .WithTags("Users")
            .MapEndpoint<GetAllUsersEndpoint>()
            .MapEndpoint<GetUserByIdEndpoint>()
            .MapEndpoint<CreateUserEndpoint>()
            .MapEndpoint<UpdateUserEndpoint>()
            .MapEndpoint<DeleteUserEndpoint>();
    }
    
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoints)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(endpoints);
        return endpoints;
    }
}