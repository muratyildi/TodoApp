import { Component, inject } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SidenavComponent } from '../../shared/components/sidenav/sidenav.component';
import { MatInputModule } from '@angular/material/input';
import { homeRepository } from '../../shared/repositories/home.repository';
import { CommonModule } from '@angular/common';
import { TodoDialogComponent } from './todo-dialog/todo-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  imports: [SidenavComponent, MatTableModule, MatInputModule, CommonModule, MatButtonModule]
})

export class HomeComponent {
  homeRepository = inject(homeRepository);
  dialog = inject(MatDialog);
  displayedColumns: string[] = ['name', 'description', 'startDate', 'endDate', 'actions'];
  sidenavOpened = false;

  ngOnInit() {
    this.getTodoData();
  }

  getTodoData() {
    this.homeRepository.getTodoDatas();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.homeRepository.dataSource.filter = filterValue.trim().toLowerCase();
  }


  openAddTodoDialog() {
    const dialogConfig: MatDialogConfig = {
      width: '780px',
      height: '500px',
      data: {}
    };
    const dialogRef = this.dialog.open(TodoDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.homeRepository.addTodo(result);
      }
    });
  }

  deleteTodo() {

  }
}