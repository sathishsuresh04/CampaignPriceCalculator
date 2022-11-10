namespace PriceCalculator.Application.Tests;

public class PriceCalculatorCommandHandlerTests
{
    private readonly PriceCalculatorCommandHandler _sut;
    private readonly PriceCalculatorCommandValidator _validator;

    public PriceCalculatorCommandHandlerTests()
    {
        _sut = new PriceCalculatorCommandHandler();
        _validator = new PriceCalculatorCommandValidator();
    }

    [Fact]
    public async Task PriceCalculatorCommandHandler_ValidateUserType_Should_Not_Have_Error_Response()
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = 0,
            ProductType = 0,
            Costprice = 10,
            CampaignEndDate = DateTime.Now.AddDays(2)
        };
        var validationResult = await _validator.ValidateAsync(priceCalculatorCommand);
        validationResult.Errors.Count.Should().Be(0);
    }

    [Fact]
    public async Task PriceCalculatorCommandHandler_ValidateUserType_Should_Have_Error_Response()
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = 5,
            ProductType = 0,
            Costprice = 10,
            CampaignEndDate = DateTime.Now.AddDays(2)
        };
        var validationResult = await _validator.ValidateAsync(priceCalculatorCommand);
        validationResult.Errors.Count.Should().Be(1);
    }

    [Fact]
    public async Task PriceCalculatorCommandHandler_ValidateProductType_Should_Not_Have_Error_Response()
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = 0,
            ProductType = 0,
            Costprice = 10,
            CampaignEndDate = DateTime.Now.AddDays(2)
        };
        var validationResult = await _validator.ValidateAsync(priceCalculatorCommand);
        validationResult.Errors.Count.Should().Be(0);
    }

    [Fact]
    public async Task PriceCalculatorCommandHandler_ValidateProductType_Should_Have_Error_Response()
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = 0,
            ProductType = 10,
            Costprice = 10,
            CampaignEndDate = DateTime.Now.AddDays(2)
        };
        var validationResult = await _validator.ValidateAsync(priceCalculatorCommand);
        validationResult.Errors.Count.Should().Be(1);
    }

    [Fact]
    public async Task PriceCalculatorCommandHandler_ConsumerResponse_Should_Be_Correct()
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = 0, //Consumer
            ProductType = 0, //Insurance so, product margin is 25
            Costprice = 10,
            CampaignEndDate = DateTime.Now.AddDays(2) // so campaign discount is 10
        };
        //validate
        var validationResult = await _validator.ValidateAsync(priceCalculatorCommand);
        validationResult.Errors.Count.Should().Be(0);

        decimal expected = 25; //( 10 + 25 - 10) (Product purchase price + product margin - discount)
        //act
        var response = await _sut.Handle(priceCalculatorCommand, CancellationToken.None);

        //assert
        response.Should().NotBe(null);
        response.Should().NotBe(0);
        response.Should().Be(expected);
    }

    [Fact]
    public async Task PriceCalculatorCommandHandler_CompanyResponse_Should_Be_Correct()
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = 1, //Consumer
            ProductType = 0, //Insurance so, product margin is 25
            Costprice = 10,
            CampaignEndDate = DateTime.Now.AddDays(2) // so campaign discount is 10
        };
        decimal expected =
            20; //( 10 + 25 - 10-5) (Product purchase price + product margin - discount-default company discount)
        //act
        var response = await _sut.Handle(priceCalculatorCommand, CancellationToken.None);

        //assert
        response.Should().NotBe(null);
        response.Should().NotBe(0);
        response.Should().Be(expected);
    }

    [Fact]
    public async Task PriceCalculatorCommandHandler_LargeCorporateResponse_Should_Be_Correct()
    {
        var priceCalculatorCommand = new PriceCalculatorCommand
        {
            UserType = 2, //LargeCorporateCustomer
            ProductType = 0, //Insurance so, product margin is 25
            Costprice = 10,
            CampaignEndDate = DateTime.Now.AddDays(2) // so campaign discount is 10
        };
        var expected =
            new decimal(
                31.5); //( 10 + 25 -3.5) (Product purchase price + product margin - discount-default company discount)
        //act
        var response = await _sut.Handle(priceCalculatorCommand, CancellationToken.None);

        //assert
        response.Should().NotBe(null);
        response.Should().NotBe(0);
        response.Should().Be(expected);
    }
}