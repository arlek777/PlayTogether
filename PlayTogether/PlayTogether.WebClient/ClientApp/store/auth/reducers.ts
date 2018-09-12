import { Action, Reducer } from 'redux';
import { AuthActionTypes, AuthKnownAction } from './actions';
import { Constants } from '../../constants';

export interface AuthState {
    userName: string;
    loggedIn: boolean;
    isLogining: boolean;
    errorMessage?: string;
}

const userName = window.localStorage.getItem(Constants.UserNameKey);
const emptySate = { loggedIn: false, userName: '', isLogining: false };
const initialState: AuthState = userName
    ? { loggedIn: true, userName, isLogining: false }
    : emptySate;

export const reducer: Reducer<AuthState> = (state: AuthState, incomingAction: Action) => {
    const action = incomingAction as AuthKnownAction;
    switch (action.type) {
    case AuthActionTypes.LOGIN_STARTED:
        return {
            isLogining: true,
            ...initialState
        };
    case AuthActionTypes.LOGIN_SUCCESS:
        return {
            userName: action.userName,
            loggedIn: true,
            isLogining: false
        };
    case AuthActionTypes.LOGIN_FAILED:
        return {
            userName: '',
            loggedIn: false,
            errorMessage: action.errorMessage,
            isLogining: false
        };
    case AuthActionTypes.LOGOUT:
        return emptySate;
    default:
        return state || initialState;
    }
}