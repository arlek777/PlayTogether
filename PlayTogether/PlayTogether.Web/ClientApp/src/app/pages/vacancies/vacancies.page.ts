import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { Constants } from '../../constants';
import { Router } from '@angular/router';
import { AppState } from '../../store';
import { Vacancy } from '../../models/vacancy';

@Component({
  templateUrl: './vacancies.page.html',
})
export class VacanciesPage {
  public vacancies: Vacancy[];

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router) {
  }

  ngOnInit() {
    this.backendService.getVacancies().subscribe((vacancies) => {
      this.vacancies = vacancies;
    });
  }

  reopenVacancy(vacancy: Vacancy) {
    this.backendService.changeVacancyStatus(vacancy.id);
    vacancy.isClosed = false;
  }

  closeVacancy(vacancy: Vacancy) {
    if (prompt("Вы уверены, что хотите закрыть вакансию?")) {
      this.backendService.changeVacancyStatus(vacancy.id);
      vacancy.isClosed = true;
    }
  }
}
