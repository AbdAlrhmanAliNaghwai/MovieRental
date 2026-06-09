using System;
using System.Threading.Tasks;
using Acme.MovieRental.Directors;
using Acme.MovieRental.Movies;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.MovieRental;

public class MovieRentalDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Movie, Guid> _movieRepository;
    private readonly IRepository<Director, Guid> _directorRepository;

    public MovieRentalDataSeederContributor(
        IRepository<Movie, Guid> movieRepository,
        IRepository<Director, Guid> directorRepository)
    {
        _movieRepository = movieRepository;
        _directorRepository = directorRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _directorRepository.GetCountAsync() <= 0)
        {
            var michaelBay = await _directorRepository.InsertAsync(
                new Director { Name = "Michael Bay", Nationality = "American" },
                autoSave: true
            );

            var dennisdugan = await _directorRepository.InsertAsync(
                new Director { Name = "Dennis Dugan", Nationality = "American" },
                autoSave: true
            );

            var tomHolland = await _directorRepository.InsertAsync(
                new Director { Name = "Tom Holland", Nationality = "American" },
                autoSave: true
            );

            if (await _movieRepository.GetCountAsync() <= 0)
            {
                await _movieRepository.InsertAsync(new Movie
                {
                    Title = "Transformers",
                    Genre = MovieGenre.Action,
                    YearOfRelease = 2007,
                    Price = 15,
                    DirectorId = michaelBay.Id
                }, autoSave: true);

                await _movieRepository.InsertAsync(new Movie
                {
                    Title = "Grown Ups",
                    Genre = MovieGenre.Comedy,
                    YearOfRelease = 2010,
                    Price = 10,
                    DirectorId = dennisdugan.Id
                }, autoSave: true);

                await _movieRepository.InsertAsync(new Movie
                {
                    Title = "Chucky",
                    Genre = MovieGenre.Horror,
                    YearOfRelease = 1988,
                    Price = 20,
                    DirectorId = tomHolland.Id
                }, autoSave: true);
            }
        }
    }
}