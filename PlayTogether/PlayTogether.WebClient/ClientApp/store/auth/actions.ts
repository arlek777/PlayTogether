import { Action, Reducer } from 'redux';
import { AppThunkAction } from 'ClientApp/store';
import { backendService } from 'ClientApp/services/backend-service';
import { JWTTokens } from 'ClientApp/models/jwttokens';

export class AuthActionTypes {
    static LOGIN: 'AUTH_LOGIN'
};

export interface LoginAction {
    type: AuthActionTypes;
    idToken: string;
}

export type AuthKnownAction = LoginAction;

export const actionCreators = {
    login: (userName: string, password: string): AppThunkAction<AuthKnownAction> => async (dispatch, getState) => {
        const tokens: JWTTokens = await backendService.login(userName, password);
        window.localStorage.setItem(Constants.AccessTokenKey, tokens.accessToken);

        dispatch({ type: AuthActionTypes.LOGIN, idToken: tokens.idToken });
    }
};