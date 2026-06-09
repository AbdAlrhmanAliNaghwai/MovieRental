using System;
using Volo.Abp.Application.Dtos;

namespace Acme.MovieRental.Movies;

public class MovieDto : AuditedEntityDto<Guid>
{
    public string? Title { get; set; }

    public MovieGenre Genre { get; set; }

    public int YearOfRelease { get; set; }

    public float Price { get; set; }

    public Guid DirectorId { get; set; }

    public string? DirectorName { get; set; }
}