using WebApi.Constants;

namespace WebApi.Handlers.User.Queries.GetById;

public class GetByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public UserStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}