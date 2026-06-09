using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.MovieRental.Rentals;

public interface IRentalAppService : IApplicationService
{
    Task<PagedResultDto<RentalDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<RentalDto> GetAsync(Guid id);
    Task<RentalDto> CreateAsync(CreateUpdateRentalDto input);
    Task<RentalDto> MarkAsReturnedAsync(Guid id);
    Task DeleteAsync(Guid id);
}