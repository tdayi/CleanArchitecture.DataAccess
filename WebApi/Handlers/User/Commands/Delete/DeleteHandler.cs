using Core.UnitOfWork;
using MediatR;
using WebApi.Database.Repositories;

namespace WebApi.Handlers.User.Commands.Delete;

public class DeleteHandler : IRequestHandler<DeleteRequest, DeleteResponse>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IUserRepository _userRepository;

    public DeleteHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserRepository userRepository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _userRepository = userRepository;
    }

    public async Task<DeleteResponse> Handle(DeleteRequest request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Create();

        var user = await _userRepository.GetAsync(unitOfWork, x => x.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _userRepository.DeleteAsync(unitOfWork, user, cancellationToken);

        return new DeleteResponse();
    }
}