import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, exhaustMap, map, tap } from 'rxjs/operators';
import { UserActionTypes, LoginSuccess, Login, AutoLogin, Logout, UpdateUserType, LoginFailed } from './actions';
import { BackendService } from '../../services/backend.service';
import { LoginModel } from '../../models/login';
import { Constants } from '../../constants';
import { LoginResponse } from '../../models/login-response';
import { SelectUserType } from '../../models/select-user-type';
import { UserType } from '../../models/user-type';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class UserEffects {
  constructor(
    private readonly actions$: Actions,
    private readonly backendService: BackendService,
    private readonly router: Router,
    private readonly toastr: ToastrService
  ) { }

  @Effect()
  login$ = this.actions$.pipe(
      ofType<Login>(UserActionTypes.Login),
    map(action => action.payload),
      exhaustMap((login: LoginModel) =>
        this.backendService.login(login).pipe(
          map(response => new LoginSuccess(response)),
          catchError((error) => {
            console.log(error);
            this.toastr.error("Произошла ошибка.");
            return of(new LoginFailed());
          })
          )
      )
  );

  @Effect()
  autoLogin$ = this.actions$.pipe(
      ofType<AutoLogin>(UserActionTypes.AutoLogin),
      map((action: AutoLogin) => {
        const accessToken = window.localStorage.getItem(Constants.accessTokenKey);
        const user = window.localStorage.getItem(Constants.currentUserKey);

        if (!accessToken || !user) {
          return new Logout();
        } else {
          return new LoginSuccess(new LoginResponse(accessToken, JSON.parse(user), false));
        }
      })
  );

  @Effect({ dispatch: false })
  $loginSuccess = this.actions$.pipe(
      ofType(UserActionTypes.LoginSuccess),
      tap((action: LoginSuccess) => {
        const accessToken = window.localStorage.getItem(Constants.accessTokenKey);
        const user = window.localStorage.getItem(Constants.currentUserKey);
        if (!accessToken && !user) {
          window.localStorage.setItem(Constants.accessTokenKey, action.payload.accessToken);
          window.localStorage.setItem(Constants.currentUserKey, JSON.stringify(action.payload.user));
        }

        if (action.payload.isNewUser || action.payload.user.userType === UserType.Uknown) {
          this.router.navigate(['/select-user-type']);
        }
      })
  );

  @Effect({ dispatch: false })
  $updateUserType = this.actions$.pipe(
    ofType<UpdateUserType>(UserActionTypes.UpdateUserType),
      tap((action: UpdateUserType) => {
        this.backendService.selectUserType(action.payload).subscribe((response: LoginResponse) => {
          window.localStorage.setItem(Constants.accessTokenKey, response.accessToken);
          window.localStorage.setItem(Constants.currentUserKey, JSON.stringify(response.user));
          this.router.navigate(['/']);
        });
      })
  );

  @Effect({ dispatch: false })
  logout$ = this.actions$.pipe(
      ofType(UserActionTypes.Logout),
      tap(action => {
        window.localStorage.clear();
        window.sessionStorage.clear();
        this.backendService.logout();
        this.router.navigate(['/login']);
      })
  );
}
