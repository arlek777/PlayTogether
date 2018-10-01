import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, exhaustMap, map, tap } from 'rxjs/operators';
import { AuthActionTypes, LoginSuccess, LoginStarted } from './actions';
import { BackendService } from '../../services/backend.service';
import { LoginModel } from 'src/app/models/login';

@Injectable()
export class AuthEffects {
  constructor(
    private readonly actions$: Actions,
    private readonly backendService: BackendService,
    private readonly router: Router
  ) { }

  @Effect()
  login$ = this.actions$.pipe(
      ofType<LoginStarted>(AuthActionTypes.LoginStarted),
    map(action => action.payload),
      exhaustMap((login: LoginModel) =>
        this.backendService.login(login).pipe(
          map(jwtToken => new LoginSuccess(jwtToken)))
      )
    );

  @Effect({ dispatch: false })
  logout$ = this.actions$.pipe(
    ofType(AuthActionTypes.Logout),
    tap(authed => {
      this.router.navigate(['/login']);
    })
  );
}
