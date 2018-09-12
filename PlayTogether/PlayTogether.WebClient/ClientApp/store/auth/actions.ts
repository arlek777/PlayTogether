import { Action, Reducer } from 'redux';
import { addTask } from 'domain-task';
import { AppThunkAction } from '../../store';
import backendService from '../../services/backend-service';
import { JWTTokens } from '../../models/jwttokens';
import { Constants } from '../../constants';

export const AuthActionTypes = {
    LOGIN_STARTED: 'LOGIN_STARTED',
    LOGIN_SUCCESS: 'LOGIN_SUCCESS',
    LOGIN_FAILED: 'LOGIN_FAILED',
    LOGOUT: 'LOGOUT'
};

export interface LoginAction {
    type: string;
    userName: string;
    errorMessage?: string;
}

export interface LogoutAction {
    type: string;
    userName?: string;
    errorMessage?: string;
}

export type AuthKnownAction = LoginAction | LogoutAction;

export const actionCreators = {
    login: (userName: string, password: string): AppThunkAction<AuthKnownAction> => (dispatch, getState) => {
        dispatch({ type: AuthActionTypes.LOGIN_STARTED });

        try {
            let task = backendService.login(userName, password).then((tokens: JWTTokens) => {
                window.localStorage.setItem(Constants.AccessTokenKey, tokens.accessToken);
                window.localStorage.setItem(Constants.UserNameKey, tokens.userName);
                dispatch({ type: AuthActionTypes.LOGIN_SUCCESS, userName: tokens.userName });
            });
            addTask(task);
        } catch (e) {
            dispatch({ type: AuthActionTypes.LOGIN_FAILED, errorMessage: e });
        } 
    },
    logout: (): AppThunkAction<AuthKnownAction> => async (dispatch, getState) => {
        window.localStorage.clear();
        dispatch({ type: AuthActionTypes.LOGOUT });
    }
};