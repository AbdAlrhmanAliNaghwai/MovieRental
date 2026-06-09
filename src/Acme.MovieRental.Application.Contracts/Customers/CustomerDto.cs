using System;
using Volo.Abp.Application.Dtos;

namespace Acme.MovieRental.Customers;

public class CustomerDto : AuditedEntityDto<Guid>
{
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}