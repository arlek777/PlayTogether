import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { Constants } from '../../constants';
import { Router } from '@angular/router';
import { AppState } from '../../store';
import { Vacancy } from '../../models/vacancy';

@Component({
  templateUrl: './manage-vacancies.page.html',
})
export class ManageVacanciesPage {
  public vacancies: Vacancy[];
  public canCreateVacancy = false;

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router) {
  }

  ngOnInit() {
    this.backendService.isProfileFilled().subscribe(result => {
      this.canCreateVacancy = result;
    });

    this.backendService.getUserVacancies().subscribe((vacancies) => {
      this.vacancies = vacancies;
    });
  }

  reopenVacancy(vacancy: Vacancy) {
    this.backendService.changeVacancyStatus(vacancy.id).subscribe(() => {
      vacancy.isClosed = false;
    });
  }

  closeVacancy(vacancy: Vacancy) {
    if (confirm("Вы уверены, что хотите закрыть вакансию?")) {
      this.backendService.changeVacancyStatus(vacancy.id).subscribe(() => {
        vacancy.isClosed = true;
      });
    }
  }
}
