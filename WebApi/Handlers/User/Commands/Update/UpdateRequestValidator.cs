using FluentValidation;
using WebApi.Handlers.User.Commands.Create;

namespace WebApi.Handlers.User.Commands.Update;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Age).GreaterThan(0);
    }
}