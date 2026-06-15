using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.MovieRental.Customers;
using Acme.MovieRental.Movies;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.MovieRental.Rentals;

public class RentalAppService : ApplicationService, IRentalAppService
{
    private readonly IRepository<Rental, Guid> _rentalRepository;
    private readonly IRepository<Customer, Guid> _customerRepository;
    private readonly IRepository<Movie, Guid> _movieRepository;

    public RentalAppService(
        IRepository<Rental, Guid> rentalRepository,
        IRepository<Customer, Guid> customerRepository,
        IRepository<Movie, Guid> movieRepository)
    {
        _rentalRepository = rentalRepository;
        _customerRepository = customerRepository;
        _movieRepository = movieRepository;
    }

    public async Task<PagedResultDto<RentalDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _rentalRepository.WithDetailsAsync(r => r.Customer, r => r.Movie);

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        queryable = queryable
            .OrderByDescending(r => r.RentalDate)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var rentals = await AsyncExecuter.ToListAsync(queryable);

        var dtos = rentals.Select(ObjectMapper.Map<Rental, RentalDto>).ToList();

        return new PagedResultDto<RentalDto>(totalCount, dtos);
    }

    public async Task<RentalDto> GetAsync(Guid id)
    {
        var queryable = await _rentalRepository.WithDetailsAsync(r => r.Customer, r => r.Movie);
        var rental = await AsyncExecuter.FirstOrDefaultAsync(queryable, r => r.Id == id);

        if (rental == null)
            throw new Volo.Abp.Domain.Entities.EntityNotFoundException(typeof(Rental), id);

        return ObjectMapper.Map<Rental, RentalDto>(rental);
    }

    public async Task<RentalDto> CreateAsync(CreateUpdateRentalDto input)
    {
        if (input.DueDate.Date <= DateTime.Now.Date)
            throw new DueDateCannotBeInThePastException();

        var rental = new Rental
        {
            CustomerId = input.CustomerId,
            MovieId = input.MovieId,
            RentalDate = DateTime.Now,
            DueDate = input.DueDate
        };

        var create = await _rentalRepository.InsertAsync(rental, autoSave: true);

        var queryable = await _rentalRepository.WithDetailsAsync(r => r.Customer, r => r.Movie);
        var created = await AsyncExecuter.FirstOrDefaultAsync(queryable, r => r.Id == create.Id);

        return ObjectMapper.Map<Rental, RentalDto>(created);
    }

    public async Task<RentalDto> MarkAsReturnedAsync(Guid id)
    {
        var rental = await _rentalRepository.GetAsync(id);
        rental.ReturnDate = DateTime.Now;
        await _rentalRepository.UpdateAsync(rental, autoSave: true);

        var queryable = await _rentalRepository.WithDetailsAsync(r => r.Customer, r => r.Movie);
        var updated = await AsyncExecuter.FirstOrDefaultAsync(queryable, r => r.Id == id);

        return ObjectMapper.Map<Rental, RentalDto>(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var rental = await _rentalRepository.GetAsync(id);

        if (rental.ReturnDate == null)
            throw new CannotDeleteActiveRentalException();

        await _rentalRepository.DeleteAsync(id);
    }
}