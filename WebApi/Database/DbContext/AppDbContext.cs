using Microsoft.EntityFrameworkCore;
using WebApi.Constants;
using WebApi.Database.Entity;

namespace WebApi.Database.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Age);
            entity.Property(x => x.CreatedAt);
            entity.Property(x => x.UpdatedAt);
            entity.Property(x => x.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserStatus)Enum.Parse(typeof(UserStatus), v)
                )
                .HasMaxLength(50)
                .IsRequired();
        });
    }
}