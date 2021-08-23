import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { Interconnect, IMessageStream } from 'ng-interconnect';

import { Task } from './task.model';
import { environment } from '../../../../environments/environment';

import { DndDropEvent } from 'ngx-drag-drop';
import { ActivatedRoute } from '@angular/router';

import { ProjectModel, SearchResult } from 'src/app/core/models/projects.models';
import { ProjectsService } from 'src/app/core/services/projects.service';
import { AssignmentsService } from 'src/app/core/services/assignments.service';
import { Assignments } from 'src/app/core/models/assignment.models';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

 
  // bread crumb items
  breadCrumbItems: Array<{}>;
  
  //Tasks 
  tables$: Observable<Assignments[]>;
  total$: Observable<number>;
  dataLoading$: Observable<Boolean>;

  public item: Assignments;
  loading = false;
  submit: boolean;
  public innerform: FormGroup;
  
  // Task data
  pendingTasks: Task[];
  inprogressTasks: Task[];
  completedTasks: Task[];

  public urlAPI: string = `${environment.apiUrl}\\`;

  project: ProjectModel;
  //The broadcaster reference
  connectionCollaped: IMessageStream;

  constructor(
    public service: AssignmentsService,
    public projectsService: ProjectsService,
    private interconnect: Interconnect,
    private route: ActivatedRoute,
    public formBuilder: FormBuilder) { 
    this.project = new ProjectModel();
    this.connectionCollaped = this.interconnect.createBroadcaster("showTask");
   }

  async ngOnInit() {

    let projectId = parseInt(this.route.snapshot.paramMap.get('id'));

    this.project = (await this.projectsService.getById(projectId).toPromise()).data;
    
    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'Comesite', path: '/' }, 
      { label: 'Tareas', path: '/', active: true }];

    /**
     * Fetches Data
     */
    this._fetchData();

    this.service.getAll();

    this.tables$ = this.service.tables$;
    this.total$ = this.service.total$;
    this.dataLoading$ = this.service.loading$;

    /**
    * Bootstrap validation form data
    */
     this.innerform = this.formBuilder.group({
      id: [0],
      title: ["", [Validators.required]],
      dueDate: ["", [Validators.required]],
      description: [""],
      projectsId: [0, [Validators.required]],
      taskStatusId: [0, [Validators.required]],
      userId: [0, [Validators.required]]
    });
  }

  /**
   * On task drop event
   */
  onDrop(event: DndDropEvent, targetStatus?: number) {
    this.item = event.data;
    this.item.taskStatusId = targetStatus;
    this.validSubmit();
  }

  /**
   * on dragging task
   * @param item item dragged
   * @param list list from item dragged
   */
  onDragged(item: any, list: any[]) {
  }

  /**
   * Show the task
   */
  onShowTask(currentTask: Assignments = new Assignments()){
    this.connectionCollaped.emit(currentTask);
  }

  /**
   * Fetches the value of kanbanboard data
   */
  private _fetchData() {

  }

  /**
   * Returns form
   */
   get form() {
    return this.innerform.controls;
  }

  /**
   * Bootsrap validation form submit method
   */
  validSubmit() {
    this.loading = true;
    this.submit = true;

    // if (this.innerform.invalid) {
    //   this.loading = false;
    //   return;
    // }

    // this.item.id = parseInt(this.form.id.value);
    // this.item.title = this.form.title.value;
    // this.item.dueDate = this.form.dueDate.value;
    // this.item.description = this.form.description.value;
    // this.item.projectsId = this.form.projectsId.value;
    // this.item.taskStatusId = this.form.taskStatusId.value;
    // this.item.userId = this.form.userId.value;

    if (this.item.id == 0 || this.item.id == null) {

      this.service.add(this.item)
        .subscribe(result => {
          if (result) {
            this.item = result.data;
          }
          this.service.getAll();
          this.loading = false;

        }, error => {
          console.error(error);
          this.loading = false;
        }
        );

    } else {

      this.service.edit(this.item)
        .subscribe(result => {
          if (result) {
            this.item = result.data;
          }
          this.service.getAll();
          this.loading = false;

        }, error => {
          console.error(error);
          this.loading = false;
        }
        );
    }

  }

}
