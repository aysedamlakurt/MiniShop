using FluentValidation;
using MiniShop.Dtos;

namespace MiniShop.Validators;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name zorunludur")
            .MaximumLength(100).WithMessage("Name en fazla 100 karakter olabilir");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description zorunludur")
            .MaximumLength(500).WithMessage("Description en fazla 500 karakter olabilir");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price 0'dan büyük olmalı");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock negatif olamaz");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Geçerli bir CategoryId olmalı");
    }
}

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        // Id kontrolü
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id geçerli olmalı");

        // Create kuralları ile aynı kuralları burada tekrar tanımlıyoruz
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name zorunludur")
            .MaximumLength(100).WithMessage("Name en fazla 100 karakter olabilir");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description zorunludur")
            .MaximumLength(500).WithMessage("Description en fazla 500 karakter olabilir");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price 0'dan büyük olmalı");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock negatif olamaz");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Geçerli bir CategoryId olmalı");
    }
}
