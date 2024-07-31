import { Component, inject, signal } from '@angular/core';
import { AuthRepository } from '../../shared/repositories/auth.repository';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-register-account',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register-account.component.html',
  styleUrl: './register-account.component.scss'
})
export class RegisterAccountComponent {
  AuthRepository = inject(AuthRepository)
  router = inject(Router);
  hide = signal(true);

  registerForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]),
    fullName: new FormControl('', [Validators.required]),
    username: new FormControl('', [Validators.required]),
  });

  clickEvent(event: MouseEvent) {
    event.preventDefault();
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  userLogin() {
    if (this.registerForm.valid) {
      this.AuthRepository.CreateUser(this.registerForm.value.email!, this.registerForm.value.password!, this.registerForm.value.fullName!, this.registerForm.value.username!);
    }
  }

}
