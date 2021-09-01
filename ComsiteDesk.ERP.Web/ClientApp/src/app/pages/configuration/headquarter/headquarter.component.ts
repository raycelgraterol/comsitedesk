import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

import { HeadquarterModel, SearchResult, } from 'src/app/core/models/headquarter.models';
import { HeadquarterService } from 'src/app/core/services/headquarter.service';
import { AuthenticationService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-headquarter',
  templateUrl: './headquarter.component.html',
  styleUrls: ['./headquarter.component.scss']
})
export class HeadquarterComponent implements OnInit {

  
  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  tableData: HeadquarterModel[];
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

  tables$: Observable<HeadquarterModel[]>;
  total$: Observable<number>;
  item: HeadquarterModel;
  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;

  constructor(
    public service: HeadquarterService,
    public formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private modalService: NgbModal) {
      this.user = this.authService.currentUser();
      if (this.user.organization != undefined) {
        this.organizationId = this.user.organization.id;
      }
  }
  ngOnInit() {

    this.service.getAll();
    this.item = new HeadquarterModel();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'Comsite', path: '/' },
      { label: 'Sede', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      name: ["", [Validators.required]],
      phoneNumber: [""],
      address: ["", [Validators.required]],
      organizationsId: ["", [Validators.required]],
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
    this.innerform.controls.organizationsId.setValue(this.user.organization.id);

    if (Id != null && Id != 0) {
      this.IsEdit = true;
      this.service.getById(Id)
        .subscribe(result => {
          if(result){
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);
            this.innerform.controls.name.setValue(this.item.name);
            this.innerform.controls.phoneNumber.setValue(this.item.phoneNumber);
            this.innerform.controls.address.setValue(this.item.address);
            this.innerform.controls.organizationsId.setValue(this.item.organizationsId);
          }      
        }, error => {
          console.error(error);
          }
        );
    }else{
      this.item = new HeadquarterModel();
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
    this.item.address = this.form.address.value;
    this.item.organizationsId = this.form.organizationsId.value;
    
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
