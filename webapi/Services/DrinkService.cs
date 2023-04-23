using webapi.Models;
using webapi.Repositories;

namespace webapi.Services;

public class DrinkService : IDrinkService
{
    private readonly IConfiguration _Configuration;
    private readonly IDrinkRepository _DrinkRepository;

    public DrinkService(IDrinkRepository drinkRepository, IConfiguration configuration)
    {
        _DrinkRepository = drinkRepository;
        _Configuration = configuration;
    }

    public void CalculateNewPrices(Drink newDrink)
    {
        var drinks = _DrinkRepository.GetAllDrinks();
        drinks.Single(d => d.Name == newDrink.Name).AmountPurchased = newDrink.AmountPurchased;
        var changeAmount = _Configuration.GetValue<decimal>("Parameters:DrinkChangeAmount");

        foreach (var drink in drinks)
        {
            drink.CurrentPrice = drink.OriginalPrice + changeAmount * drink.AmountPurchased
                                 - changeAmount / (drinks.Count - 1) * (drinks.Sum(d => d.AmountPurchased) - drink.AmountPurchased);
        }

        _DrinkRepository.UpdateAll(drinks);
    }
}