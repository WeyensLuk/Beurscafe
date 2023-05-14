using LiteDB;
using webapi.Controllers;
using webapi.Models;
using webapi.Repositories;

namespace webapi.Tests;

public class DrinkControllerTests
{
    private Drink _Cava = new Drink { Name = "Cava", OriginalPrice = 4.5m, CurrentPrice = 4.5m, MinimumPrice = 1, Id = ObjectId.NewObjectId() };
    private DrinkController _DrinkController;
    private Drink _Duvel = new Drink { Name = "Duvel", OriginalPrice = 3.5m, CurrentPrice = 3.5m, MinimumPrice = 0.5m, Id = ObjectId.NewObjectId() };
    private Drink _WitteWijn = new Drink { Name = "Witte wijn", OriginalPrice = 3m, CurrentPrice = 3m, MinimumPrice = 0.5m, Id = ObjectId.NewObjectId() };

    [Test]
    public void Add_AddsDrink()
    {
        _DrinkController.Add(new Drink { Name = "Cava", OriginalPrice = 4.5m });
        Assert.Throws<InvalidOperationException>(() => _DrinkController.Add(new Drink { Name = "Cava", OriginalPrice = 4.5m }));
        Assert.That(_DrinkController.GetAllDrinks().ToList(), Has.Count.EqualTo(1));
    }

    [Test]
    public void Add_DoesNotAddIfDrinkWithSameNameAlreadyExists()
    {
        _DrinkController.Add(new Drink { Name = "Cava", OriginalPrice = 4.5m });
        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks, Has.Count.EqualTo(1));
        Assert.That(drinks, Has.One.Matches<Drink>(d => d.Name == "Cava"));
    }

    [Test]
    public void ChangeAmountBought_DoesNotDropPricesOfOtherDrinksBelowMinimumPrice()
    {
        AddDrinks();

        _Cava.AmountPurchased = 60;
        _DrinkController.SetTotalAmountPurchased(_Cava);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks.Single(d => d.Name == "Cava").CurrentPrice, Is.EqualTo(16.5m));
        Assert.That(drinks.Single(d => d.Name == "Duvel").CurrentPrice, Is.EqualTo(drinks.Single(d => d.Name == "Duvel").MinimumPrice));
        Assert.That(drinks.Single(d => d.Name == "Witte wijn").CurrentPrice, Is.EqualTo(drinks.Single(d => d.Name == "Witte wijn").MinimumPrice));
    }

    [Test]
    public void ChangeAmountBought_RaisesAmountBoughtProperty()
    {
        AddDrinks();

        _Cava.AmountPurchased = 1;
        _DrinkController.SetTotalAmountPurchased(_Cava);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks.Single(d => d.Name == "Cava").AmountPurchased, Is.EqualTo(1));
    }

    [Test]
    public void ChangeAmountBought_RaisesPriceOfDrinkPurchased()
    {
        AddDrinks();

        _Cava.AmountPurchased = 1;
        _DrinkController.SetTotalAmountPurchased(_Cava);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        var cava = drinks.Single(d => d.Name == "Cava");
        Assert.That(cava.CurrentPrice, Is.EqualTo(4.7m));
    }

    [Test]
    public void ChangeAmountBought_ReducesPriceOfOtherDrinks()
    {
        AddDrinks();

        _Cava.AmountPurchased = 1;
        _DrinkController.SetTotalAmountPurchased(_Cava);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks.Single(d => d.Name == "Duvel").CurrentPrice, Is.EqualTo(3.4m));
        Assert.That(drinks.Single(d => d.Name == "Witte wijn").CurrentPrice, Is.EqualTo(2.9m));
    }

    [Test]
    public void ChangeAmountBought_SeveralTimes_CalculatesCorrectPrice()
    {
        AddDrinks();

        _Cava.AmountPurchased = 1;
        _DrinkController.SetTotalAmountPurchased(_Cava);
        _Duvel.AmountPurchased = 1;
        _DrinkController.SetTotalAmountPurchased(_Duvel);
        _Cava.AmountPurchased = 2;
        _DrinkController.SetTotalAmountPurchased(_Cava);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks.Single(d => d.Name == "Cava").CurrentPrice, Is.EqualTo(4.8m));
        Assert.That(drinks.Single(d => d.Name == "Duvel").CurrentPrice, Is.EqualTo(3.5m));
        Assert.That(drinks.Single(d => d.Name == "Witte wijn").CurrentPrice, Is.EqualTo(2.7m));
    }

    [Test]
    public void GetAllDrinks_ReturnsAllAvailableDrinks()
    {
        AddDrinks();

        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks, Has.Count.EqualTo(3));
        Assert.That(drinks, Has.One.Matches<Drink>(d => d.Name == "Cava"));
        Assert.That(drinks, Has.One.Matches<Drink>(d => d.Name == "Duvel"));
        Assert.That(drinks, Has.One.Matches<Drink>(d => d.Name == "Witte wijn"));
    }

    [SetUp]
    public void SetUp()
    {
        _DrinkController = DependencyContainer.Resolve<DrinkController>();
        DependencyContainer.Resolve<IDrinkRepository>().DeleteAll();
    }

    private void AddDrinks()
    {
        _DrinkController.Add(_Cava);
        _DrinkController.Add(_Duvel);
        _DrinkController.Add(_WitteWijn);
    }
}