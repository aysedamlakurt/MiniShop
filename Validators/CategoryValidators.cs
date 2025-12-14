using FluentValidation;
using MiniShop.Dtos;

namespace MiniShop.Validators;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name zorunludur")
            .MaximumLength(100).WithMessage("Name en fazla 100 karakter olabilir");
    }
}

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        // Id kontrolü
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id geçerli olmalı");

        // Create kuralları ile aynı mantık
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name zorunludur")
            .MaximumLength(100).WithMessage("Name en fazla 100 karakter olabilir");
    }
}
