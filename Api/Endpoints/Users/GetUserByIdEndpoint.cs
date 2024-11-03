using Api.Common.Api;
using Api.DTOs;
using Api.Handlers;
using Api.Models;

namespace Api.Endpoints.Users;

public class GetUserByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder routeBuilder)
        => routeBuilder.MapGet("/{id:int}", HandleAsync)
            .WithName("Users: Get By Id")
            .WithSummary("Returns a user by id.")
            .WithDescription("Returns a user by id.")
            .WithOrder(2)
            .Produces<Response<UserDTO>>();

    private static async Task<IResult> HandleAsync(IUserHandler handler, int id)
    {
        var result = await handler.GetUserById(id);
        return result.Status
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);
    }
}