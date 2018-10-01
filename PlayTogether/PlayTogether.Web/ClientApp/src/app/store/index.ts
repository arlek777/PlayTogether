import { AuthState, authReducer } from "./auth/reducers";

export interface AppState {
  auth: AuthState
}

export const appReducers = {
  auth: authReducer
};
