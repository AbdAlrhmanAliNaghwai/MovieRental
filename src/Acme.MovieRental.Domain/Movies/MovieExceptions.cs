using Volo.Abp;

namespace Acme.MovieRental.Movies;

public class MoviePriceCannotBeLessThanZero : BusinessException
{
    public MoviePriceCannotBeLessThanZero() : base(MovieRentalDomainErrorCodes.MoviePriceCannotBeLessThanZero)
    {
        
    }
}

public class ReleaseYearCannotBeLessThanZero : BusinessException
{
    public ReleaseYearCannotBeLessThanZero() : base(MovieRentalDomainErrorCodes.ReleaseYearCannotBeLessThanZero)
    {
        
    }
}
public class CannotDeleteActiveMovieRentalException : BusinessException
{
    public CannotDeleteActiveMovieRentalException() : base(MovieRentalDomainErrorCodes.CannotDeleteActiveMovieRental)
    {}
}