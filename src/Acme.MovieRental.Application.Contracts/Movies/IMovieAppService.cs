using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.MovieRental.Movies;

public interface IMovieAppService : ICrudAppService<MovieDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMovieDto>
{
    
}