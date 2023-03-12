using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DataAccessLayer.Entities;

public class Product : IPrintable
{
    public int ProductId { get; set; }
    [Required]
    public string Name { get; set; }
    
    public int OwnerId { get; set; }
    public User Owner { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public decimal Price { get; set; } = 0;
    public static List<string> GetColumnsNames()
    {
        return new List<string>() { "ID", "Name", "Owner ID", "Category ID", "Price" };
    }

    public List<string> GetColumnsValues()
    {
        return new List<string>()
            { ProductId.ToString(), Name, OwnerId.ToString(), CategoryId.ToString(), Price.ToString(CultureInfo.InvariantCulture) };
    }
}