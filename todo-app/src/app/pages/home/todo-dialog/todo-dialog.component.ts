import { Component, inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormField } from '@angular/material/form-field';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HomeService } from '../../../shared/services/home.service';

@Component({
  selector: 'app-todo-dialog',
  standalone: true,
  imports: [MatFormField, ReactiveFormsModule, MatFormFieldModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, CommonModule, MatInputModule, MatButtonModule],
  templateUrl: './todo-dialog.component.html',
  styleUrl: './todo-dialog.component.scss'
})
export class TodoDialogComponent {
  todoForm: FormGroup;
  homeService = inject(HomeService);
  projects: any[] = [];
  statuses = [
    { value: 0, label: 'Beklemede' },
    { value: 2, label: 'İşlemde' },
    { value: 3, label: 'Tamamlandı' },
    { value: 4, label: 'Onay Aşamasında' },
    { value: 5, label: 'Tamamlandı' }
  ];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<TodoDialogComponent>
  ) {
    this.getProjectIds();
    this.todoForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      status: [0, Validators.required],
      startDate: [new Date().toISOString()],
      endDate: [new Date().toISOString()],
      projectId: [1, Validators.required]
    });
  }

  getProjectIds() {
    this.homeService.GetProjectIds().subscribe(data => {
      this.projects = data.data.projects;
    })
  }

  onSave() {
    if (this.todoForm.valid) {
      this.dialogRef.close(this.todoForm.value);
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
