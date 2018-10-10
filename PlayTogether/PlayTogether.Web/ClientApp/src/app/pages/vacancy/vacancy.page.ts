import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { Constants } from '../../constants';
import { Router } from '@angular/router';
import { AppState } from '../../store';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterValueItem } from '../../models/master-value-item';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';

@Component({
  templateUrl: './vacancy.page.html',
})
export class VacancyPage {
  public vacancyFormGroup: FormGroup;
  public selectedMusicRoles: MasterValueItem[];
  public selectedMusicGenres: MasterValueItem[];
  public musicGenres: MasterValueItem[];
  public musicianRoles: MasterValueItem[];

  public dropdownSettings: IDropdownSettings = {
    enableCheckAll: false,
    singleSelection: false,
    idField: 'id',
    textField: 'title',
    itemsShowLimit: 10,
    allowSearchFilter: true,
    closeDropDownOnSelection: true
  };

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router,
    private readonly formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.vacancyFormGroup = this.formBuilder.group({
      status: ['', [Validators.required]],
      title: ['', [Validators.required]],
      description: [''],
      minExpirience: [''],
      cities: ['']
    });
  }

  public get formControls() {
    return this.vacancyFormGroup.controls;
  }

  public submit() {
    if (this.vacancyFormGroup.invalid) return;


  }
}
