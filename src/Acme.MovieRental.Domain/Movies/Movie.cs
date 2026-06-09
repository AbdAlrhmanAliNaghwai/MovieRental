using System;
using Acme.MovieRental.Directors;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.MovieRental.Movies;

public class Movie : AuditedAggregateRoot<Guid>
{
    public string? Title { get; set; }

    public MovieGenre Genre { get; set; }

    public int YearOfRelease { get; set; }

    public float Price { get; set; }

    public Guid DirectorId { get; set; }

    public Director? Director { get; set; }
}