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
import { Vacancy, VacancyDetail, VacancyFilter } from "../models/vacancy";
import { MainProfileInfo } from "../models/main-profile-info";

enum URLS {
  login = '/auth/login',
  logout = '/auth/logout',
  selectUserType = '/auth/selectusertype',

  updateMainProfileInfo = '/profile/updatemaininfo',
  updateSkillsProfileInfo = '/profile/updateskills',
  getMainProfileInfo = '/profile/getmaininfo',
  getProfileSkills = '/profile/getskills',
  isProfileFilled = '/profile/isProfileFilled',

  getUserVacancy = '/vacancy/getUserVacancy',
  getUserVacancies = '/vacancy/getUserVacancies',
  searchVacancies = '/vacancy/searchVacancies',
  getFilteredVacanciesByUserProfile = '/vacancy/getFilteredVacanciesByUserProfile',
  updateOrCreateVacancy = '/vacancy/updateorcreate',
  changeVacancyStatus = '/vacancy/changevacancystatus',

  getMasterValues = '/mastervalues/get'
};

@Injectable()
export class BackendService {
  constructor(private http: HttpClient) {
  }

  login(login: LoginModel): Observable<LoginResponse> {
    return this.http.post(URLS.login, login).pipe(map(response => response as LoginResponse));
  }

  logout(): Observable<boolean> {
    return this.http.post(URLS.logout, null).pipe(map(response => true));
  }

  selectUserType(model: SelectUserType): Observable<LoginResponse> {
    return this.http.post(URLS.selectUserType, model).pipe(map(response => response as LoginResponse));
  }

  getMainProfileInfo() {
    return this.http.get(URLS.getMainProfileInfo).pipe(map(response => response as MainProfileInfo));
  }

  updateMainProfileInfo(model: MainProfileInfo): Observable<any> {
    return this.http.post(URLS.updateMainProfileInfo, model);
  }

  getProfileSkills(): Observable<ProfileSkills> {
    return this.http.get(URLS.getProfileSkills).pipe(map(response => response as ProfileSkills));
  }

  updateProfileSkills(skills: ProfileSkills): Observable<any> {
    return this.http.post(URLS.updateSkillsProfileInfo, skills);
  }

  isProfileFilled(): Observable<boolean> {
    return this.http.get(URLS.isProfileFilled).pipe(map(response => response as boolean));
  }

  getVacancy(id: string): Observable<VacancyDetail> {
    return this.http.get(URLS.getUserVacancy, { params: { "id": id } }).pipe(map(response => response as VacancyDetail));
  }

  getUserVacancies(): Observable<Vacancy[]> {
    return this.http.get(URLS.getUserVacancies).pipe(map(response => response as Vacancy[]));
  }

  searchVacancies(vacancyFilter: VacancyFilter): Observable<Vacancy[]> {
    return this.http.post(URLS.searchVacancies, vacancyFilter).pipe(map(response => response as Vacancy[]));
  }

  getFilteredVacanciesByUserProfile(): Observable<Vacancy[]> {
    return this.http.get(URLS.getFilteredVacanciesByUserProfile).pipe(map(response => response as Vacancy[]));
  }

  updateOrCreateVacancy(vacancy: VacancyDetail): Observable<any> {
    return this.http.post(URLS.updateOrCreateVacancy, vacancy).pipe(map(response => response as VacancyDetail));
  }

  changeVacancyStatus(id: string): Observable<boolean> {
    var vacancy = new Vacancy();
    vacancy.id = id;
    return this.http.post(URLS.changeVacancyStatus, vacancy).pipe(map(response => true));
  }

  getMasterValues(type: MasterValueTypes): Observable<MasterValueItem[]> {
    return this.http.get(URLS.getMasterValues,
      {
         params: { "type": type.toString() }
      }).pipe(map(response => response as MasterValueItem[]));
  }
}
