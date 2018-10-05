import { Action } from '@ngrx/store';
import { LoginModel } from '../../models/login';
import { JwtTokens } from '../../models/jwt-tokens';

export enum UserActionTypes {
  Login = '[Auth] Login',
  AutoLogin = '[Auth] Auto Login',
  LoginSuccess = '[Auth] Login Success',
  Logout = '[Auth] Logout'
}

export class AutoLogin implements Action {
  readonly type = UserActionTypes.AutoLogin;
}

export class Login implements Action {
  readonly type = UserActionTypes.Login;

  constructor(public payload: LoginModel) { }
}

export class LoginSuccess implements Action {
  readonly type = UserActionTypes.LoginSuccess;

  constructor(public payload: JwtTokens) { }
}

export class Logout implements Action {
  readonly type = UserActionTypes.Logout;
}

export type UserActions = Login | LoginSuccess | Logout | AutoLogin;
