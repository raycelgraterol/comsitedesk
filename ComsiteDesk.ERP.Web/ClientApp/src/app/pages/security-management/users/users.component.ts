import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Observable } from 'rxjs';

import { User, SearchResult } from '../../../core/models/auth.models';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { UserProfileService } from '../../../core/services/user.service';
import { AuthenticationService } from '../../../core/services/auth.service';
import { RolesService } from '../../../core/services/roles.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  
  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  tableData: User[];
  error = '';
  message = '';
  loading = false;
  submit: boolean;
  IsEdit = false;
  dataLoading: Observable<Boolean>;
  innerform: FormGroup;
  queryId: number;

  tables$: Observable<User[]>;
  total$: Observable<number>;
  item: User;
  roles: any;

  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;
  
  constructor(
    public service: UserProfileService,
    public formBuilder: FormBuilder,
    private modalService: NgbModal,
    private rolesService: RolesService,
    private authService: AuthenticationService) {
  }
  
  ngOnInit() {
    this.service.parentId = this.authService.currentUser().organizationId;
    
    this.service.getAll();
    this.getRoles();
    this.item = new User();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'LAExport', path: '/' },
      { label: 'Manejo Usuarios', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      firstName: ["", [Validators.required]],
      userName: ["", [Validators.required]],
      lastName: ["", [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ["", [Validators.required]],
      rolId: [0, [Validators.required]],
      password: [""]
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
   * Get Roles
   */
  getRoles() {    
    this.rolesService.getRoles()
        .subscribe(result => {
          this.roles = result.data;
        }, error => console.error(error));
  } 

  /**
   * Returns form
   */
  get form() {
    return this.innerform.controls;
  }

  /**
   * Get role by name
   * @param name Name of the rol
   * @returns 
   */
  getRolbyName(name: string){
    return this.roles.filter(x => x.name == name) == undefined 
    ? [] : this.roles.filter(x => x.name == name)[0];
  }

  /**
   * Get role by id
   */
   getRolbyId(id: number){
    return this.roles.filter(x => x.id == id) == undefined 
    ? [] : this.roles.filter(x => x.id == id)[0];
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
            this.innerform.controls.userName.setValue(this.item.userName);
            this.innerform.controls.firstName.setValue(this.item.firstName);
            this.innerform.controls.lastName.setValue(this.item.lastName);
            this.innerform.controls.email.setValue(this.item.email);
            this.innerform.controls.phoneNumber.setValue(this.item.phoneNumber);
            
            let value = this.getRolbyName(this.item.roles[0]);

            this.innerform.controls.rolId.setValue(value.id);
            this.innerform.controls['userName'].disable();
            this.innerform.controls['email'].disable();
            this.innerform.controls['password'].setValidators([]);    
            this.innerform.controls['password'].updateValueAndValidity();
          }      
        }, error => {
          console.error(error);
          }
        );
    }else{
      this.item = new User();
      this.innerform.controls['userName'].enable();
      this.innerform.controls['email'].enable();
        this.innerform.controls['password'].setValidators([Validators.required]);    
        this.innerform.controls['password'].updateValueAndValidity();
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
    this.item.firstName = this.form.firstName.value;
    this.item.lastName = this.form.lastName.value;
    this.item.userName = this.form.userName.value;
    this.item.email = this.form.email.value;
    this.item.phoneNumber = this.form.phoneNumber.value;
    this.item.password = this.form.password.value;
    this.item.organizationId = this.service.parentId;
    let value = this.getRolbyId(this.form.rolId.value);

    this.item.rolName = value.name;

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
