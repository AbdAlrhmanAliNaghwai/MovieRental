using System;
using System.Collections.Generic;
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
        var rentals = await _rentalRepository.GetListAsync();
        var totalCount = rentals.Count;
        var dtos = new List<RentalDto>();

        foreach (var rental in rentals)
        {
            await _rentalRepository.EnsurePropertyLoadedAsync(rental, r => r.Customer);
            await _rentalRepository.EnsurePropertyLoadedAsync(rental, r => r.Movie);
            dtos.Add(ObjectMapper.Map<Rental, RentalDto>(rental));
        }

        return new PagedResultDto<RentalDto>(totalCount, dtos);
    }

    public async Task<RentalDto> GetAsync(Guid id)
    {

        var rental = await _rentalRepository.GetAsync(id);
        await _rentalRepository.EnsurePropertyLoadedAsync(rental, r => r.Customer);
        await _rentalRepository.EnsurePropertyLoadedAsync(rental, r => r.Movie);
        return ObjectMapper.Map<Rental, RentalDto>(rental);
    }

    public async Task<RentalDto> CreateAsync(CreateUpdateRentalDto input)
    {
        if (input.DueDate.Date <= DateTime.Now.Date)
        {
            throw new DueDateCannotBeInThePastException();
        }


        var rental = new Rental
        {
            CustomerId = input.CustomerId,
            MovieId = input.MovieId,
            RentalDate = DateTime.Now,
            DueDate = input.DueDate
        };

        var created = await _rentalRepository.InsertAsync(rental, autoSave: true);
        await _rentalRepository.EnsurePropertyLoadedAsync(created, r => r.Customer);
        await _rentalRepository.EnsurePropertyLoadedAsync(created, r => r.Movie);
        return ObjectMapper.Map<Rental, RentalDto>(created);
    }

    public async Task<RentalDto> MarkAsReturnedAsync(Guid id)
    {
        var rental = await _rentalRepository.GetAsync(id);
        rental.ReturnDate = DateTime.Now;
        var updated = await _rentalRepository.UpdateAsync(rental, autoSave: true);
        await _rentalRepository.EnsurePropertyLoadedAsync(updated, r => r.Customer);
        await _rentalRepository.EnsurePropertyLoadedAsync(updated, r => r.Movie);
        return ObjectMapper.Map<Rental, RentalDto>(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var rental = await _rentalRepository.GetAsync(id);
        if (rental.ReturnDate == null)
        {
            throw new CannotDeleteActiveRentalException();
        }
        await _rentalRepository.DeleteAsync(id);
    }
}