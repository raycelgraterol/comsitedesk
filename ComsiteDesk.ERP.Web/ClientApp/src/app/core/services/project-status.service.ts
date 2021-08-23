import { Injectable, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { environment } from '../../../environments/environment';

import { BehaviorSubject, Observable, of, Subject, Subscription } from 'rxjs';

import { HttpClient } from '@angular/common/http';

import { SortDirection } from '../../pages/projects/advanced-sortable.directive';
import { ProjectModel, SearchResult } from '../models/projects.models';


interface State {
  page: number;
  pageSize: number;
  searchTerm: string;
  sortColumn: string;
  sortDirection: SortDirection;
  startIndex: number;
  endIndex: number;
  totalRecords: number;
  parentId: number;
}

function compare(v1, v2) {
  return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
}

/**
* Sort the table data
* @param tabless Table field value
* @param column Fetch the column
* @param direction Sort direction Ascending or Descending
*/
function sort(tables: ProjectModel[], column: string, direction: string): ProjectModel[] {
  if (direction === '') {
    return tables;
  } else {
    return [...tables].sort((a, b) => {
      const res = compare(a[column], b[column]);
      return direction === 'asc' ? res : -res;
    });
  }
}

/**
* Table Data Match with Search input
* @param tables Table field value fetch
* @param term Search the value
*/
function matches(tables: ProjectModel, term: string, pipe: PipeTransform) {
  term = term.toLocaleLowerCase();
  return tables.title.toLowerCase().includes(term)
    || pipe.transform(tables.id).includes(term);
}

@Injectable({
  providedIn: 'root'
})
export class ProjectStatusService {


  // tslint:disable-next-line: variable-name
  private _loading$ = new BehaviorSubject<boolean>(true);
  // tslint:disable-next-line: variable-name
  private _search$ = new Subject<void>();
  // tslint:disable-next-line: variable-name
  private _tables$ = new BehaviorSubject<ProjectModel[]>([]);
  // tslint:disable-next-line: variable-name
  private _total$ = new BehaviorSubject<number>(0);

  // tslint:disable-next-line: variable-name
  private _state: State = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: '',
    startIndex: 1,
    endIndex: 10,
    totalRecords: 0,
    parentId: 0
  };

  searchSubscription: Subscription;

  tableData: Array<ProjectModel>;
  item: ProjectModel;

  constructor(private pipe: DecimalPipe, private http: HttpClient) {

    //Get All init
    this._loading$.next(true);

  }

  /**
   * Returns the value
   */
  get tables$() { return this._tables$.asObservable(); }
  get total$() { return this._total$.asObservable(); }
  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get searchTerm() { return this._state.searchTerm; }
  get startIndex() { return this._state.startIndex; }
  get endIndex() { return this._state.endIndex; }
  //sorting
  get sortColumn() { return this._state.sortColumn; }
  get sortDirection() { return this._state.sortDirection; }

  get totalRecords() { return this._state.totalRecords; }
  get parentId() { return this._state.parentId; }

  /**
   * set the value
   */
  // tslint:disable-next-line: adjacent-overload-signatures
  set page(page: number) {
    this._set({ page });
    this.getAll();
  }
  // tslint:disable-next-line: adjacent-overload-signatures
  set pageSize(pageSize: number) {
    this._set({ pageSize });
    this.getAll();
  }
  // tslint:disable-next-line: adjacent-overload-signatures
  // tslint:disable-next-line: adjacent-overload-signatures
  set startIndex(startIndex: number) { this._set({ startIndex }); }
  // tslint:disable-next-line: adjacent-overload-signatures
  set endIndex(endIndex: number) { this._set({ endIndex }); }
  // tslint:disable-next-line: adjacent-overload-signatures
  set totalRecords(totalRecords: number) { this._set({ totalRecords }); }
  // tslint:disable-next-line: adjacent-overload-signatures
  set searchTerm(searchTerm: string) {
    this._set({ searchTerm });
    this.getAll();
  }
  set sortColumn(sortColumn: string) {
    this._set({ sortColumn });
    this.getAll();
  }
  set sortDirection(sortDirection: SortDirection) {
    this._set({ sortDirection });
    this.getAll();
  }

  // tslint:disable-next-line: adjacent-overload-signatures
  set parentId(parentId) {
    this._set({ parentId });
  }

  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  /**
   * Search Method
   */
  private _search(): Observable<SearchResult> {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    let tables = new Array<ProjectModel>();

    if (this.tableData !== undefined) {
      tables = this.tableData;
    }

    // 3. paginate
    const total = this.totalRecords;
    this._state.startIndex = (page - 1) * this.pageSize + 1;
    this._state.endIndex = (page - 1) * this.pageSize + this.pageSize;
    if (this.endIndex > this.totalRecords) {
      this.endIndex = this.totalRecords;
    }

    return of(
      { tables, total }
    );
  }

  /**
   * Get all
   * 
   */
  public getAll() {
    this._loading$.next(true);

    this.http.get<any>(`${environment.apiUrl}/api/ProjectStatus?Page=` + this.page
      + `&PageSize=` + this.pageSize
      + `&searchTerm=` + this.searchTerm
      + `&sortColumn=` + this.sortColumn
      + `&sortDirection=` + this.sortDirection)
      .subscribe(result => {
        this._tables$.next(result.data);
        this._total$.next(result.count);

        this.totalRecords = result.count;
        this.updatePager();

        this._loading$.next(false);
      }, error => {
        console.error(error);
        this._loading$.next(false);
      }
      );
  }

  /**
    * Get all items
    */
  getAllItems() {
    return this.http.get<any>(`${environment.apiUrl}/api/ProjectStatus/All`);
  }

  /**
   * Update the pager
   */
  public updatePager() {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    //paginate
    const total = this.totalRecords;
    this._state.startIndex = (page - 1) * this.pageSize + 1;
    this._state.endIndex = (page - 1) * this.pageSize + this.pageSize;
    if (this.endIndex > this.totalRecords) {
      this.endIndex = this.totalRecords;
    }
  }

  /**
   * Get by id
   */
  public getById(id: number) {
    return this.http.get<any>(`${environment.apiUrl}/api/ProjectStatus/` + id);
  }

  /**
   * Add item
   */
  public add(_projects: ProjectModel) {
    //Map
    let id = _projects.title;
    let name = _projects.description;

    return this.http.post<any>(`${environment.apiUrl}/api/ProjectStatus/`, {
      name
    });
  }

  /**
   * Edit item
   */
  public edit(_projects: ProjectModel) {

    //Map
    let id = _projects.title;
    let name = _projects.description;

    return this.http.put<any>(`${environment.apiUrl}/api/ProjectStatus/` + id, {
      id,
      name
    });
  }

  /**
   * Delete record
   * @param id 
   */
  delete(id: number) {
    return this.http.delete<any>(`${environment.apiUrl}/api/ProjectStatus/` + id);
  }

}