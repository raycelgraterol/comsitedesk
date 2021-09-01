import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../../core/guards/auth.guard';
import { DepartmentComponent } from './department/department.component';
import { EquipmentUserComponent } from './equipment-user/equipment-user.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { HeadquarterComponent } from './headquarter/headquarter.component';

const routes: Routes = [
  {
    path: 'headquarter',
    component: HeadquarterComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'department',
    component: DepartmentComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'equipment',
    component: EquipmentComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'equipmentUser',
    component: EquipmentUserComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
