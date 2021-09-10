import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UIModule } from 'src/app/shared/ui/ui.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbPaginationModule, NgbProgressbarModule, NgbTooltipModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { AdministrationRoutingModule } from './administration-routing.module';
import { AdvancedSortableDirective } from './advanced-sortable.directive';
import { ClientComponent } from './client/client.component';

@NgModule({
  declarations: [AdvancedSortableDirective, ClientComponent],
  imports: [
    CommonModule,
    UIModule,
    AdministrationRoutingModule,
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
export class AdministrationModule { }
