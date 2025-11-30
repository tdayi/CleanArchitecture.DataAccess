using AutoMapper;
using Core.Repository;
using WebApi.Contracts.User;

namespace WebApi.Contracts;

public class ContractMapper : Profile
{
    public ContractMapper()
    {
        CreateMap<GetUsersRequest, PagingRequest>();
        CreateMap<Database.Entity.User, GetUsersResponse.User>();
        CreateMap<PagingResponse<Database.Entity.User>, GetUsersResponse>();
    }
}