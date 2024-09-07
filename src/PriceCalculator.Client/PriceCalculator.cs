namespace PriceCalculator.Client;

public class PriceCalculator(IMediator mediator)
{
    /// <summary>
    ///     This function handles all calculation you ever need!
    /// </summary>
    /// <param name="usertype"> 0= Normal, 1 = Company</param>
    /// <param name="productType"> 0= Auction, 1 = BuyItNow</param>
    /// <param name="costPrice"></param>
    /// <param name="campaignEndDate">campaign end date</param>
    /// <returns></returns>
    public async Task<decimal> CalculatePrice(int usertype, int productType, decimal costPrice,
        DateTime campaignEndDate)
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = usertype,
            ProductType = productType,
            Costprice = costPrice,
            CampaignEndDate = campaignEndDate
        };

        var response = await mediator.Send(priceCalculatorCommand);
        return response;
    }
}