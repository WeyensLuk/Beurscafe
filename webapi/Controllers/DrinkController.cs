using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Repositories;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("drinks")]
public class DrinkController : ControllerBase
{
    private readonly IDrinkRepository _DrinkRepository;
    private readonly IDrinkService _DrinkService;

    public DrinkController(IDrinkRepository drinkRepository, IDrinkService drinkService)
    {
        _DrinkRepository = drinkRepository;
        _DrinkService = drinkService;
    }

    [HttpPost]
    public IActionResult Add(Drink drink)
    {
        _DrinkRepository.Add(drink);
        return new OkResult();
    }

    [HttpGet]
    public IEnumerable<Drink> GetAllDrinks()
    {
        return _DrinkRepository.GetAllDrinks();
    }

    [HttpPut]
    public IActionResult SetTotalAmountPurchased(Drink drink)
    {
        _DrinkService.CalculateNewPrices(drink);
        return new OkResult();
    }
}