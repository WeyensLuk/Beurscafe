using webapi.Controllers;
using webapi.Models;
using webapi.Repositories;

namespace webapi.Tests;

public class DrinkControllerTests
{
    private DrinkController _DrinkController;

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
    public void ChangeAmountBought_RaisesAmountBoughtProperty()
    {
        AddDrinks();

        _DrinkController.SetTotalAmountPurchased("Cava", 1);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks.Single(d => d.Name == "Cava").AmountPurchased, Is.EqualTo(1));
    }

    [Test]
    public void ChangeAmountBought_RaisesPriceOfDrinkPurchased()
    {
        AddDrinks();

        _DrinkController.SetTotalAmountPurchased("Cava", 1);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        var cava = drinks.Single(d => d.Name == "Cava");
        Assert.That(cava.CurrentPrice, Is.EqualTo(4.6m));
    }

    [Test]
    public void ChangeAmountBought_ReducesPriceOfOtherDrinks()
    {
        AddDrinks();

        _DrinkController.SetTotalAmountPurchased("Cava", 1);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks.Single(d => d.Name == "Duvel").CurrentPrice, Is.EqualTo(3.45m));
        Assert.That(drinks.Single(d => d.Name == "Witte wijn").CurrentPrice, Is.EqualTo(2.95m));
    }

    [Test]
    public void ChangeAmountBought_SeveralTimes_CalculatesCorrectPrice()
    {
        AddDrinks();

        _DrinkController.SetTotalAmountPurchased("Cava", 1);
        _DrinkController.SetTotalAmountPurchased("Duvel", 1);
        _DrinkController.SetTotalAmountPurchased("Cava", 2);
        var drinks = _DrinkController.GetAllDrinks().ToList();
        Assert.That(drinks.Single(d => d.Name == "Cava").CurrentPrice, Is.EqualTo(4.65m));
        Assert.That(drinks.Single(d => d.Name == "Duvel").CurrentPrice, Is.EqualTo(3.5m));
        Assert.That(drinks.Single(d => d.Name == "Witte wijn").CurrentPrice, Is.EqualTo(2.85m));
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
    }

    [TearDown]
    public void TearDown()
    {
        DependencyContainer.Resolve<IDrinkRepository>().UpdateAll(new List<Drink>());
    }

    private void AddDrinks()
    {
        _DrinkController.Add(new Drink { Name = "Cava", OriginalPrice = 4.5m });
        _DrinkController.Add(new Drink { Name = "Duvel", OriginalPrice = 3.5m });
        _DrinkController.Add(new Drink { Name = "Witte wijn", OriginalPrice = 3m });
    }
}