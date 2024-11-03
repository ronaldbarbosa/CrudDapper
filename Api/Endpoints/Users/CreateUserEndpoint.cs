using Api.Common.Api;
using Api.DTOs;
using Api.Handlers;
using Api.Models;

namespace Api.Endpoints.Users;

public class CreateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder routeBuilder)
        => routeBuilder.MapPost("/", HandleAsync)
            .WithName("Users: Create")
            .WithSummary("Creates a new user.")
            .WithDescription("Creates a new user.")
            .WithOrder(3)
            .Produces<Response<UserDTO>>();

    private static async Task<IResult> HandleAsync(IUserHandler userHandler, CreateUserDTO user)
    {
        var result = await userHandler.CreateUser(user);
        
        return result.Status
            ? TypedResults.Created($"/users/{result.Data!.Id}", result)
            : TypedResults.BadRequest(result);
    }
}