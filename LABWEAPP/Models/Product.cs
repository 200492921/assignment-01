using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabWebApp.Models;

public class Product
{
    [Key] public int Id { get; set; }

    [Required] public required string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public required string Description { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Product product) return false;
        return product.Id == Id &&
               product.Name == Name &&
               product.Price == Price &&
               product.Description == Description;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Price, Description);
    }
}