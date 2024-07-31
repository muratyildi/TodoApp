import { Component, Inject, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
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
  selector: 'app-todo-dialog-edit',
  standalone: true,
  imports: [MatFormField, ReactiveFormsModule, MatFormFieldModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, CommonModule, MatInputModule, MatButtonModule],
  templateUrl: './todo-dialog-edit.component.html',
  styleUrl: './todo-dialog-edit.component.scss'
})
export class TodoDialogEditComponent {
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
    private dialogRef: MatDialogRef<TodoDialogEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.getProjectIds();
    this.todoForm = this.fb.group({
      id: [data.id || ''],
      name: [data.name || '', Validators.required],
      description: [data.description || '', Validators.required], 
      status: [data.status || 0],
      startDate: [data.startDate || new Date()],
      endDate: [data.endDate || ''],
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
