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
        return this.http.post<any>(environment.apiUrl + '/v1/Account',
            {
                email: email,
                password: password
            }
        )
    }

}