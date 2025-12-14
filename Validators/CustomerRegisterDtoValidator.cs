using FluentValidation;
using MiniShop.Dtos;

namespace MiniShop.Validators;

public class CustomerRegisterDtoValidator 
    : AbstractValidator<CustomerRegisterDto>
{
    public CustomerRegisterDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
