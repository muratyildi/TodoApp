import { inject } from '@angular/core';
import { HttpRequest, HttpHandlerFn, HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthRepository } from '../repositories/auth.repository';

export const apiInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> => {
  const authRepository = inject(AuthRepository);
  const token = authRepository.credInformations.data.token
  const clonedReq = req.clone({ headers: req.headers.set('Authorization', `Bearer ${token}`) });
  return next(clonedReq);
};
