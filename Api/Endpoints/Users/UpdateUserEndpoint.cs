using Api.Common.Api;
using Api.DTOs;
using Api.Handlers;
using Api.Models;

namespace Api.Endpoints.Users;

public class UpdateUserEndpoint :IEndpoint
{
    public static void Map(IEndpointRouteBuilder routeBuilder)
        => routeBuilder.MapPut("/", HandleAsync)
            .WithName("Users: Update")
            .WithSummary("Updates a user.")
            .WithDescription("Updates a user.")
            .WithOrder(4)
            .Produces<Response<UserDTO>>();

    private static async Task<IResult> HandleAsync(IUserHandler userHandler, UpdateUserDTO user)
    {
        var result = await userHandler.UpdateUser(user);
        
        return result.Status
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}