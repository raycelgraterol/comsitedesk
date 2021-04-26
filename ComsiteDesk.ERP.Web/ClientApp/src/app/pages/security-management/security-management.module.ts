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

@NgModule({
  declarations: [AdvancedSortableDirective, UsersComponent, RolesComponent],
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
