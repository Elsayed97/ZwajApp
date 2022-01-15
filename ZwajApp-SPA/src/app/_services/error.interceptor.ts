import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()

export class ErrorInterceptor implements HttpInterceptor{
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if(error instanceof HttpErrorResponse){
                    const applicationError = error.headers.get('Application-Error')
                    if(applicationError){
                        return throwError(applicationError);
                    }
                    //Model State Errors
                    const serverErrors = error.error;
                    let modelStateErrors='';
                    if(serverErrors && typeof serverErrors === 'object'){
                        for(const key in serverErrors){
                              if(serverErrors[key]){
                                  modelStateErrors+=serverErrors[key] + '\n';
                              }
                        }
                    }

                    //UnAuthorized Error 
                    if(error.status === 401){
                        return throwError(error.statusText);
                    }
                    return throwError(modelStateErrors || serverErrors || 'Server-Error');
                }

                 
                
            })
        )
    }
}

export const ErrorIntercceptorProvider={
    provide:HTTP_INTERCEPTORS,
    class:ErrorInterceptor,
    multi:true
} 