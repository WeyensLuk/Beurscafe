using LiteDB;
using webapi.Models;

namespace webapi.Repositories;

public class DrinkRepository : IDrinkRepository
{
    private readonly LiteCollection<Drink> _Collection;
    private readonly LiteDatabase _DB;

    public DrinkRepository()
    {
        _DB = new LiteDatabase("Drinks.db");
        _Collection = (LiteCollection<Drink>)_DB.GetCollection<Drink>("drinks");
    }

    public void Add(Drink drink)
    {
        if (_Collection.Exists(d => d.Name == drink.Name)) throw new InvalidOperationException();
        _Collection.Insert(drink);
    }

    public void DeleteAll()
    {
        _Collection.DeleteAll();
    }

    public IList<Drink> GetAllDrinks()
    {
        return _Collection.FindAll().ToList();
    }

    public void UpdateAll(IList<Drink> drinks)
    {
        _Collection.Update(drinks);
    }
}