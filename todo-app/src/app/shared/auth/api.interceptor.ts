import { inject } from '@angular/core';
import { HttpRequest, HttpHandlerFn, HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthRepository } from '../repositories/auth.repository';

export const apiInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> => {
  const authRepository = inject(AuthRepository);
  const token = authRepository?.credInformations?.data?.token;
  const url = req.url;
  const excludedUrls = ['/Account', '/Auth/Login'];

  if (!excludedUrls.some(excludedUrl => url.includes(excludedUrl))) {
    const clonedReq = req.clone({ headers: req.headers.set('Authorization', `Bearer ${token}`) });
    return next(clonedReq);
  }

  return next(req);
};
