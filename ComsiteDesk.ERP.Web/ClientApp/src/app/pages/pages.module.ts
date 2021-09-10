import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

import { DashboardsModule } from './dashboards/dashboards.module';
import { AppsModule } from './apps/apps.module';
import { UiModule } from './ui/ui.module';
import { IconsModule } from './icons/icons.module';
import { FormModule } from './form/form.module';
import { ChartModule } from './chart/chart.module';
import { TablesModule } from './tables/tables.module';
import { ErrorModule } from './error/error.module';

import { SecurityManagementModule } from './security-management/security-management.module';
import { TicketManagementModule } from './ticket-management/ticket-management.module';

import { PagesRoutingModule } from './pages-routing.module';
import { AssignmentModule } from './assignment/assignment.module';
import { ProjectsModule } from './projects/projects.module';
import { AdministrationModule } from './administration/administration.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgbDropdownModule,
    DashboardsModule,
    AppsModule,
    UiModule,
    IconsModule,
    FormModule,
    ChartModule,
    TablesModule,
    ErrorModule,
    PagesRoutingModule,
    SecurityManagementModule,
    TicketManagementModule,
    AssignmentModule,
    ProjectsModule,
    AdministrationModule
  ]
})
export class PagesModule { }
