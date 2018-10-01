import { Action } from '@ngrx/store';
import { LoginModel } from 'src/app/models/login';
import { JwtTokens } from 'src/app/models/jwt-tokens';

export enum AuthActionTypes {
  LoginStarted = '[Auth] Login Started',
  LoginSuccess = '[Auth] Login Success',
  Logout = '[Auth] Logout'
}


export class LoginStarted implements Action {
  readonly type = AuthActionTypes.LoginStarted;

  constructor(public payload: LoginModel) { }
}

export class LoginSuccess implements Action {
  readonly type = AuthActionTypes.LoginSuccess;

  constructor(public payload: JwtTokens) { }
}

export class Logout implements Action {
  readonly type = AuthActionTypes.Logout;
}
