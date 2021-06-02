import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TicketTypesComponent } from './ticket-types/ticket-types.component';
import { TicketsComponent } from './tickets/tickets.component';

import { UIModule } from 'src/app/shared/ui/ui.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbPaginationModule, NgbTooltipModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';

import { TicketManagementRoutingModule } from './ticket-management-routing.module';
import { TicketStatusComponent } from './ticket-status/ticket-status.component';
import { TicketProcessesComponent } from './ticket-processes/ticket-processes.component';
import { TicketCategoriesComponent } from './ticket-categories/ticket-categories.component';

@NgModule({
  declarations: [TicketTypesComponent, TicketsComponent, TicketStatusComponent, TicketProcessesComponent, TicketCategoriesComponent],
  imports: [
    CommonModule,
    UIModule,
    TicketManagementRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbTooltipModule,
    ReactiveFormsModule,
    NgbPaginationModule,
    NgbTypeaheadModule,
    NgSelectModule
  ]
})
export class TicketManagementModule { }
