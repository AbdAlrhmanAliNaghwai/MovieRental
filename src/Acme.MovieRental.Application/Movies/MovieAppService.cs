using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.MovieRental.Directors;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.MovieRental.Movies;

public class MovieAppService : ApplicationService, IMovieAppService
{
    private readonly IRepository<Movie, Guid> _movieRepository;
    private readonly IRepository<Director, Guid> _directorRepository;

    public MovieAppService(
        IRepository<Movie, Guid> movieRepository,
        IRepository<Director, Guid> directorRepository)
    {
        _movieRepository = movieRepository;
        _directorRepository = directorRepository;
    }

    public async Task<PagedResultDto<MovieDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _movieRepository.WithDetailsAsync(m => m.Director);

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        queryable = queryable
            .OrderBy(m => m.Title)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var movies = await AsyncExecuter.ToListAsync(queryable);

        var dtos = movies.Select(ObjectMapper.Map<Movie, MovieDto>).ToList();

        return new PagedResultDto<MovieDto>(totalCount, dtos);
    }

    public async Task<MovieDto> GetAsync(Guid id)
    {
        var queryable = await _movieRepository.WithDetailsAsync(m => m.Director);
        var movie = await AsyncExecuter.FirstOrDefaultAsync(queryable, m => m.Id == id);

        if (movie == null)
            throw new Volo.Abp.Domain.Entities.EntityNotFoundException(typeof(Movie), id);

        return ObjectMapper.Map<Movie, MovieDto>(movie);
    }

    public async Task<MovieDto> CreateAsync(CreateUpdateMovieDto input)
    {
        if (input.YearOfRelease <= 0)
            throw new ReleaseYearCannotBeLessThanZero();

        if (input.Price <= 0)
            throw new MoviePriceCannotBeLessThanZero();

        var movie = new Movie
        {
            Title = input.Title,
            Genre = input.Genre,
            DirectorId = input.DirectorId,
            YearOfRelease = input.YearOfRelease,
            Price = input.Price
        };

        var create = await _movieRepository.InsertAsync(movie, autoSave: true);

        var queryable = await _movieRepository.WithDetailsAsync(m => m.Director);
        var created = await AsyncExecuter.FirstOrDefaultAsync(queryable, m => m.Id == create.Id);

        return ObjectMapper.Map<Movie, MovieDto>(created);
    }

    public async Task<MovieDto> UpdateAsync(Guid id, CreateUpdateMovieDto input)
    {
        if (input.YearOfRelease <= 0)
            throw new ReleaseYearCannotBeLessThanZero();

        if (input.Price <= 0)
            throw new MoviePriceCannotBeLessThanZero();

        var movie = await _movieRepository.GetAsync(id);
        movie.Title = input.Title;
        movie.Genre = input.Genre;
        movie.YearOfRelease = input.YearOfRelease;
        movie.Price = input.Price;
        movie.DirectorId = input.DirectorId;

        await _movieRepository.UpdateAsync(movie, autoSave: true);

        var queryable = await _movieRepository.WithDetailsAsync(m => m.Director);
        var updated = await AsyncExecuter.FirstOrDefaultAsync(queryable, m => m.Id == id);

        return ObjectMapper.Map<Movie, MovieDto>(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var movie = await _movieRepository.GetAsync(id);
        await _movieRepository.DeleteAsync(movie);
    }
}
// {
//     public MovieAppService(IRepository<Movie, Guid> repository) : base(repository)
//     {
//     }

//     public override async Task<MovieDto> GetAsync(Guid id)
//     {
//         await Repository.EnsurePropertyLoadedAsync(
//             await Repository.GetAsync(id), m => m.Director);
//         return await base.GetAsync(id);
//     }

//     public override async Task<MovieDto> CreateAsync(CreateUpdateMovieDto input)
//     {
//         if (input.YearOfRelease <= 0)
//         {
//             throw new ReleaseYearCannotBeLessThanZero();
//         }

//         if (input.Price <= 0)
//         {
//             throw new MoviePriceCannotBeLessThanZero();
//         }

//         return await base.CreateAsync(input);
//     }
//     public override async Task<MovieDto> UpdateAsync(Guid id, CreateUpdateMovieDto input)
//     {
//         if (input.YearOfRelease <= 0)
//         {
//             throw new ReleaseYearCannotBeLessThanZero();
//         }

//         if (input.Price <= 0)
//         {
//             throw new MoviePriceCannotBeLessThanZero();
//         }
//         return await base.UpdateAsync(id, input);
//     }

//     public override async Task<PagedResultDto<MovieDto>> GetListAsync(
//         PagedAndSortedResultRequestDto input)
//     {
//         var movies = await Repository.GetListAsync(includeDetails: true);
//         var totalCount = movies.Count;
//         var dtos = new List<MovieDto>();
//         foreach (var movie in movies)
//         {
//             await Repository.EnsurePropertyLoadedAsync(movie, m => m.Director);
//             dtos.Add(ObjectMapper.Map<Movie, MovieDto>(movie));
//         }
//         return new PagedResultDto<MovieDto>(totalCount, dtos);
//     }
// }