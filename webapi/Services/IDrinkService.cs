using webapi.Models;

namespace webapi.Services;

public interface IDrinkService
{
    void CalculateNewPrices(Drink newDrink);
}