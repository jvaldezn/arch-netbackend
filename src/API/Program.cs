using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Endpoints;
using API.Extensions;
using API.Middlewares;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCors(options => {
    options.AddPolicy("ReactClient", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContexts(builder.Configuration);
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));
builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddMassTransit(builder.Configuration);

//builder.Services.ConfigureCustomApiBehavior();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.InitializeDatabase();

app.ConfigureExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors("ReactClient");

//app.UseAuthorization();

//app.MapControllers();

app.MapUserEndpoints();

app.MapProductEndpoints();

app.MapLogEndpoints();

app.Run();
