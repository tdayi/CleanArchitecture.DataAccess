using MediatR;
using WebApi.Constants;

namespace WebApi.Handlers.User.Commands.Create;

public class CreateRequest : IRequest<CreateResponse>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public UserStatus Status { get; set; }
}