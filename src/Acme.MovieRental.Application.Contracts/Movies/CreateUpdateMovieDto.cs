using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.MovieRental.Movies;

public class CreateUpdateMovieDto
{
    [Required]
    [StringLength(128)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public MovieGenre Genre { get; set; } = MovieGenre.Undefined;

    [Required]
    public int YearOfRelease { get; set; }

    [Required]
    public float Price { get; set; }

    [Required]
    public Guid DirectorId { get; set; }
}