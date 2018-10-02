import { AuthActionTypes, AuthActions } from './actions';
import { LoginModel } from '../../models/login';

export interface AuthState {
  isLoggedIn: boolean;
  userName: string | null;
}

export const initialState: AuthState = {
  isLoggedIn: false,
  userName: null
};

export function authReducer(state = initialState, action: AuthActions): AuthState {
  switch (action.type) {
    case AuthActionTypes.LoginSuccess: {
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
