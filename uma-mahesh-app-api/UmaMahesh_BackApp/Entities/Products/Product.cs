


using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace UmaMahesh_BackApp.Entities.Products
{

    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? Price { get; set; }
        public string? Color { get; set; } = string.Empty;
        public int? ListPrice { get; set; }
        public string? Size { get; set; }
    }


}
