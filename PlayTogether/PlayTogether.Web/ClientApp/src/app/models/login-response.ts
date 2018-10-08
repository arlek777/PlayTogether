import { User } from "./user";

export class LoginResponse {
  constructor(public accessToken: string, public user: User, public isNewUser: boolean) {
  }
}
