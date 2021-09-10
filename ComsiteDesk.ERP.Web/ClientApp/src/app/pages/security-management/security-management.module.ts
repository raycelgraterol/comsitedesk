import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgbPaginationModule, NgbTypeaheadModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';

import { UIModule } from '../../shared/ui/ui.module';
import { NgSelectModule } from '@ng-select/ng-select';

import { SecurityManagementRoutingModule } from './security-management-routing.module';
import { AdvancedSortableDirective } from './advanced-sortable.directive';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { ProfileComponent } from './profile/profile.component';

import { ModulesComponent } from './modules/modules.component';
import { ActionsComponent } from './actions/actions.component';
import { FormviewsComponent } from './formviews/formviews.component';
import { ViewsactionsComponent } from './viewsactions/viewsactions.component';
import { FormURIDirective } from './form-uri.directive';
import { RolFormActionsComponent } from './rol-form-actions/rol-form-actions.component';

@NgModule({
  declarations: [
    AdvancedSortableDirective, 
    UsersComponent, 
    RolesComponent, 
    ProfileComponent,
    ModulesComponent, 
    ActionsComponent, 
    FormviewsComponent, 
    ViewsactionsComponent,
    FormURIDirective,
    RolFormActionsComponent
  ],
  imports: [
    CommonModule,
    UIModule,
    SecurityManagementRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbTooltipModule,
    ReactiveFormsModule,
    NgbPaginationModule,
    NgbTypeaheadModule,
    NgSelectModule
  ]
})

export class SecurityManagementModule { }
