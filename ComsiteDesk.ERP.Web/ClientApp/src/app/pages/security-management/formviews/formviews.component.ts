import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';

import { Observable } from 'rxjs';

import { FormViews, SearchResult } from '../../../core/models/form-views.models';
import { SecurityModulesService } from '../../../core/services/security/security-modules.service';
import { FormViewsService } from '../../../core/services/security/form-views.service';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';

import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';
import { SecurityModules } from 'src/app/core/models/security-modules.models';

@Component({
  selector: 'app-formviews',
  templateUrl: './formviews.component.html',
  styleUrls: ['./formviews.component.scss']
})
export class FormviewsComponent implements OnInit {

  
  // bread crum data
  breadCrumbItems: Array<{}>;

  // Table data
  error = '';
  message = '';
  loading = false;
  submit: boolean;
  IsEdit = false;
  dataLoading: Observable<Boolean>;

  innerform: FormGroup;
  loadingBank: false;

  tables$: Observable<FormViews[]>;
  total$: Observable<number>;
  item: FormViews;

  modules: Array<SecurityModules>;

  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;
  modalReference: any;
  closeResult: string;
  modalReferenceProvider: NgbModalRef;

  constructor(public service: FormViewsService,
                public moduleService: SecurityModulesService,                
                public formBuilder: FormBuilder,
                private modalService: NgbModal,
                private datePipe: DatePipe) {
  }

  ngOnInit() {
    
    this.service.getAll();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'LAExport', path: '/' },
      { label: 'Vistas', path: '/', active: true }
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
      description: [""],
      uri: ["", [Validators.required ]],
      moduleId: [0, [Validators.required]],
    });

    //
    this.moduleService.getAllModules()
          .subscribe(result => {
            this.modules = result.data;
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
    
    if (Id != null && Id != 0) {
      this.IsEdit = true;
      this.service.getById(Id)
        .subscribe(result => {
          if(result){
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);
            this.innerform.controls.name.setValue(this.item.name);
            this.innerform.controls.description.setValue(this.item.description);
            this.innerform.controls.uri.setValue(this.item.uri);
            this.innerform.controls.moduleId.setValue(this.item.moduleId);
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

    this.item = new FormViews();
    
    this.item.id = this.form.id.value;
    this.item.name = this.form.name.value;
    this.item.description = this.form.description.value;
    this.item.uri = this.form.uri.value;
    this.item.moduleId = this.form.moduleId.value;
    
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
            type: result.type,
            title: result.message,
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
              type: result.type,
              title: result.message,
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
