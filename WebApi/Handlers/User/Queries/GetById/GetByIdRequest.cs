using MediatR;

namespace WebApi.Handlers.User.Queries.GetById;

public class GetByIdRequest : IRequest<GetByIdResponse>
{
    public Guid Id { get; set; }
}