import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../../core/guards/auth.guard';
import { ListStatusComponent } from './list-status/list-status.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [
  {
    path: 'list/:id',
    component: ListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'status',
    component: ListStatusComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AssignmentRoutingModule { }
