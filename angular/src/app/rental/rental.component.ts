import { ListService, PagedResultDto, LocalizationService, CoreModule } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';
import { RentalService, RentalDto } from '../proxy/rentals';
import { CustomerService, CustomerDto } from '../proxy/customers';
import { MovieService, MovieDto } from '../proxy/movies';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {
  ConfirmationService,
  Confirmation,
  ModalComponent,
  NgxDatatableDefaultDirective,
  NgxDatatableListDirective,
  ToasterService,
} from '@abp/ng.theme.shared';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-rental',
  templateUrl: './rental.component.html',
  styleUrls: ['./rental.component.scss'],
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
export class RentalComponent implements OnInit {
  rentals = { items: [], totalCount: 0 } as PagedResultDto<RentalDto>;
  customers: CustomerDto[] = [];
  movies: MovieDto[] = [];

  isModalOpen = false;
  form: FormGroup;

  public readonly list = inject(ListService);
  private readonly rentalService = inject(RentalService);
  private readonly customerService = inject(CustomerService);
  private readonly movieService = inject(MovieService);
  private readonly fb = inject(FormBuilder);
  private readonly confirmation = inject(ConfirmationService);
  private readonly toaster = inject(ToasterService);
  private readonly localization = inject(LocalizationService);

  ngOnInit() {
    const streamCreator = query => this.rentalService.getList(query);
    this.list.hookToQuery(streamCreator).subscribe(response => {
      this.rentals = response;
    });

    this.customerService.getList({ maxResultCount: 100, skipCount: 0 }).subscribe(response => {
      this.customers = response.items;
    });

    this.movieService.getList({ maxResultCount: 100, skipCount: 0 }).subscribe(response => {
      this.movies = response.items;
    });
  }

  openModal() {
    this.form = this.fb.group({
      customerId: ['', Validators.required],
      movieId: ['', Validators.required],
      dueDate: ['', Validators.required],
    });
    this.isModalOpen = true;
  }

  save() {
    if (this.form.invalid) return;

    this.rentalService.create(this.form.value).subscribe(rental => {
      this.isModalOpen = false;
      this.list.get();

      const movieTitle =
        this.movies.find(m => m.id === this.form.value.movieId)?.title ?? 'the movie';
      const dueDate = new Date(this.form.value.dueDate).toLocaleDateString();
      this.toaster.success('::ConfirmedRentedMovieMessage', '::RentCreated', {
        messageLocalizationParams: [movieTitle, dueDate],
      });
    });
  }

  markAsReturned(id: string, movieTitle: string) {
    this.confirmation
      .warn('::MarkRentalAsReturnedConfirmationMessage', '::AreYouSure')
      .subscribe(status => {
        if (status === Confirmation.Status.confirm) {
          this.rentalService.markAsReturned(id).subscribe(() => {
            this.list.get();
            this.toaster.success('::MovieReturnedNotificationMessage', '::RentalReturned', {
              messageLocalizationParams: [movieTitle],
            });
          });
        }
      });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.rentalService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
}
