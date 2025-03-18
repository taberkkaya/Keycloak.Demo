import { inject } from '@angular/core';
import { CanActivateChildFn, Router } from '@angular/router';
import { jwtDecode, JwtPayload } from 'jwt-decode';

export const authGuard: CanActivateChildFn = (childRoute, state) => {
  const token = localStorage.getItem('accessToken');

  const router = inject(Router);

  if (token == null) {
    router.navigateByUrl('/login');
    return false;
  }

  const decode: JwtPayload | any = jwtDecode(token);

  const exp = decode.exp;
  const now = new Date().getTime() / 1000;

  if (now > exp) {
    router.navigateByUrl('/login');
    return false;
  }

  return true;
};
