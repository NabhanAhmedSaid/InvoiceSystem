using System.ComponentModel.DataAnnotations;

namespace invoices.Models;

public class ContactViewModel
{

    [Required]
    [StringLength(150)]
    public string? Name { get; set; }
[Required]
[StringLength(10)]
    public string Phone { get; set; } = null!;

    public string? City { get; set; }

    public string? Governorate { get; set; }
}