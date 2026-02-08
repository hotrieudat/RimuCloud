using Carter;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RimuCloud.Application.Todo;
using RimuCloud.Shared.Request;

namespace RimuCloud.ApiService.Presentation.Endpoints
{
    public class Todo : AbstractEndpoint, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/todo")
                       .WithTags("Todo");


            string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

            group.MapGet("", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetTodo");

            group.MapPost("", async (CreateTodoRequest request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new CreateTodoCommand(request), cancellationToken);
                if (result.IsFailure) return HandlerFailure(result);
                return ResponseOk(result);
            })
            .WithName("CreateTodo");
        }
    }

    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
