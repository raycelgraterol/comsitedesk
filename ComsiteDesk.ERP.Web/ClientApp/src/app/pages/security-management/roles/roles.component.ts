import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Observable } from 'rxjs';

import { Rol, SearchResult } from '../../../core/models/rol.models';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { RolesService } from '../../../core/services/roles.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss']
})
export class RolesComponent implements OnInit {

  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  tableData: Rol[];
  error = '';
  message = '';
  loading = false;
  submit: boolean;
  IsEdit = false;
  dataLoading: Observable<Boolean>;
  innerform: FormGroup;
  queryId: number;

  tables$: Observable<Rol[]>;
  total$: Observable<number>;
  item: Rol;

  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;
  
  constructor(
    public service: RolesService,
    public formBuilder: FormBuilder,
    private modalService: NgbModal) {
  }

  ngOnInit() {
    
    this.service.getAll();
    this.item = new Rol();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'LAExport', path: '/' },
      { label: 'Manejo Roles', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      name: ["", [Validators.required]]
    });

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
          if(result){
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);
            this.innerform.controls.name.setValue(this.item.name);
            
          }      
        }, error => {
          console.error(error);
          }
        );
    }else{
      this.item = new Rol();
    }


    this.modalService.open(responsiveData, { size: 'lg', centered: true, backdrop: 'static', keyboard: false});
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
    this.item.name = this.form.name.value;

    if(this.item.id == 0 || this.item.id == null){
      this.service.add(this.item)
        .subscribe(result => {
          if(result){
            this.service.getAll();
          }
          this.modalService.dismissAll();

          Swal.fire({
            position: 'center',
            type: "success",
            title: "Creado con exito!",
            showConfirmButton: false,
            timer: 1000
          });

        }, error => {
          console.error(error);
          }
        );
    }else{

      this.service.edit(this.item)
          .subscribe(result => {
            if(result){
              this.service.getAll();
            }
            this.modalService.dismissAll();

            Swal.fire({
              position: 'center',
              type: "success",
              title: "Modificado con exito!",
              showConfirmButton: false,
              timer: 1000
            });

          }, error => {
            console.error(error);
            }
          );
    }
    
  }
}
