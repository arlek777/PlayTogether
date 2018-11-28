import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { Constants } from '../../constants';
import { Router } from '@angular/router';
import { AppState } from '../../store';
import { Vacancy, VacancyFilter } from '../../models/vacancy';
import { MasterValueItem } from '../../models/master-value-item';
import { MasterValueTypes } from '../../models/master-values-types';
import { UserType } from '../../models/user-type';
import { PublicVacancy } from '../../models/public-vacancy';

@Component({
  templateUrl: './search-vacancies.page.html',
})
export class SearchVacanciesPage {
  public vacancies: PublicVacancy[];
  public vacancyFilterModel = new VacancyFilter();
  public musicGenres: MasterValueItem[];
  public musicianRoles: MasterValueItem[];
  public workTypes: MasterValueItem[];
  public cities: MasterValueItem[];
  public userType: UserType;
  public isLoaded = false;

  public dropdownSettings = Constants.getAutocompleteSettings();

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router) {

    this._setInitialFilter();
    this.store.select(s => s.user.userType).subscribe((type) => this.userType = type);
  }

  ngOnInit() {
    this.backendService.getMasterValues(MasterValueTypes.MusicGenres)
      .subscribe((values) => this.musicGenres = values);
    this.backendService.getMasterValues(MasterValueTypes.MusicianRoles)
      .subscribe((values) => this.musicianRoles = values);
    this.backendService.getMasterValues(MasterValueTypes.Cities)
      .subscribe((values) => this.cities = values);
    this.backendService.getMasterValues(MasterValueTypes.WorkTypes)
      .subscribe((values) => this.workTypes = values);

    if (sessionStorage[Constants.vacancyFiltersSessionStorageKey]) {
      this.vacancyFilterModel = JSON.parse(sessionStorage[Constants.vacancyFiltersSessionStorageKey]);
    }
    this.search();
  }

  get isGroup() {
    return this.userType === UserType.Group;
  }

  get isMusician() {
    return this.userType === UserType.Musician;
  }

  search() {
    this.isLoaded = false;
    if (!this.vacancyFilterModel.minExperience) {
      this.vacancyFilterModel.minExperience = 0;
    }
    if (!this.vacancyFilterModel.minRating) {
      this.vacancyFilterModel.minRating = 0;
    }

    sessionStorage.setItem(Constants.vacancyFiltersSessionStorageKey, JSON.stringify(this.vacancyFilterModel));
    this.backendService.searchVacancies(this.vacancyFilterModel).subscribe((vacancies) => {
      this.vacancies = vacancies;
      this.isLoaded = true;
    });
  }

  searchByProfile() {
    this.isLoaded = false;
    this.backendService.searchFilteredVacanciesByUserProfile().subscribe((result: any) => {
      this.vacancies = result.vacancies;
      this.vacancyFilterModel = result.filter;
      this.isLoaded = true;
    });
  }

  reset() {
    this.isLoaded = false;
    this._setInitialFilter();
    this.backendService.searchVacancies(this.vacancyFilterModel).subscribe((vacancies) => {
      this.vacancies = vacancies;
      this.isLoaded = true;
    });
  }

  private _setInitialFilter() {
    this.vacancyFilterModel.musicianRoles = [];
    this.vacancyFilterModel.musicGenres = [];
    this.vacancyFilterModel.workTypes = [];
    this.vacancyFilterModel.cities = [];
    this.vacancyFilterModel.minExperience = 0;
    this.vacancyFilterModel.vacancyTitle = "";
  }
}
