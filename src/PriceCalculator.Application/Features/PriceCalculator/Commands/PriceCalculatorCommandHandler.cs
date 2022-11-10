namespace PriceCalculator.Application.Features.PriceCalculator.Commands;

public class PriceCalculatorCommandHandler : IRequestHandler<PriceCalculatorCommand, decimal>
{
    public async Task<decimal> Handle(PriceCalculatorCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));


        var productType = (ProductType)request.ProductType;

        var campainDiscount = GetCampaignDiscount(request);

        return productType switch
        {
            ProductType.Insurance => await CalculatePrice(request.Costprice, Constants.InsuranceProductMargin,
                campainDiscount),
            ProductType.Hardware => await CalculatePrice(request.Costprice, Constants.HardwareProductMargin,
                campainDiscount),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    ///     Gets the campaign discount by user type and the campaign end date
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static decimal GetCampaignDiscount(PriceCalculatorCommand request)
    {
        decimal campDisc = Constants.DefaultCampaignDiscount;
        if (request.CampaignEndDate >= DateTime.Today)
            campDisc = Constants
                .CurrentCampainDiscount; // assumeed the campaign end date will be today or future day both for normal user and business users
        var userType = (UserType)request.UserType;
        var productType = (ProductType)request.ProductType;
        switch (userType)
        {
            case UserType.Consumer:
                break;
            case UserType.Business:
                campDisc += Constants.BusinessUserAdditionalDiscount;
                break;
            case UserType.LargeCorporateUser:
                campDisc = productType switch
                {
                    ProductType.Insurance => (request.Costprice + Constants.InsuranceProductMargin) * 10 /
                                             100, //10 percent
                    ProductType.Hardware => (request.Costprice + Constants.HardwareProductMargin) * 10 / 100,
                    _ => campDisc
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return campDisc;
    }

    /// <summary>
    ///     Calculate the price of the product
    /// </summary>
    /// <param name="productCost"></param>
    /// <param name="productMagin"></param>
    /// <param name="campaignDiscount"></param>
    /// <returns></returns>
    private static async Task<decimal> CalculatePrice(decimal productCost, int productMagin,
        decimal campaignDiscount = 0)
    {
        return await Task.Run(() => productCost + productMagin - campaignDiscount);
    }
}