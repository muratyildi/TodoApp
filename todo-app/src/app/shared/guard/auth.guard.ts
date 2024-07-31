import { inject } from '@angular/core';
import { CanActivateFn, UrlTree } from '@angular/router';
import { AuthRepository } from '../repositories/auth.repository';

export const authGuard: CanActivateFn = async (route, state) => {
  const authRepo = inject(AuthRepository);

  return authRepo.CheckAuth();


};