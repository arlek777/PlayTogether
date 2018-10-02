import { AuthState, authReducer } from "./auth/reducers";
import { AuthEffects } from "./auth/effects";

export interface AppState {
  auth: AuthState
}

export const appReducers = {
  auth: authReducer
};

export const appEffects = [
  AuthEffects
];
