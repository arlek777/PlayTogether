import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { Constants } from '../../constants';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginModel } from '../../models/login';
import { AppState } from '../../store';
import { PublicProfile } from '../../models/public-profile';
import { UserType } from '../../models/user-type';
import { ContactRequest } from '../../models/contact-request';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './profile.page.html',
})
export class ProfilePage {
  public profile: PublicProfile = new PublicProfile();
  public userType: UserType;

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router,
    private readonly toastr: ToastrService,
    private readonly route: ActivatedRoute) {

    this.store.select(s => s.user.userType).subscribe((type) => this.userType = type);
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.backendService.getPublicProfile(params["id"]).subscribe(profile => this.profile = profile);
      } else {
        this.backendService.getPublicProfile().subscribe(profile => this.profile = profile);
      }
    });
  }

  sendContactRequest() {
    const request = new ContactRequest();
    request.toUserId = this.profile.id;
    this.backendService.sendContactRequest(request)
      .subscribe(() => {
        this.toastr.success('Запрос отправлен');
        this.router.navigate(['/']);
      });
  }

  get isMusician() {
    return this.userType === UserType.Musician;
  }

  get isGroup() {
    return this.userType === UserType.Group;
  }
}
