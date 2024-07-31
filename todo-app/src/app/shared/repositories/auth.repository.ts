import { Inject, Injectable, PLATFORM_ID, inject } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { environment } from "../../../environments/environment";
import { Router } from "@angular/router";
import { isPlatformBrowser } from "@angular/common";

@Injectable({
    providedIn: 'root'
})
export class AuthRepository {
    public authLocalStorageToken = `${environment.appName}`;
    AuthService = inject(AuthService)
    router = inject(Router)

    constructor(@Inject(PLATFORM_ID) public platformId: any) {
    }

    UserLogin(email: string, password: string) {
        this.AuthService.Login(email, password).subscribe({
            next: (data) => {
                if (data.status == 'success') {
                    this.SetUserLocalStorage(data);
                    this.router.navigate(['/home']);
                } else {
                    throw new Error(data.status);
                }
            }
        })
    }

    SetUserLocalStorage(user: any) {
        return localStorage.setItem(`${this.authLocalStorageToken}`, JSON.stringify(user));
    }

    async CheckAuth(): Promise<boolean> {
        if (isPlatformBrowser(this.platformId)) {
            if (this.getUserInfo) {
                if (this.credInformations != '') {
                    return true;
                } else {
                    await this.LogoutUser();
                    return await false;
                }
            }
            await this.LogoutUser();
            return await false;
        }
        await this.LogoutUser();
        return await false;
    }


    get getUserInfo(): boolean {
        const user = localStorage.getItem(`${this.authLocalStorageToken}`);
        return (user == null || user == undefined || user == '') ? false : true;
    }


    get credInformations() {
        if (localStorage.getItem(`${this.authLocalStorageToken}`) != null) {
            return JSON.parse(localStorage.getItem(`${this.authLocalStorageToken}`) || '');
        } else {
            return '';
        }
    }

    LogoutUser() {
        if (isPlatformBrowser(this.platformId)) {
            localStorage.removeItem(`${this.authLocalStorageToken}`);
            this.router.navigate(['login']);
        }
    }

}