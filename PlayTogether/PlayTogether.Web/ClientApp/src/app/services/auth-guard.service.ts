import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { AppState } from '../store';
import { UserType } from '../models/user-type';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private store: Store<AppState>, private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.store.pipe(
      select(state => state.user),
      map(user => {
        if (!user.isLoggedIn) {
          this.router.navigate(['/login']);
          return false;
        } 

        return true;
      }),
      take(1)
    );
  }
}
