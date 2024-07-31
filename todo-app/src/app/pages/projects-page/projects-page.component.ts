import { Component, inject } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SidenavComponent } from '../../shared/components/sidenav/sidenav.component';
import { MatInputModule } from '@angular/material/input';
import { homeRepository } from '../../shared/repositories/home.repository';
import { CommonModule } from '@angular/common';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { projectRepository } from '../../shared/repositories/projects.repository';
import { NewProjectDialogComponent } from './new-project-dialog/new-project-dialog.component';
import { EditProjectDialogComponent } from './edit-project-dialog/edit-project-dialog.component';

@Component({
  selector: 'app-projects-page',
  standalone: true,
  imports: [SidenavComponent, MatTableModule, MatInputModule, CommonModule, MatButtonModule],
  templateUrl: './projects-page.component.html',
  styleUrl: './projects-page.component.scss'
})
export class ProjectsPageComponent {
  projectRepository = inject(projectRepository);
  dialog = inject(MatDialog);
  displayedColumns: string[] = ['name', 'description', 'actions'];


  ngOnInit() {
    this.getAllProjects();
  }

  getAllProjects() {
    this.projectRepository.getProjects();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.projectRepository.dataSource.filter = filterValue.trim().toLowerCase();
  }

  deleteProject(id: number) {
    this.projectRepository.deleteProject(id);
  }

  addNewProject() {
    const dialogConfig: MatDialogConfig = {
      width: '780px',
      height: '500px',
      data: {}
    };
    const dialogRef = this.dialog.open(NewProjectDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.projectRepository.addNewProject(result);
      }
    });
  }

  editProjectDialog(id: number, name: string, description: string, userId: any) {
    const dialogConfig: MatDialogConfig = {
      width: '780px',
      height: '500px',
      data: { id, name, description, userId }
    };
    const dialogRef = this.dialog.open(EditProjectDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.projectRepository.updateProject(result);
      }
    });
  }

}
