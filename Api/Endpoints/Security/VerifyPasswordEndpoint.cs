using Api.Common.Api;
using Api.DTOs;
using Api.Handlers;
using Api.Models;
using Security.Enums;
using Security.Interfaces;

namespace Api.Endpoints.Security;

public class VerifyPasswordEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder routeBuilder)
        => routeBuilder.MapPost("/", HandleAsync)
            .WithName("Security: Verify Password")
            .WithSummary("Verify password.")
            .WithDescription("Verify password.")
            .WithOrder(1)
            .Produces<Response<bool>>();
    
    private static async Task<IResult> HandleAsync(ISecurityHandler securityHandler, IUserHandler userHandler, VerifyUserPasswordDTO userInfo)
    {
        var hashedPassword = await userHandler.GetUserPassword(userInfo.Id);
        var result = securityHandler.VerifyHashedPassword(hashedPassword.Data ?? "", userInfo.Password);
        
        return result == PasswordVerificationResult.Success
            ? TypedResults.Ok()
            : TypedResults.BadRequest();
    }
}