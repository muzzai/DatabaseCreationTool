using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities;


public class Category : IPrintable
{
    public int CategoryId { get; set; }
    [Required] 
    public string Name { get; set; }

    public static List<string> GetColumnsNames()
    {
        return new List<string>() { "Id", "Name" };
    }

    public List<string> GetColumnsValues()
    {
        return new List<string>() { CategoryId.ToString(), Name };
    }
}