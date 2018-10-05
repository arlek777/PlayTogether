import { User } from "./user";

export class JwtTokens {
  constructor(public accessToken: string, public user: User) {
  }
}
