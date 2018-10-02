import { Action } from '@ngrx/store';
import { LoginModel } from '../../models/login';
import { JwtTokens } from '../../models/jwt-tokens';

export enum AuthActionTypes {
  Login = '[Auth] Login',
  AutoLogin = '[Auth] Auto Login',
  LoginSuccess = '[Auth] Login Success',
  Logout = '[Auth] Logout'
}

export class AutoLogin implements Action {
  readonly type = AuthActionTypes.AutoLogin;
}

export class Login implements Action {
  readonly type = AuthActionTypes.Login;

  constructor(public payload: LoginModel) { }
}

export class LoginSuccess implements Action {
  readonly type = AuthActionTypes.LoginSuccess;

  constructor(public payload: JwtTokens) { }
}

export class Logout implements Action {
  readonly type = AuthActionTypes.Logout;
}

export type AuthActions = Login | LoginSuccess | Logout | AutoLogin;
