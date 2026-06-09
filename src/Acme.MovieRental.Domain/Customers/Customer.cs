using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.MovieRental.Customers;

public class Customer : AuditedAggregateRoot<Guid>
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}