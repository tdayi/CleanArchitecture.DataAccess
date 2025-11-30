using MediatR;

namespace WebApi.Handlers.User.Commands.Delete;

public class DeleteRequest : IRequest<DeleteResponse>
{
    public Guid Id { get; set; }
}