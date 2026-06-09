import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateCustomerDto {
  fullName?: string | null;
  email?: string | null;
  phoneNumber?: string | null;
}

export interface CustomerDto extends AuditedEntityDto<string> {
  fullName?: string | null;
  email?: string | null;
  phoneNumber?: string | null;
}
