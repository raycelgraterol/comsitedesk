import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgbPaginationModule, NgbTypeaheadModule, NgbTooltipModule, NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';

import { UIModule } from '../../shared/ui/ui.module';
import { NgSelectModule } from '@ng-select/ng-select';

import { ListComponent } from './list/list.component';
import { ListStatusComponent } from './list-status/list-status.component';
import { AdvancedSortableDirective } from './advanced-sortable.directive';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ProjectsRoutingModule } from './projects-routing.module';

@NgModule({
  declarations: [AdvancedSortableDirective, ListComponent, ListStatusComponent],
  imports: [
    CommonModule,
    UIModule,
    ProjectsRoutingModule,
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
export class ProjectsModule { }
