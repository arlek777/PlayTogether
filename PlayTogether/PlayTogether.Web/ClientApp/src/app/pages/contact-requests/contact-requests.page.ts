import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { ActivatedRoute, Router } from '@angular/router';
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

  manageContactRequest(contactRequest: ContactRequest, isApproved: boolean) {
    this.backendService.manageContactRequest(contactRequest.id, isApproved).subscribe(() => {
      this.toastr.success("Вы можете начать общение.");
      contactRequest.status = isApproved ? ContactRequestStatus.Approved : ContactRequestStatus.Rejected;

      if (isApproved) {
        this.router.navigate(['/profile', contactRequest.userId]);
      }
    });
  }

 isRequestOpened(request: ContactRequest) {
   return request.status === ContactRequestStatus.Open;
 }
}
