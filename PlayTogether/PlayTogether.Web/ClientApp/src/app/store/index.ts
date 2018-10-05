import { UserState, userReducer } from "./user/reducers";
import { UserEffects } from "./user/effects";

export interface AppState {
  user: UserState
}

export const appReducers = {
  user: userReducer
};

export const appEffects = [
  UserEffects
];
