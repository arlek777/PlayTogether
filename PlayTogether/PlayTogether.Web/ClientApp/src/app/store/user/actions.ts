import { Action } from '@ngrx/store';
import { LoginModel } from '../../models/login';
import { LoginResponse } from '../../models/login-response';
import { SelectUserType } from '../../models/select-user-type';

export enum UserActionTypes {
  Login = '[User] Login',
  AutoLogin = '[User] Auto Login',
  LoginSuccess = '[User] Login Success',
  UpdateUserType = '[User] Update type',
  Logout = '[User] Logout'
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

  constructor(public payload: LoginResponse) { }
}

export class UpdateUserType implements Action {
  readonly type = UserActionTypes.UpdateUserType;

  constructor(public payload: SelectUserType) { }
}

export class Logout implements Action {
  readonly type = UserActionTypes.Logout;
}

export type UserActions = Login | LoginSuccess | UpdateUserType | Logout | AutoLogin;
