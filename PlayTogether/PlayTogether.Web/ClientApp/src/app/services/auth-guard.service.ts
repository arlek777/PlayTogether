import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { AuthState } from '../store/auth/reducers';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private store: Store<AuthState>, private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.store.pipe(
      select(state => state.isLoggedIn),
      map(authed => {
        if (!authed) {
          this.router.navigate(['/login']);
          return false;
        }

        return true;
      }),
      take(1)
    );
  }
}
