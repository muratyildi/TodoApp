import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  http = inject(HttpClient);

  GetTodoTasks(pageSize: number, size: number) {
    return this.http.get<any>(environment.apiUrl + `/Task?page=${pageSize}&size=${size}`);
  }

  PostNewTodo(data: any) {
    return this.http.post<any>(environment.apiUrl + '/Task', data);
  }

  GetProjectIds() {
    return this.http.get<any>(environment.apiUrl + `/Project`);
  }

  DeleteTodo(id: number) {
    return this.http.delete<any>(environment.apiUrl + `/Task/${id}`);
  }

  PutTodo(data: any) {
    return this.http.put<any>(environment.apiUrl + '/Task', data);
  }

}