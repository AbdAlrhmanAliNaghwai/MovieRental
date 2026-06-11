using System.ComponentModel.DataAnnotations;

namespace Acme.MovieRental.Customers;

public class CreateUpdateCustomerDto
{
    [Required]
    [StringLength(64)]
    public string? FullName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
}