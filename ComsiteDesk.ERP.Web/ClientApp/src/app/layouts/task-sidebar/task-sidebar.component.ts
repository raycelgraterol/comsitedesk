import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { Interconnect } from "ng-interconnect";

import { Assignments } from 'src/app/core/models/assignment.models';
import { AssignmentsService } from 'src/app/core/services/assignments.service';

import Swal from 'sweetalert2';
import { environment } from 'src/environments/environment';

import { AssignmentStatusService } from 'src/app/core/services/assignment-status.service';
import { UserProfileService } from 'src/app/core/services/user.service';
import { AssignmentStatus } from 'src/app/core/models/assignmentStatus.models';
import { DatePipe } from '@angular/common';
import { User } from 'src/app/core/models/auth.models';

@Component({
  selector: 'app-task-sidebar',
  templateUrl: './task-sidebar.component.html',
  styleUrls: ['./task-sidebar.component.scss']
})
export class TaskSidebarComponent implements OnInit {

  currentTask: Assignments;
  public urlAPI: string = `${environment.apiUrl}\\`;
  public taskStatus: Array<AssignmentStatus>;
  public users: Array<User>;

  loading = false;
  submit: boolean;
  public innerform: FormGroup;

  constructor(private interconnect: Interconnect,
    public service: AssignmentsService,
    public userProfileService: UserProfileService,
    private datePipe: DatePipe,
    public assignmentStatusService: AssignmentStatusService,
    public formBuilder: FormBuilder
  ) {
    this.currentTask = new Assignments();
  }

  ngOnInit() { 
    
    //Load data
    this._fetchData();

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
   * on settings button clicked from topbar
   */
  onSettingsButtonClicked() {
    document.body.classList.toggle('right-bar-enabled');
  }

  /**
   * Hide the sidebar
   */
  public hide() {
    document.body.classList.remove('right-bar-enabled');
  }

  /**
   * fetches project value
   */
   private _fetchData() {

    this.interconnect.receiveFrom("showTask", "app-report-wrapper", data => {
      this.onSettingsButtonClicked();
      this.currentTask = data;

      if (data) {
        this.form.id.setValue(this.currentTask.id);
        this.form.title.setValue(this.currentTask.title);
        this.form.description.setValue(this.currentTask.description);
        this.form.dueDate.setValue(this.datePipe.transform(this.currentTask.dueDate, 'yyyy-MM-dd'));
        this.form.projectsId.setValue(this.currentTask.projectsId);
        this.form.taskStatusId.setValue(this.currentTask.taskStatusId);
        this.form.userId.setValue(this.currentTask.userId);
      }

    });

    this.assignmentStatusService.getAllItems()
      .subscribe(result => {
        this.taskStatus = result.data;
      }, error => console.error(error));
    
    this.userProfileService.getAllUsers()
      .subscribe(result => {
        this.users = result;
      }, error => console.error(error));

  }

  public removeValidators(form: FormGroup) {
    for (const key in form.controls) {
        form.get(key).clearValidators();
        form.get(key).updateValueAndValidity();
    }
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

    if (this.innerform.invalid) {
      this.loading = false;
      return;
    }

    this.currentTask.id = parseInt(this.form.id.value);
    this.currentTask.title = this.form.title.value;
    this.currentTask.dueDate = this.form.dueDate.value;
    this.currentTask.description = this.form.description.value;
    this.currentTask.projectsId = this.form.projectsId.value;
    this.currentTask.taskStatusId = this.form.taskStatusId.value;
    this.currentTask.userId = this.form.userId.value;

    if (this.currentTask.id == 0 || this.currentTask.id == undefined) {

      this.service.add(this.currentTask)
        .subscribe(result => {
          if (result) {
            this.currentTask = result.data;
          }
          this.hide();
          this.loading = false;

          Swal.fire({
            position: 'top-end',
            width: 300,
            type: "success",
            title: "",
            showConfirmButton: false,
            timer: 1000
          });

        }, error => {
          console.error(error);
          this.loading = false;
        }
        );

    } else {

      this.service.edit(this.currentTask)
        .subscribe(result => {
          if (result) {
            this.currentTask = result.data;
          }
          this.hide();
          this.loading = false;

          Swal.fire({
            position: 'top-end',
            width: 300,
            type: "success",
            title: "",
            showConfirmButton: false,
            timer: 1000
          });

        }, error => {
          console.error(error);
          this.loading = false;
        }
        );
    }

  }

}