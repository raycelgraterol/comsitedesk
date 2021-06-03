import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../../core/guards/auth.guard';

import { TicketTypesComponent } from './ticket-types/ticket-types.component';
import { TicketsComponent } from './tickets/tickets.component';
import { TicketStatusComponent } from './ticket-status/ticket-status.component';
import { TicketProcessesComponent } from './ticket-processes/ticket-processes.component';
import { TicketCategoriesComponent } from './ticket-categories/ticket-categories.component';

const routes: Routes = [
  {
    path: 'tickets',
    component: TicketsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'types',
    component: TicketTypesComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'status',
    component: TicketStatusComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'processes',
    component: TicketProcessesComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'categories',
    component: TicketCategoriesComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TicketManagementRoutingModule { }
