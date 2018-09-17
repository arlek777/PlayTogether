import { Action, Reducer } from 'redux';
import { AuthActionTypes, AuthKnownAction, LoginAction } from './actions';
import { Constants } from '../../constants';

export interface AuthState {
    userName: string;
    isLoggedIn: boolean;
    isLogining: boolean;
    errorMessage?: string;
}

const userName = window.localStorage.getItem(Constants.UserNameKey);
const emptySate = { isLoggedIn: false, userName: '', isLogining: false };
const initialState: AuthState = userName
    ? { isLoggedIn: true, userName, isLogining: false }
    : emptySate;

export const reducer: Reducer<AuthState> = (state: AuthState, action: any) => {
    switch (action.type) {
        case AuthActionTypes.LOGIN_STARTED:
            return {
                isLogining: true,
                ...initialState
            };
        case AuthActionTypes.LOGIN_SUCCESS:
            return {
                userName: action.userName,
                isLoggedIn: true,
                isLogining: false
            };
        case AuthActionTypes.LOGIN_FAILED:
            return {
                userName: '',
                isLoggedIn: false,
                errorMessage: action.errorMessage,
                isLogining: false
            };
        case AuthActionTypes.LOGOUT:
            return emptySate;
        default:
            return state || initialState;
    }
}