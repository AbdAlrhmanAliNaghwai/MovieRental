using System;
using Volo.Abp.Application.Dtos;

namespace Acme.MovieRental.Directors;

public class DirectorDto : AuditedEntityDto<Guid>
{
    public string? Name { get; set; }

    public string? Nationality { get; set; }
}