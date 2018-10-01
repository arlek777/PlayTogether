import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { JwtTokens } from "../models/jwt-tokens";
import { LoginModel } from "../models/login";


@Injectable()
export class BackendService {
    constructor(private http: Http) {
    }

  login(login: LoginModel): Promise<JwtTokens> {
    return this.http.post("/api/auth/login", login).toPromise()
      .then((result) => { return result.json() as JwtTokens; });
  }
}
