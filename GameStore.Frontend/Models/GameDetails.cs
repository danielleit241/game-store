using System.ComponentModel.DataAnnotations;

namespace GameStore.Frontend.Models;

public class GameDetails
{
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "The Genre field is required.")]
    public string? GenreId { get; set; }
    [Range(10, 100, ErrorMessage = "Price must be between 10 and 100.")]
    public decimal Price { get; set; }
    public DateOnly ReseleaseDate { get; set; }
}
