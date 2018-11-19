import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Constants } from '../../constants';
import { ActivatedRoute, Router } from '@angular/router';
import { PublicVacancy } from '../../models/public-vacancy';
import { ContactRequest } from '../../models/contact-request';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './vacancy.page.html',
})
export class VacancyPage {
  public vacancy: PublicVacancy;

  constructor(
    private readonly backendService: BackendService,
    private readonly router: Router,
    private readonly toastr: ToastrService,
    private readonly route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.backendService.getVacancy(params["id"]).subscribe(vacancy => this.vacancy = vacancy);
      }
    });
  }

  sendContactRequest() {
    const request = new ContactRequest();
    request.toUserId = this.vacancy.userCreatorId;
    this.backendService.sendContactRequest(request)
      .subscribe(() => {
        this.toastr.success('Запрос отправлен');
        this.router.navigate(['/']);
      });
  }
}
