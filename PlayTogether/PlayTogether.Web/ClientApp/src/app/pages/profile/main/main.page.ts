import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BackendService } from '../../../services/backend.service';
import { RegExp } from '../../../constants';
import { MainInfo } from '../../../models/main-info';
import { AppState } from '../../../store';

@Component({
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css'],
})
export class MainPage implements OnInit {
  public mainPageForm: FormGroup;
  public phoneMask = RegExp.phoneMask;
  public ageSliderValue: number;
  private mainInfoDataModel: MainInfoDataModel;
  public phone1: string = '';
  public phone2: string = '';
  public address: string = '';
  public description: string = '';
  public ageSliderValue: number;

  constructor(private readonly formBuilder: FormBuilder,
    private readonly toastr: ToastrService,
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,) {

    this.mainInfoDataModel = new MainInfoDataModel();
  }

  ngOnInit() {
    this.setMainPageValidator();
  }

  get f() {
    return this.mainPageForm.controls;
  };

  public submitUserMainData() {
    if (this.mainPageForm.invalid) return;

    this.mainInfoDataModel.isActivated = true;
    this.mainInfoDataModel.name = this.f.name.value;
    this.mainInfoDataModel.contactEmail = this.f.email.value;
    this.mainInfoDataModel.phone1 = this.f.phone1.value;
    this.mainInfoDataModel.phone2 = this.phone2;
    this.mainInfoDataModel.city = this.f.city.value;
    this.mainInfoDataModel.address = this.address;
    this.mainInfoDataModel.age = this.f.age;
    this.mainInfoDataModel.experience = this.ageSliderValue || 0;
    this.mainInfoDataModel.description = this.description;
    this.mainInfoDataModel.photoBase64 = 'test';

    console.log(this.mainInfoDataModel);

    this.backendService.updateMainProfileInfo(this.mainInfoDataModel)
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
