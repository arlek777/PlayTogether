import { Injectable } from "@angular/core";
import { JwtTokens } from "../models/jwt-tokens";
import { LoginModel } from "../models/login";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { MasterValueTypes } from "../models/master-values-types";
import { MasterValueItem } from "../models/master-value-item";
import { ProfileSkills } from "../models/profile-skills";

enum URLS {
  login = '/auth/login',
  updateMainProfileInfo = '/userprofile/updatemaininfo',
  updateSkillsProfileInfo = '/userprofile/updateskills',
  getMainProfileInfo = '/userprofile/getmaininfo',
  getProfileSkills = '/userprofile/getskills',
  getMasterValues = '/mastervalues/get'
};

@Injectable()
export class BackendService {
  constructor(private http: HttpClient) {
  }

  login(login: LoginModel): Observable<JwtTokens> {
    return this.http.post(URLS.login, login).pipe(map(response => response as JwtTokens));
  }

  getProfileSkills(userId: string): Observable<ProfileSkills> {
    return this.http.get(URLS.getProfileSkills,
      {
        params: { "userId": userId }
      }).pipe(map(response => response as ProfileSkills));
  }

  updateProfileSkills(skills: ProfileSkills): Observable<any> {
    return this.http.post(URLS.updateSkillsProfileInfo, skills);
  }

  getMasterValues(type: MasterValueTypes): Observable<MasterValueItem[]> {
    return this.http.get(URLS.getMasterValues,
      {
         params: { "type": type.toString() }
      }).pipe(map(response => response as MasterValueItem[]));
  }
}
