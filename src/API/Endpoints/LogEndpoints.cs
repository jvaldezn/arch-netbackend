using Data.Dto;
using Domain.UseCase;

namespace API.Endpoints
{
    public static class LogEndpoints
    {
        public static void MapLogEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Log").WithTags(nameof(LogDto));

            group.MapPost("/register", async (LogDto logDto, ILogUseCase logUseCase) =>
            {
                return Results.Ok(await logUseCase.RegisterLog(logDto));
            })
            .WithName("RegisterLog")
            .WithOpenApi();

            group.MapGet("/dates", async (DateTime startDate, DateTime endDate, ILogUseCase logUseCase) =>
            {
                return Results.Ok(await logUseCase.GetLogsByDates(startDate, endDate));
            })
            .WithName("GetLogsByDates")
            .WithOpenApi();

            group.MapGet("/appdate", async (int applicationId, DateTime logged, ILogUseCase logUseCase) =>
            {
                return Results.Ok(await logUseCase.GetLogByApplicationAndDate(applicationId, logged));
            })
            .WithName("GetLogByApplicationAndDate")
            .WithOpenApi();

            group.MapGet("/appyearmonth", async (int applicationId, DateTime logged, ILogUseCase logUseCase) =>
            {
                return Results.Ok(await logUseCase.GetLogByApplicationAndYearAndMonth(applicationId, logged));
            })
            .WithName("GetLogByApplicationAndYearAndMonth")
            .WithOpenApi();

            group.MapGet("/yearmonth", async (DateTime logged, ILogUseCase logUseCase) =>
            {
                return Results.Ok(await logUseCase.GetLogByYearAndMonth(logged));
            })
            .WithName("GetLogByYearAndMonth")
            .WithOpenApi();
        }
    }
}
