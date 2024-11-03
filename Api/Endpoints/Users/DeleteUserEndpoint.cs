using Api.Common.Api;
using Api.DTOs;
using Api.Handlers;
using Api.Models;

namespace Api.Endpoints.Users;

public class DeleteUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder routeBuilder)
        => routeBuilder.MapDelete("/{id:int}", HandleAsync)
            .WithName("Users: Delete")
            .WithSummary("Deletes a user.")
            .WithDescription("Deletes a user.")
            .WithOrder(5)
            .Produces<Response<UserDTO>>();

    private static async Task<IResult> HandleAsync(IUserHandler handler, int id)
    {
        var result = await handler.DeleteUser(id);
        
        return result.Status
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}