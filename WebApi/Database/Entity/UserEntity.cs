using WebApi.Constants;

namespace WebApi.Database.Entity;

public class UserEntity
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
    public int Age { get; protected set; }
    public UserStatus Status { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    public UserEntity()
    {
    }

    public UserEntity(string name, int age, UserStatus status)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
        Status = status;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name, int age, UserStatus status)
    {
        Name = name;
        Age = age;
        Status = status;
        UpdatedAt = DateTime.Now;
    }
}