import { UserActionTypes, UserActions } from './actions';
import { LoginModel } from '../../models/login';

export interface UserState {
  isLoggedIn: boolean;
  userName: string | null;
  id: string | null;
}

export const initialState: UserState = {
  isLoggedIn: false,
  userName: null,
  id: null
};

export function userReducer(state = initialState, action: UserActions): UserState {
  switch (action.type) {
    case UserActionTypes.LoginSuccess: {
      return {
        isLoggedIn: true,
        userName: action.payload.user.userName,
        id: action.payload.user.id
      };
    }

    case UserActionTypes.Logout: {
      return initialState;
    }

    default: {
      return state;
    }
  }
}
