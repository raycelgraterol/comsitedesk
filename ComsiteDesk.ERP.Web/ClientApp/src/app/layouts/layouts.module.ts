import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ClickOutsideModule } from 'ng-click-outside';

import { UIModule } from '../shared/ui/ui.module';
import { LayoutComponent } from './layout.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { TopbarComponent } from './topbar/topbar.component';``
import { FooterComponent } from './footer/footer.component';
import { RightsidebarComponent } from './rightsidebar/rightsidebar.component';
import { SidebarComsiteComponent } from './sidebar-comsite/sidebar-comsite.component';
import { TaskSidebarComponent } from './task-sidebar/task-sidebar.component';

import { ModuleURIDirective } from './sidebar-comsite/module-uri.directive';
import { FormURIDirective } from './sidebar-comsite/form-uri.directive';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [
    ModuleURIDirective,
    FormURIDirective,
    LayoutComponent, 
    SidebarComponent, 
    TopbarComponent, 
    FooterComponent, 
    RightsidebarComponent, 
    SidebarComsiteComponent, TaskSidebarComponent],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    NgSelectModule,
    ReactiveFormsModule,
    NgbDropdownModule,
    ClickOutsideModule,
    UIModule
  ]
})
export class LayoutsModule { }
