import { Injectable, inject } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ProjectService } from "../services/project.service";

@Injectable({
    providedIn: 'root'
})

export class projectRepository {
    ProjectService = inject(ProjectService);
    dataSource = new MatTableDataSource();
    snackBar = inject(MatSnackBar);

    getProjects() {
        this.ProjectService.GetProjects(1, 10).subscribe(data => {
            this.dataSource = new MatTableDataSource(data.data.projects);
        })
    }

    deleteProject(id: number) {
        if (confirm('Silmek istiyor musunuz?')) {
            this.ProjectService.DeleteProject(id).subscribe(data => {
                if (data.status == 'success') {
                    this.snackBar.open('Başarıyla Silindi', 'Ok');
                    this.getProjects();
                } else {
                    this.snackBar.open(data.message, 'ok');
                }
            });
        }
    }

    addNewProject(todo: any) {
        this.ProjectService.PostNewProject(todo).subscribe(data => {
            if (data.status == 'success') {
                this.snackBar.open('Başarıyla Eklendi', 'Ok');
                this.getProjects();
            } else {
                this.snackBar.open(data.message, 'ok')
            }
        })
    }

    updateProject(data: any) {
        const formData = new FormData();

        formData.append('id', data.id);
        formData.append('Name', data.name);
        formData.append('Description', data.description);

        this.ProjectService.PutProject(formData).subscribe(data => {
            if (data.status == 'success') {
                this.snackBar.open('Başarıyla Eklendi', 'Ok');
                this.getProjects();
            } else {
                this.snackBar.open(data.message, 'ok')
            }
        })
    }

}