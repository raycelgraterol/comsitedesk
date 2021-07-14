import { Component, OnInit } from '@angular/core';
import { tasks } from './data';

import { Task } from './task.model';

import { DndDropEvent } from 'ngx-drag-drop';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

 
  // bread crumb items
  breadCrumbItems: Array<{}>;

  // Task data
  pendingTasks: Task[];
  inprogressTasks: Task[];
  completedTasks: Task[];

  constructor() { }

  ngOnInit() {
    // tslint:disable-next-line: max-line-length
    this.breadCrumbItems = [
      { label: 'Comesite', path: '/' }, 
      { label: 'Tareas', path: '/', active: true }];

    /**
     * Fetches Data
     */
    this._fetchData();
  }

  /**
   * On task drop event
   */
  onDrop(event: DndDropEvent, filteredList?: any[], targetStatus?: string) {
    if (filteredList && event.dropEffect === 'move') {
      let index = event.index;

      if (typeof index === 'undefined') {
        index = filteredList.length;
      }

      filteredList.splice(index, 0, event.data);
    }
  }

  /**
   * on dragging task
   * @param item item dragged
   * @param list list from item dragged
   */
  onDragged(item: any, list: any[]) {
    const index = list.indexOf(item);
    list.splice(index, 1);
  }

  /**
   * Fetches the value of kanbanboard data
   */
  private _fetchData() {
    // all tasks
    this.pendingTasks = tasks.filter(t => t.status === 'pending');
    this.inprogressTasks = tasks.filter(t => t.status === 'inprogress');
    this.completedTasks = tasks.filter(t => t.status === 'completed');
  }

}
