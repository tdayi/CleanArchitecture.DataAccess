using AutoMapper;
using Core.Repository;
using WebApi.Database.Entity;
using WebApi.Handlers.User.Queries.GetById;
using WebApi.Handlers.User.Queries.GetUsers;

namespace WebApi.Handlers.User.Profiles;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<GetUsersRequest, PagingRequest>();
        CreateMap<UserEntity, GetUsersResponse.User>();
        CreateMap<PagingResponse<UserEntity>, GetUsersResponse>();
        CreateMap<UserEntity, GetByIdResponse>();
    }
}