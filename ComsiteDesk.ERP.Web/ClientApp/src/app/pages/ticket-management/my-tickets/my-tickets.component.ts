import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { Tickets, SearchResult } from 'src/app/core/models/tickets.models';
import { TicketService } from 'src/app/core/services/ticket.service';

import { ClientService } from 'src/app/core/services/client.service';
import { ClientModel } from 'src/app/core/models/client.models';

import { AuthenticationService } from 'src/app/core/services/security/auth.service';
import { DatePipe } from '@angular/common';
import { EquipmentService } from 'src/app/core/services/equipment.service';
import { TicketProcessesService } from 'src/app/core/services/ticket-processes.service';
import { TicketCategoriesService } from 'src/app/core/services/ticket-categories.service';
import { TicketStatusService } from 'src/app/core/services/ticket-status.service';
import { TicketTypesService } from 'src/app/core/services/ticket-types.service';
import { TicketTypes } from 'src/app/core/models/ticket-types.models';
import { TicketStatus } from 'src/app/core/models/ticket-status.models';
import { TicketCategories } from 'src/app/core/models/ticket-categories.models';
import { TicketProcesses } from 'src/app/core/models/ticket-processes.models';
import { EquipmentModel } from 'src/app/core/models/equipment.models';
import { User } from 'src/app/core/models/auth.models';

@Component({
  selector: 'app-my-tickets',
  templateUrl: './my-tickets.component.html',
  styleUrls: ['./my-tickets.component.scss']
})
export class MyTicketsComponent implements OnInit {


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
  clients: Array<ClientModel>;

  types: Array<TicketTypes>;
  status: Array<TicketStatus>;
  categories: Array<TicketCategories>;
  processes: Array<TicketProcesses>;
  equipments: Array<EquipmentModel>;
  users: Array<User>;

  tables$: Observable<Tickets[]>;
  total$: Observable<number>;
  item: Tickets;
  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;

  constructor(
    public service: TicketService,
    public formBuilder: FormBuilder,
    public authService: AuthenticationService,
    public typesService: TicketTypesService,
    public statusService: TicketStatusService,
    public categoriesService: TicketCategoriesService,
    public processesService: TicketProcessesService,
    public equipmentService: EquipmentService,
    private clientService: ClientService,
    private datePipe: DatePipe,
    private modalService: NgbModal) {
    this.user = this.authService.currentUser();
    this.service.assignedTo = this.user.id;
  }
  ngOnInit() {

    this.service.getAll();
    this.item = new Tickets();
    this.loadDropDownList();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'Comsite', path: '/' },
      { label: 'Mis Tickets', path: '/', active: true }
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
      clientId: [0, [Validators.required]],
      usersIds: [[], [Validators.required]],
      equipmentIds: [[]]
    });
  }

  loadDropDownList() {

    this.clientService.getAllItems()
      .subscribe(result => {
        this.clients = result.data;
      }, error => console.error(error));

    this.authService.getAllUsers()
      .subscribe(result => {
        this.users = result.data;
      }, error => console.error(error));

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

    this.equipmentService.getAllItems()
      .subscribe(result => {
        this.equipments = result.data;
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
  ticketPedingModal(TicketData: string) {
    this.service.ticketStatusId = 1;
    this.service.assignedTo = 0;
    this.service.getAll();

    this.modalService.open(TicketData, { windowClass: 'modal-full', centered: true, backdrop: 'static', keyboard: false });

  }

  /**
   * Close modal and reset values
   */
  closeModalTickets(){
    this.service.ticketStatusId = 0;
    this.service.assignedTo = this.user.id;
    this.service.getAll();
    this.modalService.dismissAll();
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

    if (Id != null && Id != 0) {
      this.IsEdit = true;
      this.service.getById(Id)
        .subscribe(result => {
          if (result) {
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);

            this.form.title.setValue(this.item.title);
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
            this.form.clientIdclientId.setValue(this.item.clientId);

            this.service.GetListUsersByTicket(this.item.id)
              .subscribe(result => {
                if (result) {
                  let userIds = result.data.map(x => x.userId);
                  this.form.usersIds.setValue(userIds);
                }
              }, error => {
                console.error(error);
              }
              );

            this.service.GetListEquipmentsByTicket(this.item.id)
              .subscribe(result => {
                if (result) {
                  let equipmentIds = result.data.map(x => x.equipmentId);
                  this.form.equipmentIds.setValue(equipmentIds);
                }
              }, error => {
                console.error(error);
              }
              );

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
    this.item.hoursWorked = parseInt(this.form.hoursWorked.value == null ? "0" : this.form.hoursWorked.value);
    this.item.reportedFailure = this.form.reportedFailure.value;
    this.item.technicalFailure = this.form.technicalFailure.value;
    this.item.solutionDone = this.form.solutionDone.value;
    this.item.startTime = this.form.startTime.value == null ? null : new Date(this.form.startTime.value);
    this.item.endTime = this.form.endTime.value == null ? null : new Date(this.form.endTime.value);
    this.item.ticketStatusId = parseInt(this.form.ticketStatusId.value);
    this.item.ticketTypeId = parseInt(this.form.ticketTypeId.value);
    this.item.ticketCategoryId = parseInt(this.form.ticketCategoryId.value);
    this.item.ticketProcessId = this.form.ticketProcessId.value == null ? null : parseInt(this.form.ticketProcessId.value);
    this.item.clientId = parseInt(this.form.clientId.value);
    this.item.usersIds = this.form.usersIds.value;
    this.item.equipmentIds = this.form.equipmentIds.value;

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

}