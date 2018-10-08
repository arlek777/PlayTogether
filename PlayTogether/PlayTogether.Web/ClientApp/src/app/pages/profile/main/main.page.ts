import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css'],
})
export class MainPage implements OnInit {

  constructor(private readonly formBuilder: FormBuilder,
              private readonly toastr: ToastrService) {
  }

  ngOnInit() {
    this.setMainPageValidator();
  }

  get f() {
    return this.mainPageForm.controls;
  };

  public mainPageForm: FormGroup;
  public phoneMask = ['(', /[0-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, '-', /\d/, /\d/];//add in new file
  public phone1 = '';
  public phone2 = '';
  public address = '';
  public description = '';
  public ageSliderValue: number;

  public submitUserMainData() {
    if (this.mainPageForm.invalid) return;

    const data = {
      'name': this.f.name.value,
      'contact_email': this.f.email.value,
      'phone1': this.f.phone1.value,
      'phone2': this.phone2,
      'city': this.f.city.value,
      'address': this.address,
      'age': this.ageSliderValue,
      'experience': this.f.experience.value,
      'description': this.description,
      'photoBase64': null
    };
    console.log(data);

  }

  public getAgeSliderValue(value: number | null) {
    this.ageSliderValue = value;
    return this.ageSliderValue;
  }

  private setMainPageValidator() {
    this.mainPageForm = this.formBuilder.group({
      name: ['', [
        Validators.required,
        Validators.pattern(/^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$/),
        Validators.minLength(2),
      ]],
      email: ['', [
        Validators.required,
        Validators.pattern(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/),
      ]],
      phone1: ['', [
        Validators.required,
      ]],
      city: ['', [
        Validators.required,
      ]],
      experience: ['', [
        Validators.minLength(1),
        Validators.maxLength(2),
        Validators.min(0),
        Validators.max(50),
      ]]
    });
  }
}
