import { Injectable } from "@angular/core";
import { LoginResponse } from "../models/login-response";
import { LoginModel } from "../models/login";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { MasterValueTypes } from "../models/master-values-types";
import { MasterValueItem } from "../models/master-value-item";
import { SelectUserType } from "../models/select-user-type";
import { Vacancy, VacancyDetail, VacancyFilter } from "../models/vacancy";
import { MainProfileInfo } from "../models/main-profile-info";
import { PublicProfile } from "../models/public-profile";
import { ContactInfo } from "../models/contact-profile-info";

enum URLS {
  login = '/auth/login',
  logout = '/auth/logout',
  selectUserType = '/auth/selectusertype',

  updateMainProfileInfo = '/profile/updatemaininfo',
  updateContactProfileInfo = '/profile/updateContactInfo',
  getMainProfileInfo = '/profile/getUserProfileMainInfo',
  getContactProfileInfo = '/profile/getUserProfileContactInfo',
  getPublicProfile = '/profile/getPublicProfile',
  isUserProfileFilled = '/profile/isUserProfileFilled',

  getUserVacancy = '/vacancy/getUserVacancy',
  getVacancy = '/vacancy/getVacancy',
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

  getPublicProfile(id: string = null) {
    if (id) {
      return this.http.get(URLS.getPublicProfile, { params: { "id": id } })
        .pipe(map(response => response as PublicProfile));
    } else {
      return this.http.get(URLS.getPublicProfile).pipe(map(response => response as PublicProfile));
    }
  }

  getMainProfileInfo() {
    return this.http.get(URLS.getMainProfileInfo).pipe(map(response => response as MainProfileInfo));
  }

  updateMainProfileInfo(model: MainProfileInfo): Observable<any> {
    return this.http.post(URLS.updateMainProfileInfo, model);
  }

  getContactProfileInfo(): Observable<ContactInfo> {
    return this.http.get(URLS.getContactProfileInfo).pipe(map(response => response as ContactInfo));
  }

  updateProfileSkills(model: ContactInfo): Observable<any> {
    return this.http.post(URLS.updateContactProfileInfo, model);
  }

  isUserProfileFilled(): Observable<boolean> {
    return this.http.get(URLS.isUserProfileFilled).pipe(map(response => response as boolean));
  }

  getUserVacancy(id: string): Observable<VacancyDetail> {
    return this.http.get(URLS.getUserVacancy, { params: { "id": id } }).pipe(map(response => response as VacancyDetail));
  }

  getVacancy(id: string): Observable<VacancyDetail> {
    return this.http.get(URLS.getVacancy, { params: { "id": id } }).pipe(map(response => response as VacancyDetail));
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
