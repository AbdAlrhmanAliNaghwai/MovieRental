namespace Acme.MovieRental;

public static class MovieRentalDomainErrorCodes
{
    public const string DueDateCannotBeInThePast = "MovieRental:DueDateCannotBeInThePast";
    public const string CannotDeleteActiveRental = "MovieRental:CannotDeleteActiveRental";
    public const string MoviePriceCannotBeLessThanZero = "MovieRental:MoviePriceCannotBeLessThanZero";
}
