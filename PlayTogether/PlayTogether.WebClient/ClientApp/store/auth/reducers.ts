import { Action, Reducer } from 'redux';
import { AuthActionTypes, AuthKnownAction } from 'ClientApp/store/auth/actions';

export interface AuthState {
    user: {
        id: string;
        userName: string;
    }
}

const initialState = {
    user: {id: null, userName: ""}
};

export const reducer: Reducer<AuthState> = (state: AuthState, incomingAction: Action) => {
    const action = incomingAction as AuthKnownAction;
    switch (action.type) {
        case AuthActionTypes.LOGIN:
            const base64UserClaims = action.idToken.split(".")[1];
            const userClaims = JSON.parse(atob(base64UserClaims));
            return {
                user: {
                    id: userClaims.id,
                    userName: userClaims.userName
                }
            };
        default:
            return state || initialState;
    } 
}