using webapi.Models;
using webapi.Repositories;

namespace webapi.Services;

public class DrinkService : IDrinkService
{
    private readonly decimal _ChangeAmount;
    private readonly IDrinkRepository _DrinkRepository;

    public DrinkService(IDrinkRepository drinkRepository, IConfiguration configuration)
    {
        _DrinkRepository = drinkRepository;
        _ChangeAmount = configuration.GetValue<decimal>("Parameters:DrinkChangeAmount");
    }

    public void CalculateNewPrices(Drink newDrink)
    {
        var drinks = _DrinkRepository.GetAllDrinks();
        drinks.Single(d => d.Name == newDrink.Name).AmountPurchased = newDrink.AmountPurchased;
        foreach (var drink in drinks)
        {
            drink.ResetPrice();
        }

        for (var index = 0; index < drinks.Count; index++)
        {
            var drink = drinks[index];
            drink.CurrentPrice += _ChangeAmount * drink.AmountPurchased;
            CalculateNewPriceForAmountPurchased(drink, drinks, index);
        }

        _DrinkRepository.UpdateAll(drinks);
    }

    private void CalculateNewPriceForAmountPurchased(Drink drink, IList<Drink> drinks, int index)
    {
        var i = 0;
        var j = index;

        while (i < drink.AmountPurchased)
        {
            for (int k = 0; k < 4; k++)
            {
                if (drinks[j] == drink) j++;
                drinks[j].CurrentPrice -= _ChangeAmount / 4;
                j++;
                if (j == drinks.Count) j = 0;
            }

            i++;
        }
    }
}