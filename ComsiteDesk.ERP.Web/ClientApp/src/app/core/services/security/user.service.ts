import { Injectable, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { environment } from '../../../../environments/environment';

import { BehaviorSubject, Observable, of, Subject, Subscription } from 'rxjs';
import { debounceTime, delay, map, switchMap, tap } from 'rxjs/operators';

import { HttpClient } from '@angular/common/http';

import { SortDirection } from '../../../pages/security-management/advanced-sortable.directive';
import { User, SearchResult } from '../../models/auth.models';


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
function sort(tables: User[], column: string, direction: string): User[] {
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
function matches(tables: User, term: string, pipe: PipeTransform) {
    term = term.toLocaleLowerCase();
    return tables.fullName.toLowerCase().includes(term)
        || tables.phoneNumber.toLowerCase().includes(term)
        || pipe.transform(tables.id).includes(term);
}

@Injectable({ providedIn: 'root' })
export class UserProfileService {

    // tslint:disable-next-line: variable-name
    private _loading$ = new BehaviorSubject<boolean>(true);
    // tslint:disable-next-line: variable-name
    private _search$ = new Subject<void>();
    // tslint:disable-next-line: variable-name
    private _tables$ = new BehaviorSubject<User[]>([]);
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

    tableData: Array<User>;
    item: User;

    constructor(private pipe: DecimalPipe, private http: HttpClient) {

        //Get All States init
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

        let tables = new Array<User>();

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
     * Get all Clients
     * 
     */
    public getAll() {

        this.http.get<any>(`${environment.apiUrl}/api/Authenticate/users?Page=` + this.page
            + `&PageSize=` + this.pageSize
            + `&searchTerm=` + this.searchTerm
            + `&sortColumn=` + this.sortColumn
            + `&sortDirection=` + this.sortDirection
            + `&organizationId=` + this.parentId)
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
     * Get all ProviderTypes
     */
    getAllClients() {
        return this.http.get<any>(`${environment.apiUrl}/api/Authenticate/all`);
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
        return this.http.get<any>(`${environment.apiUrl}/api/Authenticate/` + id);
    }

    /**
     * Add item
     */
    public add(_user: User) {
        //Map
        let userName = _user.userName;
        let firstName = _user.firstName;
        let lastName = _user.lastName;
        let phoneNumber = _user.phoneNumber;
        let email = _user.email;
        let password = _user.password;
        let organizationId = _user.organizationId;
        let rolName = _user.rolName;

        return this.http.post<any>(`${environment.apiUrl}/api/authenticate/register`, { 
            firstName, 
            lastName, 
            userName,
            email,           
            phoneNumber,
            password,
            rolName,
            organizationId
        });
    }

    /**
     * Edit item
     */
    public edit(_user: User) {

        //Map
        let id = _user.id;
        let userName = _user.userName;
        let firstName = _user.firstName;
        let lastName = _user.lastName;
        let phoneNumber = _user.phoneNumber;
        let email = _user.email;
        let rolName = _user.rolName;

        return this.http.put<any>(`${environment.apiUrl}/api/Authenticate/` + id, {
            userName,
            firstName,
            lastName,
            phoneNumber,
            email,
            rolName
        });
    }

    /**
      * Get all users
      */
    getAllUsers() {
        return this.http.get<any>(`${environment.apiUrl}/api/Authenticate/users`);
    }    

    /**
     * Delete record
     * @param id 
     */
    delete(id: number) {
        return this.http.delete<any>(`${environment.apiUrl}/api/Authenticate/` + id);
    }

}