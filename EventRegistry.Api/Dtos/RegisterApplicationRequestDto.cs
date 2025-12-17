using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventRegistry.Api.Dtos;

public class RegisterApplicationRequestDto
{
    [Required(ErrorMessage = "Application name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    [Description("The unique name for the application")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [Description("An optional description of the application's purpose")]
    public string? Description { get; set; }
}
