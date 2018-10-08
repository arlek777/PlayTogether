import { Injectable } from "@angular/core";
import { LoginResponse } from "../models/login-response";
import { LoginModel } from "../models/login";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { MasterValueTypes } from "../models/master-values-types";
import { MasterValueItem } from "../models/master-value-item";
import { ProfileSkills } from "../models/profile-skills";
import { SelectUserType } from "../models/select-user-type";

enum URLS {
  login = '/auth/login',
  selectUserType = '/auth/selectusertype',
  updateMainProfileInfo = '/profile/updatemaininfo',
  updateSkillsProfileInfo = '/profile/updateskills',
  getMainProfileInfo = '/profile/getmaininfo',
  getProfileSkills = '/profile/getskills',
  getMasterValues = '/mastervalues/get'
};

@Injectable()
export class BackendService {
  constructor(private http: HttpClient) {
  }

  login(login: LoginModel): Observable<LoginResponse> {
    return this.http.post(URLS.login, login).pipe(map(response => response as LoginResponse));
  }

  selectUserType(model: SelectUserType): Observable<boolean> {
    return this.http.post(URLS.selectUserType, model).pipe(map(response => true));
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
