using System;
using System.ComponentModel.DataAnnotations;
using Acme.MovieRental.Customers;
using Acme.MovieRental.Movies;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.MovieRental.Rentals;

public class Rental : AuditedAggregateRoot<Guid>
{
    public Guid CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public Guid MovieId { get; set; }

    public Movie? Movie { get; set; }

    public DateTime RentalDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public bool IsReturned => ReturnDate.HasValue;
}
