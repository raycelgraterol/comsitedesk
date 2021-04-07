import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../../environments/environment';

import { CookieService } from '../services/cookie.service';
import { User } from '../models/auth.models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    user: User;
    userType: string;

    constructor(private http: HttpClient, private cookieService: CookieService) {
    }

    /**
     * Returns the current user
     */
    public currentUser(): User {
        if (!this.user) {
            this.user = JSON.parse(this.cookieService.getCookie('currentUser'));
        }
        return this.user;
    }

    /**
     * Return token
     */
    public currentUserToken(): string {
        let token = this.cookieService.getCookie('userToken');
        return token;
    }

    /**
     * Get all Statesc
     * 
     */
    public getAll() {
        this.http.get<any>(`${environment.apiUrl}/api/States`)
        .subscribe(result => {
        console.log(result.data);
        }, error => {
        console.error(error);
        }
        );
    }


    /**
     * Returns current rol
     */
    getUserType() {
        return (this.user === undefined || this.user.roles.length == 0) ? '' :  this.user.roles[0];
    }

    /**
     * Performs the auth
     * @param email email of user
     * @param password password of user
     */
    login(username: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/api/Authenticate/login`, { username, password })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    this.user = user;
                    //Set token
                    this.cookieService.setCookie('userToken', user.token, 1);
                    // store user details and jwt in cookie
                    this.cookieService.setCookie('currentUser', JSON.stringify(user), 1);
                }
                return user;
            }));
    }

    /**
     * Performs the auth
     * @param email email of user
     * @param password password of user
     */
    resetPassword(username: string, password: string = "123") {
        return this.http.post<any>(`${environment.apiUrl}/api/authenticate/reset-password`, { username, password })
            .pipe(map(result => {                
                return result;
            }));
    }

    /**
     * Register Admin
     */
    register(firstName: string, lastName: string, password: string, email: string, phoneNumber: string, organizationId: number, keyAccess: string = "") {

        let urlRegister = (keyAccess != "" && keyAccess != null && keyAccess != undefined) ?
                            `${environment.apiUrl}/api/authenticate/register-admin` : `${environment.apiUrl}/api/authenticate/register`;

        return this.http.post<any>(urlRegister, { email, password, firstName, lastName, phoneNumber, organizationId, keyAccess })
            .pipe(map(user => {
                if (user && user.token) {
                    this.user = user;
                    this.cookieService.setCookie('currentUser', JSON.stringify(user), 1);
                }
                return user;
            }
            ));
    }

    /**
     * Create a new organization
     */
     registerOrganization(businessName: string, RIF: string, email: string, phoneNumber: string, address: string, keyAccess: string,) {
         
        return this.http.post<any>(`${environment.apiUrl}/api/organizations`, 
        {
            businessName, 
            RIF, 
            email, 
            phoneNumber, 
            address,
            keyAccess 
        })
            .pipe(map(data => {
                
                return data;
            }
            ));
    }

    /**
     * Update Profile
     */
    updateProfile(UserName: string, FirstName: string, LastName: string, PhoneNumber: string) {
        const formData: FormData = new FormData();
        formData.append('UserName', UserName);
        formData.append('FirstName', FirstName);
        formData.append('LastName', LastName);
        formData.append('PhoneNumber', PhoneNumber);

        return this.http.post<any>(`${environment.apiUrl}/api/authenticate/update`, formData)
            .pipe(map(_result => {
                if (_result.user) {
                    this.user.phoneNumber = _result.user.phoneNumber;
                    this.user.firstName = _result.user.firstName;
                    this.user.lastName = _result.user.lastName;
                    // TODO: Set Correct values
                    // this.cookieService.setCookie('currentUser', JSON.stringify(_result.user), 1);
                }
                return _result;
            }
            ));
    }
    
    /**
     * Performs the auth
     * @param email email of user
     */
     forgotPassword(email: string) {
        return this.http.post<any>(`${environment.apiUrl}/api/authenticate/forgot-password`, { email })
            .pipe(map(result => {
                return result;
            }));
    }

    /**
     * Change the password
     * @param email 
     * @param password
     */
     changePassword(email: string, password: string, token: string) {
        return this.http.post<any>(`${environment.apiUrl}/api/authenticate/reset-password`, { email, password, token })
            .pipe(map(_result => {
                return _result;
            }));
    }

    /**
    * Get all Users
    */
    getAllUsers()
    {
        return this.http.get<any>(`${ environment.apiUrl}/api/authenticate/users`);
    }
        
    /**
     * Logout the user
     */
    logout() {
        // remove user from local storage to log user out
        this.cookieService.deleteCookie('currentUser');
        this.user = null;
    }
}

