import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { ProjectModel, SearchResult } from 'src/app/core/models/projects.models';
import { ProjectsService } from 'src/app/core/services/projects.service';

import { Projects } from './projects.model';

import { projectData } from './data';
import { ProjectStatusService } from 'src/app/core/services/project-status.service';
import { ProjectStatusModel } from 'src/app/core/models/projectStatus.models';
import { DatePipe, DecimalPipe } from '@angular/common';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { User } from 'src/app/core/models/auth.models';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  // bread crumb items
  breadCrumbItems: Array<{}>;

  // Table data
  tableData: ProjectModel[];
  error = '';
  message = '';
  loading = false;
  submit: boolean;
  IsEdit = false;
  user: User;
  
  innerform: FormGroup;
  queryId: number;

  tables$: Observable<ProjectModel[]>;
  total$: Observable<number>;
  dataLoading$: Observable<Boolean>;
  item: ProjectModel;

  projectStatus: ProjectStatusModel;
  currentStatus: string;

  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;

  projectData: Projects[];

  constructor(
    public service: ProjectsService,
    public formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private datePipe: DatePipe,
    public projectStatusService: ProjectStatusService,
    private modalService: NgbModal) {
  }

  ngOnInit() {

    this.service.getAll();
    this.item = new ProjectModel();
    this.user = this.authService.currentUser();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'ComSite', path: '/' }, 
      { label: 'Projects', path: '/', active: true }];

    /**
     * fetches data
     */
    this._fetchData();

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading$ = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      title: ["", [Validators.required]],
      description: [""],
      startDate: ["", [Validators.required]],
      endDate: [""],
      organizationId: [1, [Validators.required]],
      projectStatusId: [0, [Validators.required]]
    });

  }

  /**
   * fetches project value
   */
  private _fetchData() {

    this.projectStatusService.getAllItems()
      .subscribe(result => {
        this.projectStatus = result.data;
      }, error => console.error(error));

    this.projectData = projectData;
  }

  /**
   * Change Status to search
   * @param statusName 
   */
  changeStatus(id: number, statusName: string){
    this.service.parentId = id;
    this.currentStatus = statusName;
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

    if (Id != null && Id != 0) {
      this.IsEdit = true;
      this.service.getById(Id)
        .subscribe(result => {
          if (result) {
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);
            this.innerform.controls.title.setValue(this.item.title);
            this.innerform.controls.description.setValue(this.item.description);
            this.innerform.controls.startDate.setValue(this.datePipe.transform(this.item.startDate, 'yyyy-MM-dd'));
            this.innerform.controls.endDate.setValue(this.datePipe.transform(this.item.endDate, 'yyyy-MM-dd'));
            this.innerform.controls.organizationId.setValue(this.item.organizationId);
            this.innerform.controls.projectStatusId.setValue(this.item.projectStatusId);

          }
        }, error => {
          console.error(error);
        }
        );
    } else {
      this.innerform.controls.organizationId.setValue(this.user.organizationId);
      this.item = new ProjectModel();
    }

    this.modalService.open(responsiveData, { size: 'lg', centered: true, backdrop: 'static', keyboard: false });
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

    this.item.id = this.form.id.value;
    this.item.title = this.form.title.value;
    this.item.description = this.form.description.value;
    this.item.startDate = this.form.startDate.value;
    this.item.endDate = this.form.endDate.value == "" ? null : this.form.endDate.value;
    this.item.organizationId = this.form.organizationId.value;
    this.item.projectStatusId = this.form.projectStatusId.value;

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