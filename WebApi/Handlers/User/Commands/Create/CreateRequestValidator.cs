using FluentValidation;

namespace WebApi.Handlers.User.Commands.Create;

public class CreateRequestValidator: AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Age).GreaterThan(0);
    }
}