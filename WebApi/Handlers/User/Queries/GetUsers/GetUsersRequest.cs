using Core.Contract;
using MediatR;

namespace WebApi.Handlers.User.Queries.GetUsers;

public class GetUsersRequest : RequestPaginationBase, IRequest<GetUsersResponse>
{
    
}