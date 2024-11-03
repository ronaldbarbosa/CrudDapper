using Api.Common.Api;
using Api.DTOs;
using Api.Handlers;
using Api.Models;

namespace Api.Endpoints.Users;

public class GetAllUsersEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder routeBuilder)
        => routeBuilder.MapGet("/", HandleAsync)
            .WithName("Users: Get All")
            .WithSummary("Returns a list of all users.")
            .WithDescription("Returns a list of all users.")
            .WithOrder(1)
            .Produces<Response<List<UserDTO>>>();

    private static async Task<IResult> HandleAsync(IUserHandler handler)
    {
        var result = await handler.GetAllUsers();
        return result.Status 
            ? TypedResults.Ok(result) 
            : Results.NotFound(result);
    }
}