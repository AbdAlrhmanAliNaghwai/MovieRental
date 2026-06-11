using Volo.Abp;

namespace Acme.MovieRental.Movies;

public class MoviePriceCannotBeLessThanZero : BusinessException
{
    public MoviePriceCannotBeLessThanZero() : base(MovieRentalDomainErrorCodes.MoviePriceCannotBeLessThanZero)
    {
        
    }
}