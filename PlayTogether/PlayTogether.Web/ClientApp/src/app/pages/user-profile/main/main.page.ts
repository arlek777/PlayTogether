import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BackendService } from '../../../services/backend.service';
import { RegExp } from '../../../constants';
import { MainProfileInfo } from '../../../models/main-profile-info';
import { AppState } from '../../../store';

@Component({
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css'],
})
export class MainPage implements OnInit {
  public mainInfoModel = new MainProfileInfo();
  public mainPageForm: FormGroup;
  public phoneMask = RegExp.phoneMask;
  public ageSliderValue: number;

  constructor(private readonly formBuilder: FormBuilder,
    private readonly toastr: ToastrService,
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>) {
  }

  ngOnInit() {
    this.setMainPageValidator();
  }

  get formControls() {
    return this.mainPageForm.controls;
  }

  public submit() {
    if (this.mainPageForm.invalid) return;

    this.mainInfoModel.isActivated = true;
    this.mainInfoModel.name = this.formControls.name.value;
    this.mainInfoModel.contactEmail = this.formControls.email.value;
    this.mainInfoModel.phone1 = this.formControls.phone1.value;
    this.mainInfoModel.city = this.formControls.city.value;
    this.mainInfoModel.age = this.formControls.age;
    this.mainInfoModel.experience = this.ageSliderValue || 0;
    this.mainInfoModel.photoBase64 = 'test';

    console.log(this.mainInfoModel);

    this.backendService.updateMainProfileInfo(this.mainInfoModel)
     .subscribe(() => this.toastr.success("Ваш профиль сохранен."));
  }

  public getExperienceSliderValue(value: number | null) {
    this.ageSliderValue = value;
    return this.ageSliderValue;
  }

  private setMainPageValidator() {
    this.mainPageForm = this.formBuilder.group({
      name: ['', [
        Validators.required,
        Validators.pattern(RegExp.namePattern),
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
      ]],
      age: ['', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(2),
        Validators.min(0),
        Validators.max(95),
      ]]
    });
  }
}
