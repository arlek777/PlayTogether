import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Constants } from '../../constants';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterValueItem } from '../../models/master-value-item';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { MasterValueTypes } from '../../models/master-values-types';
import { VacancyDetail, VacancyFilter } from '../../models/vacancy';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './manage-vacancy.page.html',
})
export class ManageVacancyPage {
  public vacancyFormGroup: FormGroup;
  public vacancyFilterModel: VacancyFilter = new VacancyFilter();
  public musicGenres: MasterValueItem[];
  public musicianRoles: MasterValueItem[];
  public workTypes: MasterValueItem[];
  public cities: MasterValueItem[];
  public formSubmitted = false;

  public dropdownSettings = Constants.getAutocompleteSettings();

  constructor(
    private readonly backendService: BackendService,
    private readonly toastr: ToastrService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.vacancyFormGroup = this.formBuilder.group({
      title: ['', [Validators.required]],
      musicianRoles: ['', [Validators.required]],
      description: [''],
      minExpirience: [0],
      cities: ['']
    });

    this.backendService.getMasterValues(MasterValueTypes.MusicGenres)
      .subscribe((values) => this.musicGenres = values);
    this.backendService.getMasterValues(MasterValueTypes.MusicianRoles)
      .subscribe((values) => this.musicianRoles = values);
    this.backendService.getMasterValues(MasterValueTypes.WorkTypes)
      .subscribe((values) => this.workTypes = values);
    this.backendService.getMasterValues(MasterValueTypes.Cities)
      .subscribe((values) => this.cities = values);

    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.backendService.getUserVacancy(params["id"])
          .subscribe((vacancy: VacancyDetail) => {
            this.vacancyFilterModel = vacancy.vacancyFilter;
            this.formControls.musicianRoles.setValue(vacancy.vacancyFilter.musicianRoles);
            this.formControls.description.setValue(vacancy.description);
            this.formControls.title.setValue(vacancy.title);
            this.formControls.minExpirience.setValue(vacancy.vacancyFilter.minExpirience);
            this.formControls.cities.setValue(vacancy.vacancyFilter.cities);
          });
      }
    });
  }

  public get formControls() {
    return this.vacancyFormGroup.controls;
  }

  onSelect(value) {
    this.formControls.musicianRoles.setValue(value);
  }

  onDeSelect() {
    this.formControls.musicianRoles.setValue(null);
  }

  public submit() {
    this.formSubmitted = true;
    if (this.vacancyFormGroup.invalid) return;

    const vacancy = new VacancyDetail();
    vacancy.title = this.formControls.title.value;
    vacancy.description = this.formControls.description.value;
    vacancy.vacancyFilter = this.vacancyFilterModel;
    vacancy.vacancyFilter.minExpirience = this.formControls.minExpirience.value;

    this.backendService.updateOrCreateVacancy(vacancy)
      .subscribe((vacancy) => {
        this.vacancyFilterModel.id = vacancy.id;
        this.toastr.success("Вакансия успешно сохранена.");
        this.formSubmitted = false;
        this.router.navigate(['/my/vacancy', vacancy.id]);
      });
  }
}
