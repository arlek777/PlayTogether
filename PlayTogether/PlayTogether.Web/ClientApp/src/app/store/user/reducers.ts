import { UserActionTypes, UserActions } from './actions';
import { LoginModel } from '../../models/login';
import { UserType } from '../../models/user-type';

export interface UserState {
  isLoggedIn: boolean;
  userName: string | null;
  userType: UserType;
}

export const initialState: UserState = {
  isLoggedIn: false,
  userName: null,
  userType: UserType.Uknown,
};

export function userReducer(state = initialState, action: UserActions): UserState {
  switch (action.type) {
    case UserActionTypes.LoginSuccess: {
      return {
        isLoggedIn: true,
        userName: action.payload.user.userName,
        userType: action.payload.user.userType
      };
    }

    case UserActionTypes.UpdateUserType: {
      return {
        ...state,
        userType: action.payload.userType
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
