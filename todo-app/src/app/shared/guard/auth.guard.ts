import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = async () => {
    return true;
};