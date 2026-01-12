import { CanActivateFn } from '@angular/router';
                                        //route, state
export const authGuard: CanActivateFn = () => {
  return !!localStorage.getItem('token');
};
