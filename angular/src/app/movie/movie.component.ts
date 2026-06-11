import { ListService, PagedResultDto, LocalizationService, CoreModule } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';
import { MovieService, MovieDto } from '../proxy/movies';
import { DirectorService, DirectorDto } from '../proxy/directors';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {
  ConfirmationService,
  Confirmation,
  ModalComponent,
  NgxDatatableDefaultDirective,
  NgxDatatableListDirective,
} from '@abp/ng.theme.shared';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    CoreModule,
    ModalComponent,
    NgxDatatableModule,
    NgxDatatableDefaultDirective,
    NgxDatatableListDirective,
    ReactiveFormsModule,
    NgbDropdownModule,
  ],
  providers: [ListService],
})
export class MovieComponent implements OnInit {
  movies = { items: [], totalCount: 0 } as PagedResultDto<MovieDto>;
  directors: DirectorDto[] = [];

  isModalOpen = false;
  isEditMode = false;
  selectedMovieId: string | null = null;
  form: FormGroup;

  genres = [
    { value: 1, key: '::Enum:MovieGenre.0' },
    { value: 2, key: '::Enum:MovieGenre.1' },
    { value: 3, key: '::Enum:MovieGenre.2' },
    { value: 4, key: '::Enum:MovieGenre.3' },
    { value: 5, key: '::Enum:MovieGenre.4' },
    { value: 6, key: '::Enum:MovieGenre.5' },
  ];

  getGenreLabel(value: number): string {
    const genre = this.genres.find(g => g.value === value);
    return genre ? this.localization.instant(genre.key) : '';
  }

  public readonly list = inject(ListService);
  private readonly movieService = inject(MovieService);
  private readonly directorService = inject(DirectorService);
  private readonly fb = inject(FormBuilder);
  private readonly confirmation = inject(ConfirmationService);
  private readonly localization = inject(LocalizationService);

  ngOnInit() {
    const streamCreator = query => this.movieService.getList(query);
    this.list.hookToQuery(streamCreator).subscribe(response => {
      this.movies = response;
    });

    this.directorService.getList({ maxResultCount: 100, skipCount: 0 }).subscribe(response => {
      this.directors = response.items;
    });
  }

  openModal(movie?: MovieDto) {
    this.isEditMode = !!movie;
    this.selectedMovieId = movie?.id ?? null;
    this.form = this.fb.group({
      title: [movie?.title ?? '', Validators.required],
      genre: [movie?.genre ?? 0, Validators.required],
      yearOfRelease: [movie?.yearOfRelease ?? new Date().getFullYear(), Validators.required],
      price: [movie?.price ?? 0, Validators.required],
      directorId: [movie?.directorId ?? '', Validators.required],
    });
    this.isModalOpen = true;
  }

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const request = this.isEditMode
      ? this.movieService.update(this.selectedMovieId, this.form.value)
      : this.movieService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.list.get();
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.movieService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
}
