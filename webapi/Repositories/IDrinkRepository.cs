using webapi.Models;

namespace webapi.Repositories;

public interface IDrinkRepository
{
    void Add(Drink drink);

    IList<Drink> GetAllDrinks();

    void UpdateAll(IList<Drink> drinks);
    void DeleteAll();
}