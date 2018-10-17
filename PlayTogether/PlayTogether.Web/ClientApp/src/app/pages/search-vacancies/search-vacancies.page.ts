import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { Constants } from '../../constants';
import { Router } from '@angular/router';
import { AppState } from '../../store';
import { Vacancy, VacancyFilter } from '../../models/vacancy';
import { MasterValueItem } from '../../models/master-value-item';
import { MasterValueTypes } from '../../models/master-values-types';

@Component({
  templateUrl: './search-vacancies.page.html',
})
export class SearchVacanciesPage {
  public vacancies: Vacancy[];
  public vacancyFilter = new VacancyFilter();
  public musicGenres: MasterValueItem[];
  public musicianRoles: MasterValueItem[];

  constructor(
    private readonly backendService: BackendService,
    private readonly router: Router) {
  }

  ngOnInit() {
    this.backendService.getMasterValues(MasterValueTypes.MusicGenres)
      .subscribe((values) => this.musicGenres = values);

    this.backendService.getMasterValues(MasterValueTypes.MusicianRoles)
      .subscribe((values) => this.musicianRoles = values);

    this.search();
  }

  search() {
    this.backendService.searchVacancies(this.vacancyFilter).subscribe((vacancies) => {
      this.vacancies = vacancies;
    });
  }
}
