using Core.Contract;
using WebApi.Constants;

namespace WebApi.Handlers.User.Queries.GetUsers;

public class GetUsersResponse : ResponsePaginationBase
{
    public int TotalCount { get; set; }
    public IEnumerable<User> Result { get; set; }

    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}