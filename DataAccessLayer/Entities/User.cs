using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class User : IPrintable
{
    public int UserId { get; set; }
    [Required]
    public string FullName { get; set; }

    public static List<string> GetColumnsNames()
    {
        return new List<string>() { "Id", "Full Name" };
    }

    public List<string> GetColumnsValues()
    {
        return new List<string>() { UserId.ToString(), FullName };
    }
}