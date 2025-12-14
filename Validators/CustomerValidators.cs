using FluentValidation;
using MiniShop.Dtos;

namespace MiniShop.Validators;

public class CustomerCreateDtoValidator : AbstractValidator<CustomerCreateDto>
{
    public CustomerCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name zorunludur")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur")
            .EmailAddress().WithMessage("Geçerli bir email giriniz");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password zorunludur")
            .MinimumLength(6).WithMessage("Password en az 6 karakter olmalı");
    }
}

public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
{
    public CustomerUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id geçerli olmalı");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name zorunludur")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur")
            .EmailAddress().WithMessage("Geçerli bir email giriniz");
    }
}


