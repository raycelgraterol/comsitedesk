import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';

import { Observable } from 'rxjs';

import { SecurityModules, SearchResult } from '../../../core/models/security-modules.models';

import { SecurityModulesService } from '../../../core/services/security/security-modules.service';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';

import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-modules',
  templateUrl: './modules.component.html',
  styleUrls: ['./modules.component.scss']
})
export class ModulesComponent implements OnInit {

  
  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  error = '';
  message = '';
  loading = false;
  submit: boolean;
  IsEdit = false;
  

  innerform: FormGroup;
  loadingBank: false;

  tables$: Observable<SecurityModules[]>;
  total$: Observable<number>;
  dataLoading$: Observable<Boolean>;
  item: SecurityModules;

  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;
  modalReference: any;
  closeResult: string;
  modalReferenceProvider: NgbModalRef;

  constructor(public service: SecurityModulesService,             
                public formBuilder: FormBuilder,
                private modalService: NgbModal,
                private datePipe: DatePipe) {
  }

  ngOnInit() {
    
    this.service.getAll();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'LAExport', path: '/' },
      { label: 'Modulos', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading$ = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      name: ["", [Validators.required]],
      uri: ["", [Validators.required]],
      description: [""]
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
    this.IsEdit = false;
    
    this.innerform.reset();    
    
    if (Id != null && Id != 0) {
      this.IsEdit = true;
      this.service.getById(Id)
        .subscribe(result => {
          if(result){
            this.IsEdit = true;
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);
            this.innerform.controls.name.setValue(this.item.name);
            this.innerform.controls.uri.setValue(this.item.uri);
            this.innerform.controls.description.setValue(this.item.description);
          }
        }, error => {
          console.error(error);
          }
        );
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

    this.item = new SecurityModules();
    
    this.item.id = this.form.id.value;
    this.item.name = this.form.name.value;
    this.item.uri = this.form.uri.value;
    this.item.description = this.form.description.value;
    
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
          this.loading = false;


        }, error => {
          console.error(error);
          this.loading = false;
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
            this.loading = false;

          }, error => {
            console.error(error);
            this.loading = false;
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

