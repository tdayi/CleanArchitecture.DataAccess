using MediatR;
using WebApi.Constants;
using WebApi.Handlers.User.Commands.Create;

namespace WebApi.Handlers.User.Commands.Update;

public class UpdateRequest : IRequest<UpdateResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public UserStatus Status { get; set; }
}