using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.MovieRental.Directors;

public class CreateUpdateDirectorDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(128)]
    public string Nationality { get; set; } = string.Empty;
}