using Core.UnitOfWork;
using MediatR;
using WebApi.Database.Repositories;

namespace WebApi.Handlers.User.Commands.Update;

public class UpdateHandler : IRequestHandler<UpdateRequest, UpdateResponse>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IUserRepository _userRepository;

    public UpdateHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserRepository userRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _userRepository = userRepository;
    }

    public async Task<UpdateResponse> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Create();

        var user = await _userRepository.GetAsync(unitOfWork, x => x.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        user.Update(request.Name, request.Age, request.Status);

        await _userRepository.UpdateAsync(unitOfWork, user, cancellationToken);

        return new UpdateResponse
        {
            Id = user.Id
        };
    }
}