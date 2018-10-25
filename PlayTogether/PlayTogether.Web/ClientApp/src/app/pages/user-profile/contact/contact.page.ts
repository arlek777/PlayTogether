import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { BackendService } from '../../../services/backend.service';
import { ContactInfo } from '../../../models/contact-profile-info';
import { AppState } from '../../../store';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserType } from '../../../models/user-type';
import { RegExp } from '../../../constants';

@Component({
  templateUrl: './contact.page.html',
})
export class ContactPage {
  public contactInfoModel: ContactInfo = new ContactInfo();
  public formSubmitted = false;
  public userType: UserType;
  public form: FormGroup;
  public phoneMask = RegExp.phoneMask;

  constructor(private readonly backendService: BackendService,
    private readonly formBuilder: FormBuilder,
    private readonly store: Store<AppState>,
    private readonly toastr: ToastrService) {

    this.store.select(s => s.user.userType).subscribe((type) => this.userType = type);
  }

  ngOnInit() {
    this.setPageValidator();

    this.backendService.getContactProfileInfo()
      .subscribe((value) => {
        this.contactInfoModel = value;

        this.formControls.email.setValue(value.contactEmail);
        this.formControls.phone1.setValue(value.phone1);
        this.formControls.city.setValue(value.city);
      });
  }

  get formControls() {
    return this.form.controls;
  }

  public submit() {
    this.formSubmitted = true;
    if (this.form.invalid) return;

    this.contactInfoModel.contactEmail = this.formControls.email.value;
    this.contactInfoModel.phone1 = this.formControls.phone1.value;
    this.contactInfoModel.city = this.formControls.city.value;

    this.backendService.updateProfileSkills(this.contactInfoModel)
      .subscribe(() => this.toastr.success("Ваш профиль сохранен."));
  }

  private setPageValidator() {
    this.form = this.formBuilder.group({
      email: [
        '', [
          Validators.required,
          Validators.pattern(RegExp.emailPattern)
        ]
      ],
      phone1: [
        '', [
          Validators.required
        ]
      ],
      city: [
        '', [
          Validators.required
        ]
      ]
    });
  }
}
