using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.MovieRental.Directors;

public class Director : AuditedAggregateRoot<Guid>
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Nationality { get; set; }
}