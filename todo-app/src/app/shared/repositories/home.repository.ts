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

    deleteTodo(id: number) {
        if (confirm('Silmek istiyor musunuz?')) {
            this.homeService.DeleteTodo(id).subscribe(data => {
                if (data.status == 'success') {
                    this.snackBar.open('Başarıyla Silindi', 'Ok');
                    this.getTodoDatas();
                } else {
                    this.snackBar.open(data.message, 'Ok');
                }
            });
        }
    }

    addTodo(todo: any) {
        this.homeService.PostNewTodo(todo).subscribe(data => {
            if (data.status == 'success') {
                this.snackBar.open('Başarıyla Eklendi','Ok');
                this.getTodoDatas();
            } else {
                this.snackBar.open(data.message, 'ok')
            }
        })
    }

    updateTodo(data: any) {
        const formData = new FormData();
        
        formData.append('id',data.id);
        formData.append('Name',data.name);
        formData.append('Description',data.description);
        formData.append('Status',data.status.toString());
        formData.append('Name',data.name);
        formData.append('StartDate',data.startDate);
        formData.append('EndDate',data.endDate);

        this.homeService.PutTodo(formData).subscribe(data => {
            if (data.status == 'success') {
                this.snackBar.open('Başarıyla Eklendi','Ok');
                this.getTodoDatas();
            } else {
                this.snackBar.open(data.message, 'ok')
            }
        })
    }

}