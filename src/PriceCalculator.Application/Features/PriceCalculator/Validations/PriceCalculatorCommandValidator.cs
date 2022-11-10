namespace PriceCalculator.Application.Features.PriceCalculator.Validations;

public class PriceCalculatorCommandValidator : AbstractValidator<PriceCalculatorCommand>
{
    public PriceCalculatorCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.UserType).NotNull().WithMessage("User type is not defined").Custom((x, context) =>
        {
            if (!Enum.IsDefined(typeof(UserType), x)) context.AddFailure("User type is not valid");
        });
        RuleFor(x => x.ProductType).NotNull().WithMessage("Product type is not defined").Custom((x, context) =>
        {
            if (Enum.IsDefined(typeof(ProductType), x)) return;
            context.AddFailure("Product type is not valid");
        });
        RuleFor(x => x.Costprice).Cascade(CascadeMode.Stop).NotEmpty()
            .WithMessage("Purchase price should be specified and cannot be 0");
        RuleFor(x => x.CampaignEndDate).Must(BeAValidDate).WithMessage("Campaign EndDate is required");
    }

    private static bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default);
    }
}