import { AuthActionTypes } from './actions';
import { CommonAction } from '../../models/action';

export interface State {
  loggedIn: boolean;
  userName: string | null;
}

export const initialState: State = {
  loggedIn: false,
  userName: null
};

export function reducer(state = initialState, action: CommonAction<AuthActionTypes, string>): State {
  switch (action.type) {
    case AuthActionTypes.Login: {
      return {
        loggedIn: true,
        userName: action.payload
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

//export const getLoggedIn = (state: State) => state.loggedIn;
//export const getUser = (state: State) => state.user;
