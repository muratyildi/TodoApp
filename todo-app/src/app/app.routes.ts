import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { authGuard } from './shared/guard/auth.guard';
import { NotFoundComponent } from './pages/not-found/not-found.component';

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
        path: '404',
        title: 'Bulunamadı',
        component: NotFoundComponent
    },
    { path: '**', redirectTo: '404' }
];