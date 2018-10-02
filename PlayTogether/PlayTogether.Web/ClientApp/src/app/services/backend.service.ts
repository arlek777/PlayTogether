import { Injectable } from "@angular/core";
import { JwtTokens } from "../models/jwt-tokens";
import { LoginModel } from "../models/login";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";


@Injectable()
export class BackendService {
  constructor(private http: HttpClient) {
  }

  login(login: LoginModel): Observable<JwtTokens> {
    return this.http.post("/auth/login", login).pipe(map(response => response as JwtTokens));
  }
}
