using System;
using System.Threading.Tasks;
using Acme.MovieRental.Customers;
using Acme.MovieRental.Directors;
using Acme.MovieRental.Movies;
using Shouldly;
using Xunit;

namespace Acme.MovieRental.Rentals;

public abstract class RentalAppServiceTests<TStartupModule> : MovieRentalApplicationTestBase<TStartupModule>
    where TStartupModule : Volo.Abp.Modularity.IAbpModule
{
    private readonly IRentalAppService _rentalAppService;
    private readonly ICustomerAppService _customerAppService;
    private readonly IMovieAppService _movieAppService;
    private readonly IDirectorAppService _directorAppService;

    protected RentalAppServiceTests()
    {
        _rentalAppService = GetRequiredService<IRentalAppService>();
        _customerAppService = GetRequiredService<ICustomerAppService>();
        _movieAppService = GetRequiredService<IMovieAppService>();
        _directorAppService = GetRequiredService<IDirectorAppService>();
    }

    private async Task<Guid> CreateTestCustomerAsync()
    {
        var customer = await _customerAppService.CreateAsync(new CreateUpdateCustomerDto
        {
            FullName = "Test Customer",
            Email = "test@test.com",
            PhoneNumber = "0791234567"
        });
        return customer.Id;
    }

    private async Task<Guid> CreateTestMovieAsync()
    {
        var director = await _directorAppService.CreateAsync(new CreateUpdateDirectorDto
        {
            Name = "Test Director",
            Nationality = "Test"
        });

        var movie = await _movieAppService.CreateAsync(new CreateUpdateMovieDto
        {
            Title = "Test Movie",
            Genre = MovieGenre.Action,
            YearOfRelease = 2020,
            Price = 10,
            DirectorId = director.Id
        });
        return movie.Id;
    }

    [Fact]
    public async Task Should_Create_A_Valid_Rental()
    {

        var customerId = await CreateTestCustomerAsync();
        var movieId = await CreateTestMovieAsync();


        var rental = await _rentalAppService.CreateAsync(new CreateUpdateRentalDto
        {
            CustomerId = customerId,
            MovieId = movieId,
            DueDate = DateTime.Now.AddDays(3)
        });


        rental.Id.ShouldNotBe(Guid.Empty);
        rental.CustomerId.ShouldBe(customerId);
        rental.MovieId.ShouldBe(movieId);
        rental.IsReturned.ShouldBeFalse();
        rental.CustomerName.ShouldBe("Test Customer");
        rental.MovieTitle.ShouldBe("Test Movie");
    }

    [Fact]
    public async Task Should_Not_Create_Rental_With_Past_Due_Date()
    {
        var customerId = await CreateTestCustomerAsync();
        var movieId = await CreateTestMovieAsync();

        await Assert.ThrowsAsync<DueDateCannotBeInThePastException>(async () =>
        {
            await _rentalAppService.CreateAsync(new CreateUpdateRentalDto
            {
                CustomerId = customerId,
                MovieId = movieId,
                DueDate = DateTime.Now.AddDays(-1)
            });
        });
    }

    [Fact]
    public async Task Should_Not_Create_Rental_With_Todays_Date_As_Due_Date()
    {
        var customerId = await CreateTestCustomerAsync();
        var movieId = await CreateTestMovieAsync();


        await Assert.ThrowsAsync<DueDateCannotBeInThePastException>(async () =>
        {
            await _rentalAppService.CreateAsync(new CreateUpdateRentalDto
            {
                CustomerId = customerId,
                MovieId = movieId,
                DueDate = DateTime.Now.Date
            });
        });
    }

    [Fact]
    public async Task Should_Mark_Rental_As_Returned()
    {
        var customerId = await CreateTestCustomerAsync();
        var movieId = await CreateTestMovieAsync();

        var rental = await _rentalAppService.CreateAsync(new CreateUpdateRentalDto
        {
            CustomerId = customerId,
            MovieId = movieId,
            DueDate = DateTime.Now.AddDays(3)
        });


        var returned = await _rentalAppService.MarkAsReturnedAsync(rental.Id);


        returned.IsReturned.ShouldBeTrue();
    }

    [Fact]
    public async Task Should_Not_Delete_An_Active_Rental()
    {
        var customerId = await CreateTestCustomerAsync();
        var movieId = await CreateTestMovieAsync();

        var rental = await _rentalAppService.CreateAsync(new CreateUpdateRentalDto
        {
            CustomerId = customerId,
            MovieId = movieId,
            DueDate = DateTime.Now.AddDays(3)
        });


        await Assert.ThrowsAsync<CannotDeleteActiveRentalException>(async () =>
        {
            await _rentalAppService.DeleteAsync(rental.Id);
        });
    }

    [Fact]
    public async Task Should_Delete_A_Returned_Rental()
    {
        var customerId = await CreateTestCustomerAsync();
        var movieId = await CreateTestMovieAsync();

        var rental = await _rentalAppService.CreateAsync(new CreateUpdateRentalDto
        {
            CustomerId = customerId,
            MovieId = movieId,
            DueDate = DateTime.Now.AddDays(3)
        });

        await _rentalAppService.MarkAsReturnedAsync(rental.Id);


        await _rentalAppService.DeleteAsync(rental.Id);


        await Assert.ThrowsAsync<Volo.Abp.Domain.Entities.EntityNotFoundException>(async () =>
        {
            await _rentalAppService.GetAsync(rental.Id);
        });
    }

    [Fact]
    public async Task Multiple_Customers_Should_Be_Able_To_Rent_The_Same_Movie()
    {
        var movieId = await CreateTestMovieAsync();

        var customer1 = await _customerAppService.CreateAsync(new CreateUpdateCustomerDto
        {
            FullName = "Customer One",
            Email = "one@test.com",
            PhoneNumber = "0791111111"
        });

        var customer2 = await _customerAppService.CreateAsync(new CreateUpdateCustomerDto
        {
            FullName = "Customer Two",
            Email = "two@test.com",
            PhoneNumber = "0792222222"
        });

        var rental1 = await _rentalAppService.CreateAsync(new CreateUpdateRentalDto
        {
            CustomerId = customer1.Id,
            MovieId = movieId,
            DueDate = DateTime.Now.AddDays(3)
        });

        var rental2 = await _rentalAppService.CreateAsync(new CreateUpdateRentalDto
        {
            CustomerId = customer2.Id,
            MovieId = movieId,
            DueDate = DateTime.Now.AddDays(5)
        });

        rental1.Id.ShouldNotBe(rental2.Id);
        rental1.MovieId.ShouldBe(rental2.MovieId);
    }
}