using Core.EntityFramework.UnitOfWork;
using Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using WebApi.Database.DbContext;
using WebApi.Database.Repositories;

namespace WebApi.Database;

public static class DatabaseInstaller
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbContextFactory, AppDbContextFactory>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
    }
}