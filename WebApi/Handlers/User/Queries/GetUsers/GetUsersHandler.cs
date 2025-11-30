using AutoMapper;
using Core.Repository;
using Core.UnitOfWork;
using MediatR;
using WebApi.Database.Repositories;

namespace WebApi.Handlers.User.Queries.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(
        IMapper mapper,
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
        _userRepository = userRepository;
    }

    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Create();

        var pagingRequest = _mapper.Map<PagingRequest>(request);

        var userResponse = await _userRepository.GetUsersAsync(unitOfWork, pagingRequest, cancellationToken);

        return _mapper.Map<GetUsersResponse>(userResponse);
    }
}