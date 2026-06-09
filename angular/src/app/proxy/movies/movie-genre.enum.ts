import { mapEnumToOptions } from '@abp/ng.core';

export enum MovieGenre {
  Undefined = 0,
  Horror = 1,
  Action = 2,
  Drama = 3,
  Comedy = 4,
  Si_Fi = 5,
}

export const movieGenreOptions = mapEnumToOptions(MovieGenre);
