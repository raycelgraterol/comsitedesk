import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { ListStatusComponent } from './list-status/list-status.component';

@NgModule({
  declarations: [ListComponent, ListStatusComponent],
  imports: [
    CommonModule
  ]
})
export class ProjectsModule { }
