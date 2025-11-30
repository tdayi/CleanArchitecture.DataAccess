using Core.UnitOfWork;
using MediatR;
using WebApi.Database.Entity;
using WebApi.Database.Repositories;

namespace WebApi.Handlers.User.Commands.Create;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IUserRepository _userRepository;

    public CreateHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserRepository userRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _userRepository = userRepository;
    }

    public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Create();

        var user = new UserEntity(
            request.Name,
            request.Age,
            request.Status);

        await _userRepository.InsertAsync(unitOfWork, user, cancellationToken);

        return new CreateResponse
        {
            Id = user.Id
        };
    }
}