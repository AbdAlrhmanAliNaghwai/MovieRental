import { ListService, PagedResultDto, LocalizationService, CoreModule } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';
import { CustomerService, CustomerDto } from '../proxy/customers';
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
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss'],
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
export class CustomerComponent implements OnInit {
  customers = { items: [], totalCount: 0 } as PagedResultDto<CustomerDto>;

  isModalOpen = false;
  isEditMode = false;
  selectedCustomerId: string | null = null;
  form: FormGroup;

  public readonly list = inject(ListService);
  private readonly customerService = inject(CustomerService);
  private readonly fb = inject(FormBuilder);
  private readonly confirmation = inject(ConfirmationService);
  private readonly localization = inject(LocalizationService);

  ngOnInit() {
    const streamCreator = query => this.customerService.getList(query);
    this.list.hookToQuery(streamCreator).subscribe(response => {
      this.customers = response;
    });
  }

  openModal(customer?: CustomerDto) {
    this.isEditMode = !!customer;
    this.selectedCustomerId = customer?.id ?? null;
    this.form = this.fb.group({
      fullName: [customer?.fullName ?? '', Validators.required],
      email: [customer?.email ?? '', [Validators.required, Validators.email]],
      phoneNumber: [customer?.phoneNumber ?? '', Validators.required],
    });
    this.isModalOpen = true;
  }

  save() {
    if (this.form.invalid) return;

    const request = this.isEditMode
      ? this.customerService.update(this.selectedCustomerId, this.form.value)
      : this.customerService.create(this.form.value);

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
          this.customerService.delete(id).subscribe(() => this.list.get());
        }
      });
  }
}
