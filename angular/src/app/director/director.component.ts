import {
  ListService,
  PagedAndSortedResultRequestDto,
  PagedResultDto,
  LocalizationService,
  CoreModule,
} from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';
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
  selector: 'app-director',
  templateUrl: './director.component.html',
  styleUrls: ['./director.component.scss'],
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
export class DirectorComponent implements OnInit {
  directors = { items: [], totalCount: 0 } as PagedResultDto<DirectorDto>;

  isModalOpen = false;
  isEditMode = false;
  selectedDirectorId: string | null = null;
  form: FormGroup;

  public readonly list = inject(ListService);
  private readonly directorService = inject(DirectorService);
  private readonly fb = inject(FormBuilder);
  private readonly confirmation = inject(ConfirmationService);
  private readonly localization = inject(LocalizationService);

  ngOnInit() {
    const streamCreator = (query: PagedAndSortedResultRequestDto) =>
      this.directorService.getList(query);
    this.list.hookToQuery(streamCreator).subscribe(response => {
      this.directors = response;
    });
  }

  openModal(director?: DirectorDto) {
    this.isEditMode = !!director;
    this.selectedDirectorId = director?.id ?? null;
    this.form = this.fb.group({
      name: [director?.name ?? '', Validators.required],
      nationality: [director?.nationality ?? '', Validators.required],
    });
    this.isModalOpen = true;
  }

  save() {
    if (this.form.invalid) return;

    const request = this.isEditMode
      ? this.directorService.update(this.selectedDirectorId, this.form.value)
      : this.directorService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.list.get();
    });
  }

  delete(id: string) {
    this.confirmation
      .warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe(status => {
        if (status === Confirmation.Status.confirm) {
          this.directorService.delete(id).subscribe(() => this.list.get());
        }
      });
  }
}
