import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { authGuard } from './shared/guard/auth.guard';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { RegisterAccountComponent } from './pages/register-account/register-account.component';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'home'
    },
    {
        path: 'home',
        pathMatch: 'full',
        canActivate: [authGuard],
        title: 'Anasayfa',
        component: HomeComponent
    },
    {
        path: 'login',
        pathMatch: 'full',
        title: 'Giriş',
        component: LoginComponent
    },
    {
        path: 'register',
        pathMatch: 'full',
        title: 'Kayıt Ol',
        component: RegisterAccountComponent
    },
    {
        path: '404',
        title: 'Bulunamadı',
        component: NotFoundComponent
    },
    { path: '**', redirectTo: '404' }
];