import { AuthActionTypes, AuthActions } from './actions';
import { LoginModel } from '../../models/login';

export interface AuthState {
  isLoggedIn: boolean;
  userName: string | null;
  id: string | null;
}

export const initialState: AuthState = {
  isLoggedIn: false,
  userName: null,
  id: null
};

export function authReducer(state = initialState, action: AuthActions): AuthState {
  switch (action.type) {
    case AuthActionTypes.LoginSuccess: {
      return {
        isLoggedIn: true,
        userName: action.payload.user.userName,
        id: action.payload.user.id
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
