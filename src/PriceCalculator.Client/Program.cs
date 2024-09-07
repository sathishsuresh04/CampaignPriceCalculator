using System.Globalization;

var serviceCollection = new ServiceCollection()
    .AddLogging()
    .AddApplicationDependency();


IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
var mediatR = serviceProvider.GetService<IMediator>();

decimal price = 0;

try
{
    if (mediatR != null)
    {
        var priceCalculator = new PriceCalculator.Client.PriceCalculator(mediatR);
        price = await priceCalculator.CalculatePrice(1, 0, 100, DateTime.Today);
        //TODO read from console
    }
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    throw;
}

Console.WriteLine($"Calculated price is {price.ToString(CultureInfo.InvariantCulture)}");
Console.ReadKey();

if (serviceProvider is IDisposable disposable) disposable.Dispose();