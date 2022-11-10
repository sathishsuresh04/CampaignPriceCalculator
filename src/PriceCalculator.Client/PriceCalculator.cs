namespace PriceCalculator.Client;

public class PriceCalculator
{
    private readonly IMediator _mediator;

    public PriceCalculator(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     This function handles all calculation you ever need!
    /// </summary>
    /// <param name="usertype"> 0= Normal, 1 = Company</param>
    /// <param name="producttype"> 0= Auction, 1 = BuyItNow</param>
    /// <param name="costprice"></param>
    /// <param name="campaignEndDate">campaign end date</param>
    /// <returns></returns>
    public async Task<decimal> CalculatePrice(int usertype, int producttype, decimal costprice,
        DateTime campaignEndDate)
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = usertype,
            ProductType = producttype,
            Costprice = costprice,
            CampaignEndDate = campaignEndDate
        };

        var response = await _mediator.Send(priceCalculatorCommand);
        return response;
    }
}