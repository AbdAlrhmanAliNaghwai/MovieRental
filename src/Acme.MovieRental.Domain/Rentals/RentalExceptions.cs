using Volo.Abp;

namespace Acme.MovieRental.Rentals;

public class DueDateCannotBeInThePastException : BusinessException
{
    public DueDateCannotBeInThePastException() : base(MovieRentalDomainErrorCodes.DueDateCannotBeInThePast)
    {

    }
}

public class CannotDeleteActiveRentalException : BusinessException
{
    public CannotDeleteActiveRentalException()
        : base(MovieRentalDomainErrorCodes.CannotDeleteActiveRental)
    {

    }
}
