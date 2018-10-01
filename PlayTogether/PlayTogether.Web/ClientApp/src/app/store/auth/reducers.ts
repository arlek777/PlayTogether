import { AuthActionTypes } from './actions';
import { CommonAction } from '../../models/common-action';
import { LoginModel } from 'src/app/models/login';

export interface AuthState {
  isLoggedIn: boolean;
  userName: string | null;
}

export const initialState: AuthState = {
  isLoggedIn: false,
  userName: null
};

export function authReducer(state = initialState, action: CommonAction<AuthActionTypes, LoginModel>): AuthState {
  switch (action.type) {
    case AuthActionTypes.Login: {
      return {
        isLoggedIn: true,
        userName: action.payload.userName
      };
    }

    case AuthActionTypes.Logout: {
      return initialState;
    }

    default: {
      return state;
    }
  }
}
