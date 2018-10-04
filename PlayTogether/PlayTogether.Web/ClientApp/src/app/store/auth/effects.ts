import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, exhaustMap, map, tap } from 'rxjs/operators';
import { AuthActionTypes, LoginSuccess, Login, AutoLogin, Logout } from './actions';
import { BackendService } from '../../services/backend.service';
import { LoginModel } from '../../models/login';
import { Constants } from '../../constants';
import { JwtTokens } from '../../models/jwt-tokens';

@Injectable()
export class AuthEffects {
  constructor(
    private readonly actions$: Actions,
    private readonly backendService: BackendService,
    private readonly router: Router
  ) { }

  @Effect()
  login$ = this.actions$.pipe(
      ofType<Login>(AuthActionTypes.Login),
    map(action => action.payload),
      exhaustMap((login: LoginModel) =>
        this.backendService.login(login).pipe(
          map(jwtToken => new LoginSuccess(jwtToken)))
      )
  );

  @Effect()
  autoLogin$ = this.actions$.pipe(
    ofType<AutoLogin>(AuthActionTypes.AutoLogin),
      map((action: AutoLogin) => {
        const accessToken = window.localStorage.getItem(Constants.accessTokenKey);
        const user = window.localStorage.getItem(Constants.currentUserKey);

        if (!accessToken || !user) {
          return new Logout();
        } else {
          return new LoginSuccess(new JwtTokens(accessToken, JSON.parse(user)));
        }
      })
  );

  @Effect({ dispatch: false })
  $loginSuccess = this.actions$.pipe(
    ofType(AuthActionTypes.LoginSuccess),
      tap((action: LoginSuccess) => {
        const accessToken = window.localStorage.getItem(Constants.accessTokenKey);
        const user = window.localStorage.getItem(Constants.currentUserKey);
        if (!accessToken && !user) {
          window.localStorage.setItem(Constants.accessTokenKey, action.payload.accessToken);
          window.localStorage.setItem(Constants.currentUserKey, JSON.stringify(action.payload.user));
        }
        if (action.payload.user.isNewUser) {
          this.router.navigate(['/profile']);
        } else {
          this.router.navigate(['/']);
        }
      })
  );

  @Effect({ dispatch: false })
  logout$ = this.actions$.pipe(
    ofType(AuthActionTypes.Logout),
      tap(action => {
        window.localStorage.clear();
        window.sessionStorage.clear();
      this.router.navigate(['/login']);
    })
  );
}
