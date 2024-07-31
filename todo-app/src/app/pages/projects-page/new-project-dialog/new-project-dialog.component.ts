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
import { AuthRepository } from '../../../shared/repositories/auth.repository';

@Component({
  selector: 'app-new-project-dialog',
  standalone: true,
  imports: [MatFormField, ReactiveFormsModule, MatFormFieldModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, CommonModule, MatInputModule, MatButtonModule],
  templateUrl: './new-project-dialog.component.html',
  styleUrl: './new-project-dialog.component.scss'
})
export class NewProjectDialogComponent {
  projectForm: FormGroup;
  authRepository = inject(AuthRepository);

  userId = this.authRepository?.credInformations?.data?.user.accuntId;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<NewProjectDialogComponent>
  ) {
    this.projectForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      userIds: [[this.userId], Validators.required],
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
