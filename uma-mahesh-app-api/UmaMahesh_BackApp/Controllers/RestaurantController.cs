using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UmaMahesh_BackApp.Data;
using UmaMahesh_BackApp.Models.Restaurants;

namespace UmaMahesh_BackApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : Controller
{

    private readonly DataRepositoryContext _repositoryContext;

    public RestaurantsController(DataRepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await _repositoryContext.Restaurants.Select(rest => rest).ToListAsync();
        return Ok(restaurants);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantsById([FromRoute] int id)
    {
        var restaurant =
            await _repositoryContext.Restaurants.FirstOrDefaultAsync(prod => prod.Id == id);

        if (restaurant == null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant(Restaurants restaurant)
    {
        await _repositoryContext.Restaurants.AddAsync(restaurant);
        _repositoryContext.SaveChanges();
        return Ok();
    }


    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int Id ,  Restaurants _restaurant)
    {
        var restaurant = await _repositoryContext.Restaurants.FindAsync(Id);
        if (restaurant == null)
        {
            return NotFound();
        }

        restaurant.Name = _restaurant.Name;
        restaurant.Cuisine = _restaurant.Cuisine;
        restaurant.Address = _restaurant.Address;
        await _repositoryContext.SaveChangesAsync();

        return Ok(restaurant);
    }















}
