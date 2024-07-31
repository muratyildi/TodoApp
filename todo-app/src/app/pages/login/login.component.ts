import { Component, inject, signal } from '@angular/core';
import { AuthRepository } from '../../shared/repositories/auth.repository';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  AuthRepository = inject(AuthRepository)
  router = inject(Router);

  loginForm = new FormGroup({
    email: new FormControl('test@user.com', [Validators.required, Validators.email]),
    password: new FormControl('123456', [Validators.required, Validators.minLength(3), Validators.maxLength(20)])
  });
  hide = signal(true);

  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  userLogin() {
    this.router.navigate(['home']);
    if (this.loginForm.valid) {

    }


  }







}