namespace PriceCalculator.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        
        services.AddMediatR(
            cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(IPriceCalculatorRoot));
            });
        return services;
    }
}