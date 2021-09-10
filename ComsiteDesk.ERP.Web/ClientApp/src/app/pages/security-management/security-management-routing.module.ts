import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../../core/guards/auth.guard';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { ProfileComponent } from './profile/profile.component';
import { ModulesComponent } from './modules/modules.component';
import { FormviewsComponent } from './formviews/formviews.component';
import { ActionsComponent } from './actions/actions.component';
import { RolFormActionsComponent } from './rol-form-actions/rol-form-actions.component';
import { ViewsactionsComponent } from './viewsactions/viewsactions.component';

const routes: Routes = [
  {
    path: 'users',
    component: UsersComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'roles',
    component: RolesComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'modules',
    component: ModulesComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'formviews',
    component: FormviewsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'actions',
    component: ActionsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'views-actions/:id',
    component: ViewsactionsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'rol-views-actions/:id',
    component: RolFormActionsComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityManagementRoutingModule { }
