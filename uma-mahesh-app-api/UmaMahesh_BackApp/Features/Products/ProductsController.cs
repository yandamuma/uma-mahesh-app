using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UmaMahesh_BackApp.Entities.Custom;
using UmaMahesh_BackApp.Entities.Products;

namespace UmaMahesh_BackApp.Features.Products;

[Authorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]

public class ProductsController : Controller
{

    private readonly DataRepositoryContext _dataRepositoryContext;
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(DataRepositoryContext dataRepositoryContext, ILogger<ProductsController> logger)
    {
        _dataRepositoryContext = dataRepositoryContext;
        _logger = logger;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
    {
        try
        {
            IQueryable<Product> products = _dataRepositoryContext.Products;

            if (queryParameters.MinPrice != null)
            {
                products = products
                                    .Where(p => p.Price >= queryParameters.MinPrice.Value)
                                    .OrderBy(p => p.Price);
            }

            if (queryParameters.MaxPrice != null)
            {
                products = products
                                    .Where(p => p.Price <= queryParameters.MaxPrice.Value)
                                    .OrderByDescending(p => p.Price); ;
            }

            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                products = products.Where(
                                       p => p.Color!.ToLower() == queryParameters.SearchTerm ||
                                                p.Name!.ToLower().Contains(queryParameters.SearchTerm));
            }


            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products
                    .Where(p => p.Name!.ToLower().Contains(queryParameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParameters.Color))
            {
                products = products
                    .Where(p => p.Color == queryParameters.Color.ToLower());
            }

            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products
                        .OrderByCustom(
                        queryParameters.SortBy,
                        queryParameters.SortOrder);
                }
            }

            products = products.Where(prod => prod.Color != null)
                                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                                .Take(queryParameters.Size);


            return Ok(await products.ToArrayAsync());
        }

        catch (Exception)
        {
            throw;
        }

    }




    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        try
        {
            var product =
                         await _dataRepositoryContext.Products.FirstOrDefaultAsync(prod => prod.Id == id);

            if (product == null)
            {
                _logger.LogWarning($"Product with Id : {id} is Not Found");
                return NotFound($"Product with Id : {id} is Not Found");
            }

            return Ok(product);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    [Route("~/api/restauranttest")]
    public async Task<IActionResult> GetRestaurantsById()
    {
        try
        {

            var restaurants =
                await _dataRepositoryContext.Restaurants.Select(prod => prod).ToListAsync();

            return Ok(restaurants);
        }
        catch (Exception)
        {
            throw;
        }

    }


    [HttpGet("{color}")]
    public async Task<IActionResult> GetAllProductsByColor(string color)
    {
        try
        {

            var products = await _dataRepositoryContext.Products.Where(prod => prod.Color!.Equals(color)).ToListAsync();

            return Ok(products);

        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    [Route("~/api/prod")]
    public async Task<IActionResult> GetAllProductsByListPrice()
    {
        try
        {
            var products = await _dataRepositoryContext.Products.Where(prod => prod.ListPrice == null)
                                    .Union(_dataRepositoryContext.Products.Where(prod => prod.Size == "string"))
                                    .OrderByDescending(prod => prod.Id).ToListAsync();

            return Ok(products);
        }
        catch (Exception)
        {
            throw;
        }

    }


    [HttpPost]
    public async Task<IActionResult> AddProduct(Product res)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Bad Request recieved having {ModelState}");
                return BadRequest(ModelState);
            }
            await _dataRepositoryContext.Products.AddAsync(res);

            _dataRepositoryContext.SaveChanges();
            return Ok();
        }

        catch (Exception)
        {
            throw;
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct([FromRoute] int id, Product request)
    {
        try
        {
            var product = await _dataRepositoryContext.Products.FindAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Unable to find the Product having Id : {id}");
                return NotFound();
            }
            product.Name = request.Name;
            product.Color = request.Color;
            product.Price = request.Price;
            product.Size = request.Size;
            product.ListPrice = request.ListPrice;

            await _dataRepositoryContext.SaveChangesAsync();
            return Ok(product);
        }
        catch (Exception)
        {
            throw;
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        try
        {
            var product = await _dataRepositoryContext.Products.FindAsync(id);

            if (product == null)
            {
                _logger.LogWarning($"Unable to find the Product having Id : {id}");
                return NotFound();
            }

            _dataRepositoryContext.Products.Remove(product);

            await _dataRepositoryContext.SaveChangesAsync();

            return Ok(product);
        }
        catch (Exception)
        {
            throw;
        }
    }
}

