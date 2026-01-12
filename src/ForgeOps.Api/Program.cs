var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var version = Environment.GetEnvironmentVariable("APP_VERSION") ?? "dev";
bool failMode = false;

app.MapGet("/health", () =>
{
    if (failMode)
        return Results.StatusCode(500);

    return Results.Ok("healthy");
});

app.MapGet("/version", () =>
{
    return Results.Ok(new { version });
});

app.MapPost("/simulate-error", () =>
{
    failMode = true;
    return Results.Ok("failure mode enabled");
});

app.Run();
