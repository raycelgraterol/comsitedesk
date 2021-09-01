import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { EquipmentModel, SearchResult } from 'src/app/core/models/equipment.models';
import { EquipmentService } from 'src/app/core/services/equipment.service';

import { EquipmentUserModel } from 'src/app/core/models/equipmentUser.models';
import { DepartmentService } from 'src/app/core/services/department.service';
import { DepartmentModel } from 'src/app/core/models/department.models';
import { EquipmentUserService } from 'src/app/core/services/equipment-user.service';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.scss']
})
export class EquipmentComponent implements OnInit {

  
  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  tableData: EquipmentModel[];
  error = '';
  message = '';
  loading = false;
  submit: boolean;
  IsEdit = false;
  dataLoading: Observable<Boolean>;
  innerform: FormGroup;
  queryId: number;

  equipmentUsers : Array<EquipmentUserModel>;
  departments : Array<DepartmentModel>;

  tables$: Observable<EquipmentModel[]>;
  total$: Observable<number>;
  item: EquipmentModel;
  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;

  constructor(
    public service: EquipmentService,
    public formBuilder: FormBuilder,
    private departmentService: DepartmentService,
    private equipmentUserService: EquipmentUserService,
    private modalService: NgbModal) {
      
  }
  ngOnInit() {

    this.service.getAll();
    this.item = new EquipmentModel();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'Comsite', path: '/' },
      { label: 'Sede', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading = this.service.loading$;

    this.departmentService.getAllItems()
      .subscribe(result => {
        this.departments = result.data;
      }, error => console.error(error));
      
    this.equipmentUserService.getAllItems()
      .subscribe(result => {
        this.equipmentUsers = result.data;
      }, error => console.error(error));

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      name: ["", [Validators.required]],
      type: [""],
      make: [""],
      model: [""],
      serial: [""],
      features: [""],
      notes: [""],
      departmentId: [0, [Validators.required]],
      equipmentUserId: [0, [Validators.required]],
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
            this.innerform.controls.type.setValue(this.item.type); 
            this.innerform.controls.make.setValue(this.item.make);
            this.innerform.controls.model.setValue(this.item.model);
            this.innerform.controls.serial.setValue(this.item.serial);
            this.innerform.controls.features.setValue(this.item.features);
            this.innerform.controls.notes.setValue(this.item.notes);
            this.innerform.controls.departmentId.setValue(this.item.departmentId);           
            this.innerform.controls.equipmentUserId.setValue(this.item.equipmentUserId);           

          }      
        }, error => {
          console.error(error);
          }
        );
    }else{
      this.item = new EquipmentModel();
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
    this.item.type = this.form.type.value;
    this.item.make = this.form.make.value;
    this.item.model = this.form.model.value;
    this.item.serial = this.form.serial.value;
    this.item.features = this.form.features.value;
    this.item.notes = this.form.notes.value;
    this.item.departmentId = this.form.departmentId.value;
    this.item.equipmentUserId = this.form.equipmentUserId.value;
    
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
