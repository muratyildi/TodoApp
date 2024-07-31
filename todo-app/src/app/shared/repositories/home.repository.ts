import { Injectable, inject } from "@angular/core";
import { HomeService } from "../services/home.service";
import { MatTableDataSource } from "@angular/material/table";

@Injectable({
    providedIn: 'root'
})

export class homeRepository {
    homeService = inject(HomeService);
    dataSource = new MatTableDataSource();

    getTodoDatas() {
        this.homeService.GetTodoTasks(1,10).subscribe(data => {
            console.log(data.data.tasks);
            this.dataSource =  new MatTableDataSource(data.data.tasks);
        })
    }

    postNewTodo() {

    }

    PutTodo(id: string) {

    }

    deleteTodo(id: string) {

    }

}