using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.MovieRental.Directors;

public class DirectorAppService : CrudAppService<Director, DirectorDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateDirectorDto>, IDirectorAppService
{
    public DirectorAppService(IRepository<Director, Guid> repository) : base(repository)
    {
        
    }
}