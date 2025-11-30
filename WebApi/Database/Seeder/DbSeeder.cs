using WebApi.Constants;
using WebApi.Database.DbContext;
using WebApi.Database.Entity;

namespace WebApi.Database.Seeder;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        var users = new List<User>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Ahmet",
                Age = 25,
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Mehmet",
                Age = 32,
                Status = UserStatus.Passive,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                IsActive = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Ayşe",
                Age = 28,
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Fatma",
                Age = 40,
                Status = UserStatus.Passive,
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                IsActive = false
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}