import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { Tickets, SearchResult, } from 'src/app/core/models/tickets.models';
import { TicketService } from 'src/app/core/services/ticket.service';

import { TicketTypes } from 'src/app/core/models/ticket-types.models';
import { TicketTypesService } from 'src/app/core/services/ticket-types.service';

import { TicketStatus } from 'src/app/core/models/ticket-status.models';
import { TicketStatusService } from 'src/app/core/services/ticket-status.service';

import { TicketCategories } from 'src/app/core/models/ticket-categories.models';
import { TicketCategoriesService } from 'src/app/core/services/ticket-categories.service';

import { TicketProcesses } from 'src/app/core/models/ticket-processes.models';
import { TicketProcessesService } from 'src/app/core/services/ticket-processes.service';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit {

  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  tableData: Tickets[];
  error = '';
  message = '';
  loading = false;
  submit: boolean;
  IsEdit = false;
  dataLoading: Observable<Boolean>;
  innerform: FormGroup;
  queryId: number;
  user: any;
  organizationId: number;
  
  tables$: Observable<Tickets[]>;
  total$: Observable<number>;
  item: Tickets;

  types: Array<TicketTypes>;
  status: Array<TicketStatus>;
  categories: Array<TicketCategories>;
  processes: Array<TicketProcesses>;

  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;

  constructor(
    public service: TicketService,
    public typesService: TicketTypesService,
    public statusService: TicketStatusService,
    public categoriesService: TicketCategoriesService,
    public processesService: TicketProcessesService,
    private authService: AuthenticationService,
    public formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private modalService: NgbModal) {

    this.user = this.authService.currentUser();
    if(this.user.organization != undefined){
      this.organizationId = this.user.organization.id;
    }

  }
  ngOnInit() {

    this.loadDropDownList();

    this.service.getAll();
    this.item = new Tickets();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'Comsite', path: '/' },
      { label: 'Tickets', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      title: ["", [Validators.required]],
      ticketDate: ["", [Validators.required]],
      hoursWorked: [0],
      reportedFailure: [""],
      technicalFailure: [""],
      solutionDone: [""],
      notes: [""],
      startTime: [Date.now],
      endTime: [Date.now],
      ticketStatusId: [0, [Validators.required]],
      ticketCategoryId: [0],
      ticketTypeId: [0, [Validators.required]],
      ticketProcessId: [0],
      organizationId: [0, [Validators.required]],
    });
  }

  loadDropDownList() {
    this.typesService.getAllItems()
      .subscribe(result => {
        this.types = result.data;
      }, error => console.error(error));

    this.statusService.getAllItems()
      .subscribe(result => {
        this.status = result.data;
      }, error => console.error(error));

    this.categoriesService.getAllItems()
      .subscribe(result => {
        this.categories = result.data;
      }, error => console.error(error));

    this.processesService.getAllItems()
      .subscribe(result => {
        this.processes = result.data;
      }, error => console.error(error));
  }

  /**
   * Sort table data
   * @param param0 sort the column
   *
   */
  onSort({ column, direction }: SortEvent) {
    // resetting other headers
    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });
    this.service.sortColumn = column;
    this.service.sortDirection = direction;
  }

  /**
   * Returns form
   */
  get form() {
    return this.innerform.controls;
  }

  /**
   * Responsive modal open
   * @param responsiveData responsive modal data
   */
  responsiveModal(responsiveData: string, Id: number = 0) {
    this.submit = false;
    this.loading = false;
    this.IsEdit = false;

    this.innerform.reset();
    this.innerform.controls.id.setValue(0);
    this.innerform.controls.organizationId.setValue(this.organizationId);

    if (Id != null && Id != 0) {
      this.IsEdit = true;
      this.service.getById(Id)
        .subscribe(result => {
          if (result) {
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);

            this.form.title.setValue(this.item.title);
            this.form.ticketDate.setValue(this.datePipe.transform(this.item.ticketDate, 'yyyy-MM-dd'));
            this.form.hoursWorked.setValue(this.item.hoursWorked);
            this.form.reportedFailure.setValue(this.item.reportedFailure);
            this.form.technicalFailure.setValue(this.item.technicalFailure);
            this.form.solutionDone.setValue(this.item.solutionDone);
            this.form.startTime.setValue(this.datePipe.transform(this.item.startTime, 'yyyy-MM-dd'));
            this.form.endTime.setValue(this.datePipe.transform(this.item.endTime, 'yyyy-MM-dd'));
            this.form.ticketStatusId.setValue(this.item.ticketStatusId);
            this.form.ticketTypeId.setValue(this.item.ticketTypeId);
            this.form.ticketCategoryId.setValue(this.item.ticketCategoryId);
            this.form.ticketProcessId.setValue(this.item.ticketProcessId);
            this.form.organizationId.setValue(this.item.organizationId);

          }
        }, error => {
          console.error(error);
        }
        );
    } else {
      this.item = new Tickets();
    }

    this.modalService.open(responsiveData, { windowClass: 'modal-full', centered: true, backdrop: 'static', keyboard: false });
  }

  /**
   * Bootsrap validation form submit method
   */
  validSubmit() {
    this.loading = true;
    this.submit = true;

    if (this.innerform.invalid) {
      this.loading = false;
      return;
    }

    this.item.id = parseInt(this.form.id.value);
    this.item.title = this.form.title.value;
    this.item.ticketDate = this.form.ticketDate.value;
    this.item.hoursWorked = parseInt(this.form.hoursWorked.value);
    this.item.reportedFailure = this.form.reportedFailure.value;
    this.item.technicalFailure = this.form.technicalFailure.value;
    this.item.solutionDone = this.form.solutionDone.value;
    this.item.startTime = this.form.startTime.value;
    this.item.endTime = this.form.endTime.value;
    this.item.ticketStatusId = parseInt(this.form.ticketStatusId.value);
    this.item.ticketTypeId = parseInt(this.form.ticketTypeId.value);
    this.item.ticketCategoryId = parseInt(this.form.ticketCategoryId.value);
    this.item.ticketProcessId = parseInt(this.form.ticketProcessId.value);
    this.item.organizationId = parseInt(this.form.organizationId.value);

    if (this.item.id == 0 || this.item.id == null) {

      this.service.add(this.item)
        .subscribe(result => {
          if (result) {
            console.log(result);
            this.service.getAll();
          }
          this.modalService.dismissAll();

          Swal.fire({
            position: 'center',
            type: "success",
            title: "Creado con exito!",
            showConfirmButton: false,
            timer: 1500
          });


        }, error => {
          console.error(error);
        }
        );

    } else {

      this.service.edit(this.item)
        .subscribe(result => {
          if (result) {
            console.log(result);
            this.service.getAll();
          }
          this.modalService.dismissAll();

          Swal.fire({
            position: 'center',
            type: "success",
            title: "Modificado con exito!",
            showConfirmButton: false,
            timer: 1500
          });

        }, error => {
          console.error(error);
        }
        );
    }

  }

  /**
   * Bootsrap validation form submit method
   */
  deleteItem(id: number) {
    this.loading = true;
    this.submit = true;

    Swal.fire({
      title: 'Estas seguro de querer Eliminar este Registro?',
      text: 'Este Cambio es irreversible!',
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'No, cancelar!',
      confirmButtonClass: 'btn btn-success mt-2',
      cancelButtonClass: 'btn btn-danger ml-2 mt-2',
      buttonsStyling: false
    }).then((confirm) => {
      if (confirm.value) {
        this.service.delete(id)
          .subscribe(result => {
            if (result) {

              Swal.fire({
                position: 'center',
                type: "success",
                title: "Borrado con exito",
                showConfirmButton: true
              });

              console.log(result);
              this.service.getAll();
            }
          }, error => {
            console.error(error);
          }
          );
      }
    });

    this.loading = false;

  }

}
