import { Injectable, inject } from "@angular/core";
import { HomeService } from "../services/home.service";
import { MatTableDataSource } from "@angular/material/table";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
    providedIn: 'root'
})

export class homeRepository {
    homeService = inject(HomeService);
    dataSource = new MatTableDataSource();
    snackBar = inject(MatSnackBar);

    getTodoDatas() {
        this.homeService.GetTodoTasks(1, 10).subscribe(data => {
            this.dataSource = new MatTableDataSource(data.data.tasks);
        })
    }

    postNewTodo() {

    }

    PutTodo(id: string) {

    }

    deleteTodo(id: string) {

    }

    addTodo(todo: any) {
        this.homeService.PostNewTodo(todo).subscribe(data => {
            console.log(data);
            if (data.status == 'success') {
                this.snackBar.open('Başarıyla Eklendi');
            }
        })
    }

}