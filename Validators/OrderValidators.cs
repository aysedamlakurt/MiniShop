using FluentValidation;
using MiniShop.Dtos;

namespace MiniShop.Validators;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId geçerli olmalı");

        RuleFor(x => x.Adet)
            .GreaterThan(0).WithMessage("Adet 0'dan büyük olmalı");
    }
}

public class CreateOrderRequestDtoValidator : AbstractValidator<CreateOrderRequestDto>
{
    public CreateOrderRequestDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId geçerli olmalı");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("En az bir ürün olmalı");

        RuleForEach(x => x.Items)
            .SetValidator(new OrderItemDtoValidator());
    }
}
