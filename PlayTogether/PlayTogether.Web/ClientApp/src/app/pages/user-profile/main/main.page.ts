import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BackendService } from '../../../services/backend.service';
import { RegExp } from '../../../constants';
import { MainProfileInfo } from '../../../models/main-profile-info';
import { AppState } from '../../../store';
import { UserType } from '../../../models/user-type';

@Component({
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css'],
})
export class MainPage implements OnInit {
  public mainInfoModel = new MainProfileInfo();
  public mainPageForm: FormGroup;
  public phoneMask = RegExp.phoneMask;
  public formSubmitted = false;
  public userType: UserType;

  constructor(private readonly formBuilder: FormBuilder,
    private readonly toastr: ToastrService,
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>) {

    this.store.select(s => s.user.userType).subscribe((type) => this.userType = type);
  }

  ngOnInit() {
    this.setMainPageValidator();

    this.backendService.getMainProfileInfo().subscribe(profile => {
      this.mainInfoModel = profile;
      this.formControls.name.setValue(profile.name);
      this.formControls.email.setValue(profile.contactEmail);
      this.formControls.phone1.setValue(profile.phone1);
      this.formControls.city.setValue(profile.city);

      if (this.isGroup) {
        this.formControls.groupName.setValue(profile.groupName);
      } else if (this.isMusician) {
        this.formControls.experience.setValue(profile.experience);
        this.formControls.age.setValue(profile.age);
      }
    });
  }

  get formControls() {
    return this.mainPageForm.controls;
  }

  get isMusician() {
    return this.userType === UserType.Musician;
  }

  get isGroup() {
    return this.userType === UserType.Group;
  }

  public submit() {
    this.formSubmitted = true;
    if (this.mainPageForm.invalid) return;

    this.mainInfoModel.isActivated = true;
    this.mainInfoModel.name = this.formControls.name.value;
    this.mainInfoModel.contactEmail = this.formControls.email.value;
    this.mainInfoModel.phone1 = this.formControls.phone1.value;
    this.mainInfoModel.city = this.formControls.city.value;
    this.mainInfoModel.photoBase64 = 'test';

    if (this.isGroup) {
      this.mainInfoModel.groupName = this.formControls.groupName.value;
    } else if (this.isMusician) {
      this.mainInfoModel.experience = this.formControls.experience.value;
      this.mainInfoModel.age = this.formControls.age.value;
    }

    console.log(this.mainInfoModel);

    this.backendService.updateMainProfileInfo(this.mainInfoModel)
      .subscribe(() => {
        this.formSubmitted = false;
        this.toastr.success("Ваш профиль сохранен.")
      });
  }

  private setMainPageValidator() {
    this.mainPageForm = this.formBuilder.group({
      name: ['', [
        Validators.required,
        Validators.minLength(2),
      ]],
      email: ['', [
        Validators.required,
        Validators.pattern(RegExp.emailPattern),
      ]],
      phone1: ['', [
        Validators.required,
      ]],
      city: ['', [
        Validators.required,
      ]]
    });

    if (this.isGroup) {
      this.mainPageForm.addControl('groupName', this.formBuilder.control('', [
        Validators.required
      ]));
    } else if (this.isMusician) {
      this.mainPageForm.addControl('age', this.formBuilder.control('', [
        Validators.required,
        Validators.maxLength(2),
        Validators.min(10),
        Validators.max(95),
      ]));

      this.mainPageForm.addControl('experience', this.formBuilder.control('', [
        Validators.min(0),
        Validators.max(95),
      ]));
    }
  }
}
