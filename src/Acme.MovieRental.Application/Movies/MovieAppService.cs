using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.MovieRental.Movies;

public class MovieAppService : CrudAppService<Movie, MovieDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMovieDto>,
IMovieAppService
{
    public MovieAppService(IRepository<Movie, Guid> repository) : base(repository)
    {
    }

    public override async Task<MovieDto> GetAsync(Guid id)
    {
        await Repository.EnsurePropertyLoadedAsync(
            await Repository.GetAsync(id), m => m.Director);
        return await base.GetAsync(id);
    }

    public override async Task<PagedResultDto<MovieDto>> GetListAsync(
        PagedAndSortedResultRequestDto input)
    {
        var movies = await Repository.GetListAsync(includeDetails: true);
        var totalCount = movies.Count;
        var dtos = new List<MovieDto>();
        foreach (var movie in movies)
        {
            await Repository.EnsurePropertyLoadedAsync(movie, m => m.Director);
            dtos.Add(ObjectMapper.Map<Movie, MovieDto>(movie));
        }
        return new PagedResultDto<MovieDto>(totalCount, dtos);
    }
}