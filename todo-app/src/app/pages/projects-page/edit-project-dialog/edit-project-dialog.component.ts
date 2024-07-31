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
import { AuthRepository } from '../../../shared/repositories/auth.repository';

@Component({
  selector: 'app-new-project-dialog',
  standalone: true,
  imports: [MatFormField, ReactiveFormsModule, MatFormFieldModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, CommonModule, MatInputModule, MatButtonModule],
  templateUrl: './edit-project-dialog.component.html',
  styleUrl: './edit-project-dialog.component.scss'
})
export class EditProjectDialogComponent {
  projectForm: FormGroup;
  authRepository = inject(AuthRepository);

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EditProjectDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.projectForm = this.fb.group({
      id: [data.id, Validators.required],
      name: [data.name, Validators.required],
      description: [data.description, Validators.required],
      userIds: [[data.userId], Validators.required],
    });
  }

  onSave() {
    if (this.projectForm.valid) {
      this.dialogRef.close(this.projectForm.value);
    }
  }

  onCancel() {
    this.dialogRef.close();
  }

}
