using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.MovieRental.Directors;

public interface IDirectorAppService : ICrudAppService<DirectorDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateDirectorDto>
{
    
}