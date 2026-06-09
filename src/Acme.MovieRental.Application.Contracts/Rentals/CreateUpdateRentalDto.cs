using System;

namespace Acme.MovieRental.Rentals;

public class CreateUpdateRentalDto
{
    public Guid CustomerId { get; set; }

    public Guid MovieId { get; set; }

    public DateTime DueDate { get; set; }
}