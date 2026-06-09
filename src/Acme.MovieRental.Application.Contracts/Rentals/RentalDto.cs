using System;
using Volo.Abp.Application.Dtos;

namespace Acme.MovieRental.Rentals;

public class RentalDto : AuditedEntityDto<Guid>
{
    public Guid CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public Guid MovieId { get; set; }

    public string? MovieTitle { get; set; }

    public DateTime RentalDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public bool IsReturned { get; set; }
}