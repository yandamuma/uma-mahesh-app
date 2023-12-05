using UmaMahesh_BackApp.Entities.Custom;

namespace UmaMahesh_BackApp.Entities.Products;

public class ProductQueryParameters : QueryParameters
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public string SearchTerm { get; set; } = string.Empty;









}
