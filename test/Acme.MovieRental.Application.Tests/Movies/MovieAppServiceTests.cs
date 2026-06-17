using System;
using System.Threading.Tasks;
using Acme.MovieRental.Directors;
using Shouldly;
using Volo.Abp.Validation;
using Xunit;

namespace Acme.MovieRental.Movies;

public abstract class MovieAppServiceTests<TStartupModule> : MovieRentalApplicationTestBase<TStartupModule>
    where TStartupModule : Volo.Abp.Modularity.IAbpModule
{
    private readonly IMovieAppService _movieAppService;
    private readonly IDirectorAppService _directorAppService;

    protected MovieAppServiceTests()
    {
        _movieAppService = GetRequiredService<IMovieAppService>();
        _directorAppService = GetRequiredService<IDirectorAppService>();
    }

    private async Task<Guid> CreateTestDirectorAsync()
    {
        var director = await _directorAppService.CreateAsync(new CreateUpdateDirectorDto
        {
            Name = "Test Director",
            Nationality = "Test"
        });
        return director.Id;
    }

    [Fact]
    public async Task Should_Create_A_Valid_Movie()
    {
        var directorId = await CreateTestDirectorAsync();

        var movie = await _movieAppService.CreateAsync(new CreateUpdateMovieDto
        {
            Title = "Inception",
            Genre = MovieGenre.SciFi,
            YearOfRelease = 2010,
            Price = 12,
            DirectorId = directorId
        });

        movie.Id.ShouldNotBe(Guid.Empty);
        movie.Title.ShouldBe("Inception");
        movie.Price.ShouldBe(12);
    }

    [Fact]
    public async Task Should_Not_Create_Movie_With_Zero_Price()
    {
        var directorId = await CreateTestDirectorAsync();

        var exception = await Assert.ThrowsAsync<MoviePriceCannotBeLessThanZero>(async () =>
        {
            await _movieAppService.CreateAsync(new CreateUpdateMovieDto
            {
                Title = "Free Movie",
                Genre = MovieGenre.Comedy,
                YearOfRelease = 2020,
                Price = 0,
                DirectorId = directorId
            });
        });

        exception.ShouldNotBeNull();
    }

    [Fact]
    public async Task Should_Not_Create_Movie_With_Negative_Price()
    {
        var directorId = await CreateTestDirectorAsync();

        await Assert.ThrowsAsync<MoviePriceCannotBeLessThanZero>(async () =>
        {
            await _movieAppService.CreateAsync(new CreateUpdateMovieDto
            {
                Title = "Negative Price Movie",
                Genre = MovieGenre.Drama,
                YearOfRelease = 2020,
                Price = -5,
                DirectorId = directorId
            });
        });
    }

    [Fact]
    public async Task Should_Not_Create_Movie_With_Invalid_Release_Year()
    {
        var directorId = await CreateTestDirectorAsync();

        await Assert.ThrowsAsync<ReleaseYearCannotBeLessThanZero>(async () =>
        {
            await _movieAppService.CreateAsync(new CreateUpdateMovieDto
            {
                Title = "Old Movie",
                Genre = MovieGenre.Drama,
                YearOfRelease = 0,
                Price = 10,
                DirectorId = directorId
            });
        });
    }
}