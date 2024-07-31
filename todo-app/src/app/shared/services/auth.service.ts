import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    http = inject(HttpClient);

    Login(email: string, password: string): Observable<any> {
        return this.http.post<any>(environment.apiUrl + '/Auth/Login',
            {
                email: email,
                password: password
            }
        )
    }


    CreateUser(email: string, password: string, fullName: string, username: string): Observable<any> {
        return this.http.post<any>(environment.apiUrl + '/Account',
            {
                email: email,
                password: password,
                fullName: fullName,
                username: username
            }
        )
    }

}