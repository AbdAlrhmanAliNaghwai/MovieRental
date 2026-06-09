import type { CreateUpdateDirectorDto, DirectorDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DirectorService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateUpdateDirectorDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DirectorDto>({
      method: 'POST',
      url: '/api/app/director',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/director/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DirectorDto>({
      method: 'GET',
      url: `/api/app/director/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DirectorDto>>({
      method: 'GET',
      url: '/api/app/director',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateDirectorDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DirectorDto>({
      method: 'PUT',
      url: `/api/app/director/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}