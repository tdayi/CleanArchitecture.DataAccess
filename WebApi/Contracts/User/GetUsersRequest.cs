using Core.Contract;
using MediatR;

namespace WebApi.Contracts.User;

public class GetUsersRequest : RequestPaginationBase, IRequest<GetUsersResponse>
{
    
}