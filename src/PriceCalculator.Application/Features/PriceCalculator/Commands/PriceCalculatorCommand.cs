namespace PriceCalculator.Application.Features.PriceCalculator.Commands;

public class PriceCalculatorCommand : IRequest<decimal>
{
    public int UserType { get; set; }
    public int ProductType { get; set; }
    public decimal Costprice { get; set; }
    public DateTime CampaignEndDate { get; set; }
}