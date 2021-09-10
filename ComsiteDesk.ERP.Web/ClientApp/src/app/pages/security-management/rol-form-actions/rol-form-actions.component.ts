import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormGroup, FormControl, FormArray, FormBuilder, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';

import { Observable } from 'rxjs';

import { RoleFormAction, RoleFormArrayActions, SearchResult } from '../../../core/models/role-form-actions.models';
import { RoleFormActionsService } from '../../../core/services/security/role-form-actions.service';

import { AdvancedSortableDirective, SortEvent } from '../advanced-sortable.directive';

import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';
import { SecurityModules } from 'src/app/core/models/security-modules.models';
import { ActivatedRoute } from '@angular/router';
import { RolesService } from 'src/app/core/services/security/roles.service';

import { Actionsviews } from 'src/app/core/models/actionsviews.models';
import { ActionsviewsService } from 'src/app/core/services/security/actionsviews.service';

import { FormViews } from 'src/app/core/models/form-views.models';
import { FormViewsService } from 'src/app/core/services/security/form-views.service';
import { SecurityModulesService } from 'src/app/core/services/security/security-modules.service';


@Component({
  selector: 'app-rol-form-actions',
  templateUrl: './rol-form-actions.component.html',
  styleUrls: ['./rol-form-actions.component.scss']
})
export class RolFormActionsComponent implements OnInit {


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

  tables$: Observable<RoleFormAction[]>;
  total$: Observable<number>;
  item: RoleFormAction;

  actionsviews: Array<Actionsviews>;
  formViews: Array<Actionsviews>;
  securityModules: any;
  formActions: any;

  roleId: number;
  roleName: string;

  @ViewChildren(AdvancedSortableDirective) headers: QueryList<AdvancedSortableDirective>;
  modalReference: any;
  closeResult: string;
  modalReferenceProvider: NgbModalRef;

  constructor(public service: RoleFormActionsService,
    public rolesService: RolesService,
    public actionsviewsService: ActionsviewsService,
    public formViewsService: FormViewsService,
    public modulesService: SecurityModulesService,
    public formBuilder: FormBuilder,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private datePipe: DatePipe) {
    this.roleId = parseInt(this.route.snapshot.paramMap.get('id'));
    this.service.parentId = this.roleId;

    this.rolesService.getById(this.roleId)
      .subscribe(result => {
        this.roleName = result.data.name;
      }, error => console.error(error));;
  }

  ngOnInit() {

    this.service.getAll();

    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'LAExport', path: '/' },
      { label: 'Roles', path: '/security/roles' },
      { label: 'Vistas Acciones', path: '/', active: true }
    ];

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
    this.innerform = this.formBuilder.group({
      roleId: [{ value: 0, disabled: true }, [Validators.required]],
      formId: [0, [Validators.required]],
      moduleId: [0, [Validators.required]],
      actions: [[], [Validators.required]]
    });

    //
    this.modulesService.getAllModules()
    .subscribe(result => {
      this.securityModules = result.data;
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
  responsiveModal(responsiveData: string, moduleId: number = 0, Id: number = 0) {
    this.submit = false;
    this.loading = false;
    this.IsEdit = false;

    this.innerform.reset();
    this.formViews = [];
    this.formActions = [];
    this.innerform.controls.roleId.setValue(this.roleId);

    if (Id != null && Id != 0) {

      this.innerform.controls.moduleId.setValue(moduleId);
      this.changeModule();
      
      this.innerform.controls.formId.setValue(Id);
      this.changeView();
      

      this.IsEdit = true;
      this.actionsviewsService.GetListFormActions(this.roleId, Id)
        .subscribe(result => {
          if (result) {
            this.item = result.data;
            
            let actionIds = result.data.map(x => x.id);
            this.innerform.controls.actions.setValue(actionIds);

          }
        }, error => {
          console.error(error);
        }
        );
    }


    this.modalService.open(responsiveData, { size: 'lg', centered: true, backdrop: 'static', keyboard: false });
  }

  changeModule(){
    if(this.form.moduleId.value != 0 || this.form.moduleId.value != null ){
      //
      this.formViewsService.getAllViewsByModule(this.form.moduleId.value)
      .subscribe(result => {
        this.formViews = result.data;
      }, error => console.error(error));
    }        
  }

  changeView(){    
    if(this.form.formId.value != 0 || this.form.formId.value != null )
      this.actionsviewsService.getAllActionsByForm(this.form.formId.value)
        .subscribe(result => {
          this.formActions = result.data;
        }, error => console.error(error));      
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

    let itemModel = new RoleFormArrayActions();

    itemModel.roleId = this.form.roleId.value;
    itemModel.formId = this.form.formId.value;
    itemModel.formActionIds = this.form.actions.value;

    if (itemModel.roleId != 0 || itemModel.roleId != null) {

      this.service.add(itemModel)
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
