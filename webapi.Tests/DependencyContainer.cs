using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapi.Controllers;
using webapi.Repositories;
using webapi.Services;

namespace webapi.Tests;

public static class DependencyContainer
{
    private static readonly Dictionary<string, string> Configuration = new()
    {
        {"Parameters:DrinkChangeAmount", "0.10"}
    };

    private static readonly ServiceProvider Provider;

    static DependencyContainer()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IDrinkRepository, DrinkRepository>();
        services.AddTransient<DrinkController>();
        services.AddTransient<IDrinkService, DrinkService>();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().AddInMemoryCollection(Configuration).Build());
        Provider = services.BuildServiceProvider();
    }

    public static T Resolve<T>()
    {
        var service = Provider.GetService<T>();
        return service == null
            ? throw new KeyNotFoundException()
            : service;
    }
}