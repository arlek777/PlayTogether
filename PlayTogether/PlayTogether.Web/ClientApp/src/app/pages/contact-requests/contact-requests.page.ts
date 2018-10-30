import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Constants } from '../../constants';
import { ActivatedRoute, Router } from '@angular/router';
import { PublicVacancy } from '../../models/public-vacancy';
import { ContactRequest, ContactRequestStatus } from '../../models/contact-request';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './contact-requests.page.html',
})
export class ContactRequestsPage {
  public contactRequests: ContactRequest[];
  public ContactRequestStatus = ContactRequestStatus;

  constructor(
    private readonly backendService: BackendService,
    private readonly router: Router,
    private readonly toastr: ToastrService,
    private readonly route: ActivatedRoute) {
  }

  ngOnInit() {
    this.backendService.getUserContactRequests()
      .subscribe((requests) => this.contactRequests = requests);
  }

  manageContactRequest(id, isApproved: boolean) {
    this.backendService.manageContactRequest(id, isApproved).subscribe(() => {
      this.toastr.success("Запрос успешно обновлен.");
    });
  }
}
