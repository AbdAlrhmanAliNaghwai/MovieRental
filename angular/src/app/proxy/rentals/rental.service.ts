import type { CreateUpdateRentalDto, RentalDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RentalService {
  private restService = inject(RestService);
  apiName = 'Default';

  create = (input: CreateUpdateRentalDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RentalDto>({
      method: 'POST',
      url: '/api/app/rental',
      body: input,
    },
    { apiName: this.apiName, ...config });

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/rental/${id}`,
    },
    { apiName: this.apiName, ...config });

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RentalDto>({
      method: 'GET',
      url: `/api/app/rental/${id}`,
    },
    { apiName: this.apiName, ...config });

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<RentalDto>>({
      method: 'GET',
      url: '/api/app/rental',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName, ...config });

  markAsReturned = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RentalDto>({
      method: 'POST',
      url: `/api/app/rental/${id}/mark-as-returned`,
    },
    { apiName: this.apiName, ...config });
}