import type { MovieGenre } from './movie-genre.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateMovieDto {
  title: string;
  genre: MovieGenre;
  yearOfRelease: number;
  price: number;
  directorId: string;
}

export interface MovieDto extends AuditedEntityDto<string> {
  title?: string | null;
  genre?: MovieGenre;
  yearOfRelease?: number;
  price?: number;
  directorId?: string;
  directorName?: string | null;
}
