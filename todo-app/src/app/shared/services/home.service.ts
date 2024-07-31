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


}