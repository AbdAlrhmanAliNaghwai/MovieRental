import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateRentalDto {
  customerId?: string;
  movieId?: string;
  dueDate?: string;
}

export interface RentalDto extends AuditedEntityDto<string> {
  customerId?: string;
  customerName?: string | null;
  movieId?: string;
  movieTitle?: string | null;
  rentalDate?: string;
  dueDate?: string;
  returnDate?: string | null;
  isReturned?: boolean;
}
