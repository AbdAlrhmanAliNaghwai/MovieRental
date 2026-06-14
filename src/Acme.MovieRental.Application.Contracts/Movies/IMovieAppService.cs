using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.MovieRental.Movies;

public interface IMovieAppService : IApplicationService
{
    Task<PagedResultDto<MovieDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<MovieDto> GetAsync(Guid id);
    Task<MovieDto> CreateAsync(CreateUpdateMovieDto input);
    Task DeleteAsync(Guid id);
}