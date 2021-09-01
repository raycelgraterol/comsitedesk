import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HeadquarterComponent } from './headquarter/headquarter.component';
import { DepartmentComponent } from './department/department.component';
import { EquipmentUserComponent } from './equipment-user/equipment-user.component';
import { EquipmentComponent } from './equipment/equipment.component';

import { UIModule } from 'src/app/shared/ui/ui.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbPaginationModule, NgbProgressbarModule, NgbTooltipModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { ConfigurationRoutingModule } from './configuration-routing.module';
import { AdvancedSortableDirective } from './advanced-sortable.directive';

@NgModule({
  declarations: [AdvancedSortableDirective, HeadquarterComponent, DepartmentComponent, EquipmentUserComponent, EquipmentComponent],
  imports: [
    CommonModule,
    UIModule,
    ConfigurationRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbTooltipModule,
    NgbProgressbarModule,
    ReactiveFormsModule,
    NgbPaginationModule,
    NgbTypeaheadModule,
    NgSelectModule
  ]
})
export class ConfigurationModule { }
