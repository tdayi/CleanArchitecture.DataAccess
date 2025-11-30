using AutoMapper;
using Core.UnitOfWork;
using MediatR;
using WebApi.Database.Repositories;

namespace WebApi.Handlers.User.Queries.GetById;

public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IUserRepository _userRepository;

    public GetByIdHandler(
        IMapper mapper,
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
        _userRepository = userRepository;
    }

    public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Create();

        var user = await _userRepository.GetAsync(unitOfWork, x => x.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return _mapper.Map<GetByIdResponse>(user);
    }
}