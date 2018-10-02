export class JwtTokens {
  constructor(public accessToken: string, public user: { userName: string, isNewUser: boolean }) {
  }
}
