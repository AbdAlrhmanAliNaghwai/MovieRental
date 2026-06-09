using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.MovieRental.Directors;

public class Director : AuditedAggregateRoot<Guid>
{
    public string? Name { get; set; }

    public string? Nationality { get; set; }
}