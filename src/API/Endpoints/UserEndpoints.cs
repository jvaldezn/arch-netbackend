using API.Extensions;
using Data.Dto;
using Domain.UseCase;

namespace API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/User").WithTags(nameof(UserDto));

            group.MapGet("/", async (IUserUseCase userUseCase) =>
            {
                return Results.Ok(await userUseCase.GetAllUsers());
            })
            .WithName("GetAllUsers")
            .WithOpenApi()
            .RequireAuthorization();

            group.MapGet("/{id}", async (int id, IUserUseCase userUseCase) =>
            {
                return Results.Ok(await userUseCase.GetUserById(id));
            })
            .WithName("GetUserById")
            .WithOpenApi()
            .RequireAuthorization();

            group.MapPut("/{id}", async (int id, UserDto userDto, IUserUseCase userUseCase) =>
            {
                return Results.Ok(await userUseCase.UpdateUser(id, userDto));
            })
            .WithName("UpdateUser")
            .WithOpenApi()
            .RequireAuthorization();

            group.MapPost("/", async (UserDto userDto, IUserUseCase userUseCase) =>
            {
                return Results.Ok(await userUseCase.CreateUser(userDto));
            })
            .WithName("CreateUser")
            .WithFluentValidation<UserDto>()
            .WithOpenApi()
            .RequireAuthorization();

            group.MapDelete("/{id}", async (int id, IUserUseCase userUseCase) =>
            {
                return Results.Ok(await userUseCase.DeleteUser(id));
            })
            .WithName("DeleteUser")
            .WithOpenApi()
            .RequireAuthorization();

            group.MapPost("/login", async (string username, string password, IUserUseCase userUseCase) =>
            {
                return Results.Ok(await userUseCase.AuthenticateUser(username, password));
            })
            .WithName("LoginUser")
            .WithOpenApi();
        }
    }
}
