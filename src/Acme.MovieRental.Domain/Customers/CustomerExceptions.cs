using Volo.Abp;

namespace Acme.MovieRental.Customers;

public class ActiveRentalForCustomerException : BusinessException
{
    public ActiveRentalForCustomerException() : base(MovieRentalDomainErrorCodes.ActiveRentalForCustomer)
    {
        
    }
}