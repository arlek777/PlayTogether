import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Constants } from '../../constants';
import { ActivatedRoute, Router } from '@angular/router';
import { VacancyDetail } from '../../models/vacancy';

@Component({
  templateUrl: './vacancy.page.html',
})
export class VacancyPage {
  public vacancy: VacancyDetail;

  constructor(
    private readonly backendService: BackendService,
    private readonly router: Router,
    private readonly route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.backendService.getVacancy(params["id"]).subscribe(vacancy => this.vacancy = vacancy);
      }
    });
  }
}
