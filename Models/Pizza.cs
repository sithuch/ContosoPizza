
// namespace ContosoPizza.Models;

// public class Pizza
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public bool IsGlutenFree { get; set; }
// }

using System.ComponentModel.DataAnnotations;
namespace ContosoPizza.Models;

public class Pizza
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public bool IsGlutenFree { get; set; }
    
}