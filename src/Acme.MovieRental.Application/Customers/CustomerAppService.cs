using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.MovieRental.Rentals;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.MovieRental.Customers;

public class CustomerAppService : CrudAppService<Customer, CustomerDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCustomerDto>, ICustomerAppService
{
    private readonly IRepository<Rental, Guid> _rentalRepository;

    public CustomerAppService(
        IRepository<Customer, Guid> repository,
        IRepository<Rental, Guid> rentalRepository) : base(repository)
    {
        _rentalRepository = rentalRepository;
    }

    public override async Task DeleteAsync(Guid id)
    {
        var hasActiveRental = await AsyncExecuter.AnyAsync(
            (await _rentalRepository.GetQueryableAsync())
                .Where(r => r.CustomerId == id && r.ReturnDate == null)
        );

        if (hasActiveRental)
        {
            throw new ActiveRentalForCustomerException();
        }

        await base.DeleteAsync(id);
    }
}