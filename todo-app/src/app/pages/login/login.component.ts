import { Component, inject, signal } from '@angular/core';
import { AuthRepository } from '../../shared/repositories/auth.repository';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  AuthRepository = inject(AuthRepository)
  router = inject(Router);
  hide = signal(true);

  loginForm = new FormGroup({
    email: new FormControl('test@user.com', [Validators.required, Validators.email]),
    password: new FormControl('123456', [Validators.required, Validators.minLength(3), Validators.maxLength(20)])
  });


  clickEvent(event: MouseEvent) {
    event.preventDefault();
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  userLogin() {
    if (this.loginForm.valid) {
      this.AuthRepository.UserLogin(this.loginForm.value.email!, this.loginForm.value.password!);
    }
  }




}