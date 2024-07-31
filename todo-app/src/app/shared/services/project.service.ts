import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class ProjectService {
    http = inject(HttpClient);

    GetProjects(pageSize: number, size: number) {
        return this.http.get<any>(environment.apiUrl + `/Project?page=${pageSize}&size=${size}`);
    }

    PostNewProject(data: any) {
        return this.http.post<any>(environment.apiUrl + '/Project', data);
    }

    DeleteProject(id: number) {
        return this.http.delete<any>(environment.apiUrl + `/Project/${id}`);
    }

    PutProject(data: any) {
        return this.http.put<any>(environment.apiUrl + '/Project', data);
    }

}