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

        var users = new List<UserEntity>
        {
            new UserEntity("Ahmet", 25, UserStatus.Active),
            new UserEntity("Mehmet", 20, UserStatus.Active),
            new UserEntity("Ayşe", 21, UserStatus.Active),
            new UserEntity("Fatma", 23, UserStatus.Active),
            new UserEntity("Veli", 34, UserStatus.Active),
            new UserEntity("Ekin", 21, UserStatus.Active),
            new UserEntity("Selin", 20, UserStatus.Active)
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}