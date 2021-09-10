import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { ClientModel, SearchResult } from 'src/app/core/models/client.models';
import { ClientService } from 'src/app/core/services/client.service';

import { EquipmentUserModel } from 'src/app/core/models/equipmentUser.models';
import { DepartmentService } from 'src/app/core/services/department.service';
import { DepartmentModel } from 'src/app/core/models/department.models';
import { EquipmentUserService } from 'src/app/core/services/equipment-user.service';
import { ClientTypeModel } from 'src/app/core/models/clientType.models';
import { AuthenticationService } from 'src/app/core/services/security/auth.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.scss']
})
export class ClientComponent implements OnInit {

  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  tableData: ClientModel[];
  error = '';
  message = '';
  loading = false;
  submit: boolean;
  IsEdit = false;
  dataLoading: Observable<Boolean>;
  innerform: FormGroup;
  queryId: number;
  organizationId: number;
  user: any;

  public clientType : Array<ClientTypeModel> = [];

  equipmentUsers : Array<EquipmentUserModel>;
  departments : Array<DepartmentModel>;

  tables$: Observable<ClientModel[]>;
  total$: Observable<number>;
  item: ClientModel;
  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;

  constructor(
    public service: ClientService,
    public formBuilder: FormBuilder,
    private departmentService: DepartmentService,
    private authService: AuthenticationService,
    private equipmentUserService: EquipmentUserService,
    private modalService: NgbModal) {
    this.user = this.authService.currentUser();
      if (this.user.organization != undefined) {
        this.organizationId = this.user.organization.id;
      }
  }
  ngOnInit() {

    this.service.getAll();
    this.item = new ClientModel();
    this.clientType.push(
      {id:1,name:"Natural",code:"N"},
      {id:2,name:"Juridico",code:"J"}
    );

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'Comsite', path: '/' },
      { label: 'Clientes', path: '/', active: true }
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
      id: [0, [Validators.required]],
      businessName: ["", [Validators.required]],
      firstName: ["", [Validators.required]],
      lastName: ["", [Validators.required]],
      idNumer: ["", [Validators.required]],
      email: ["", [Validators.required]],
      phoneNumber: [""],
      address: [""],
      clientTypesId: [0, [Validators.required]],
      organizationId: [0, [Validators.required]]
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
    this.innerform.controls.organizationId.setValue(this.organizationId);

    if (Id != null && Id != 0) {
      this.IsEdit = true;
      this.service.getById(Id)
        .subscribe(result => {
          if(result){
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);
            this.innerform.controls.businessName.setValue(this.item.businessName);
            this.innerform.controls.firstName.setValue(this.item.firstName);
            this.innerform.controls.lastName.setValue(this.item.lastName);
            this.innerform.controls.idNumer.setValue(this.item.idNumer);
            this.innerform.controls.email.setValue(this.item.email);
            this.innerform.controls.phoneNumber.setValue(this.item.phoneNumber);
            this.innerform.controls.address.setValue(this.item.address);
            this.innerform.controls.clientTypesId.setValue(this.item.clientTypesId);
            this.innerform.controls.organizationId.setValue(this.item.organizationId);         

          }      
        }, error => {
          console.error(error);
          }
        );
    }else{
      this.item = new ClientModel();
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
    this.item.businessName = this.form.businessName.value;
    this.item.firstName = this.form.firstName.value;
    this.item.lastName = this.form.lastName.value;
    this.item.idNumer = this.form.idNumer.value;
    this.item.email = this.form.email.value;
    this.item.phoneNumber = this.form.phoneNumber.value;
    this.item.address = this.form.address.value;
    this.item.clientTypesId = this.form.clientTypesId.value;
    this.item.organizationId = this.form.organizationId.value;
    
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
