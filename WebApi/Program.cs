using FluentValidation;
using Serilog;
using WebApi.Database;
using WebApi.Database.DbContext;
using WebApi.Database.Seeder;
using WebApi.Middleware;
using Microsoft.OpenApi.Models;
using WebApi.Handlers.User.Commands.Create;
using WebApi.Handlers.User.Profiles;
using WebApi.Handlers.User.Queries.GetUsers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.logs.json", true, true);

IConfiguration config = builder.Configuration;

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddRepositories(config);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataAccess Api", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(GetUsersHandler).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<CreateRequestValidator>(ServiceLifetime.Scoped);

builder.Services.AddAutoMapper(cfg => { }, typeof(UserMapper).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated();
    DbSeeder.Seed(db);
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();