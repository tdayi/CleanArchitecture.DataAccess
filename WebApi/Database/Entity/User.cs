using WebApi.Constants;

namespace WebApi.Database.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public UserStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
