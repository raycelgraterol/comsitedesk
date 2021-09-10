import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';

import { Observable } from 'rxjs';

import { Actionsviews, SearchResult } from '../../../core/models/actionsviews.models';
import { ActionsviewsService } from '../../../core/services/security/actionsviews.service';

import { FormViewsService } from '../../../core/services/security/form-views.service';
import { FormActionsService } from '../../../core/services/security/form-actions.service';

import { FormViews } from '../../../core/models/form-views.models';
import { FormActions } from '../../../core/models/form-actions.models';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';

import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';
import { SecurityModules } from 'src/app/core/models/security-modules.models';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-viewsactions',
  templateUrl: './viewsactions.component.html',
  styleUrls: ['./viewsactions.component.scss']
})
export class ViewsactionsComponent implements OnInit {

  
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

  tables$: Observable<Actionsviews[]>;
  total$: Observable<number>;
  item: Actionsviews;

  views: Array<FormViews>;
  actions: Array<FormActions>;
  viewId: number;
  viewName: string;

  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;
  modalReference: any;
  closeResult: string;
  modalReferenceProvider: NgbModalRef;

  constructor(public service: ActionsviewsService,
                public formViewsService: FormViewsService,
                public formActionsService: FormActionsService,
                public formBuilder: FormBuilder,
                private modalService: NgbModal,
                private route: ActivatedRoute,
                private datePipe: DatePipe) {
    this.viewId = parseInt(this.route.snapshot.paramMap.get('id'));
    this.service.parentId = this.viewId;

    this.formViewsService.getById(this.viewId)
        .subscribe(result => {
          this.viewName = result.data.name;
        }, error => console.error(error));;
  }

  ngOnInit() {
    
    this.service.getAll();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'LAExport', path: '/' },
      { label: 'Vistas', path: '/security/formviews'},
      { label: 'Acciones', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      id: [0],
      formId: [{value :0, disabled: true}, [Validators.required]],
      actionId: [0, [Validators.required]],
    });

    //
    this.formViewsService.getAllViews()
          .subscribe(result => {
            this.views = result.data;
          }, error => console.error(error));
    
    //
    this.formActionsService.getAllActions()
          .subscribe(result => {
            this.actions = result.data;
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
    this.innerform.controls.formId.setValue(this.viewId);

    if (Id != null && Id != 0) {
      this.IsEdit = true;
      this.service.getById(Id)
        .subscribe(result => {
          if(result){
            this.item = result.data;
            this.innerform.controls.id.setValue(this.item.id);
            this.innerform.controls.formId.setValue(this.item.formId);
            this.innerform.controls.actionId.setValue(this.item.actionId);
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

    this.item = new Actionsviews();
    
    this.item.id = this.form.id.value;
    this.item.formId = this.form.formId.value;
    this.item.actionId = this.form.actionId.value;
    
    if(this.item.id == 0 || this.item.id == null){

      this.service.add(this.item)
        .subscribe(result => {
          if(result){
            console.log(result.data);
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
