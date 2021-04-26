import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from '../services/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                let error = "Acceso no autorizado, compruebe sus datos.";

                if(err.error != null && err.error.message != null){
                    error = err.error.message;
                }
                // auto logout if 401 response returned from api
                
                return throwError(error);
            }

            if(err.message.includes("Http failure response") && err.status === 0){
                const error = "Falló la conexión con el servidor";
                return throwError(error);
            }

            const error = err.error.message || err.statusText;
            return throwError(error);
        }));
    }
}
