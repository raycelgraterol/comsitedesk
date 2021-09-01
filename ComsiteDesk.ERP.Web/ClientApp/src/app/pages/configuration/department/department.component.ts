import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { DepartmentModel, SearchResult, } from 'src/app/core/models/department.models';
import { DepartmentService } from 'src/app/core/services/department.service';

import { HeadquarterModel } from 'src/app/core/models/headquarter.models';
import { HeadquarterService } from 'src/app/core/services/headquarter.service';


@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent implements OnInit {

  
  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  tableData: DepartmentModel[];
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

  eadquarters: Array<HeadquarterModel>;

  tables$: Observable<DepartmentModel[]>;
  total$: Observable<number>;
  item: DepartmentModel;
  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;

  constructor(
    public service: DepartmentService,
    public headquarterService: HeadquarterService,
    public formBuilder: FormBuilder,
    private modalService: NgbModal) {
  }
  ngOnInit() {

    this.service.getAll();
    this.item = new DepartmentModel();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'Comsite', path: '/' },
      { label: 'Departamento', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading = this.service.loading$;

    this.headquarterService.getAllItems()
    .subscribe(result => {
      this.eadquarters = result.data;
    }, error => console.error(error));

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      name: ["", [Validators.required]],
      phoneNumber: [""],
      headquarterId: ["", [Validators.required]]
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
            this.innerform.controls.phoneNumber.setValue(this.item.phoneNumber); 
            this.innerform.controls.headquarterId.setValue(this.item.headquarterId);           

          }      
        }, error => {
          console.error(error);
          }
        );
    }else{
      this.item = new DepartmentModel();
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
    this.item.phoneNumber = this.form.phoneNumber.value;
    this.item.headquarterId = this.form.headquarterId.value;
    
    if(this.item.id == 0 || this.item.id == null){

      this.service.add(this.item)
        .subscribe(result => {
          if(result){
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
      
    }else{

      this.service.edit(this.item)
          .subscribe(result => {
            if(result){
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
            if(result){

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
