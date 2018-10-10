import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Constants } from '../../constants';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterValueItem } from '../../models/master-value-item';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { MasterValueTypes } from '../../models/master-values-types';
import { VacancyDetail } from '../../models/vacancy';
import { ToastrService } from 'ngx-toastr';

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

  private vacancy: VacancyDetail;

  constructor(
    private readonly backendService: BackendService,
    private readonly toastr: ToastrService,
    private readonly route: ActivatedRoute,
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

    this.backendService.getMasterValues(MasterValueTypes.MusicGenres)
      .subscribe((values) => this.musicGenres = values);

    this.backendService.getMasterValues(MasterValueTypes.MusicianRoles)
      .subscribe((values) => this.musicianRoles = values);

    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.backendService.getVacancy(params["id"])
          .subscribe((vacancy: VacancyDetail) => {
            this.vacancy = vacancy;
            this.formControls.status.setValue(vacancy.status);
            this.formControls.description.setValue(vacancy.description);
            this.formControls.title.setValue(vacancy.title);
            this.formControls.minExpirience.setValue(vacancy.vacancyFilter.minExpirience);
            this.formControls.cities.setValue(vacancy.vacancyFilter.cities);
            this.selectedMusicGenres = vacancy.vacancyFilter.musicGenreIds;
            this.selectedMusicRoles = vacancy.vacancyFilter.musicianRoleIds;
          });
      }
    });
  }

  public get formControls() {
    return this.vacancyFormGroup.controls;
  }

  public submit() {
    if (this.vacancyFormGroup.invalid) return;

    const vacancy = new VacancyDetail();
    vacancy.id = this.vacancy.id;
    vacancy.title = this.formControls.title.value;
    vacancy.description = this.formControls.description.value;
    vacancy.status = this.formControls.status.value;
    vacancy.vacancyFilter.cities = this.formControls.cities.value;
    vacancy.vacancyFilter.minExpirience = this.formControls.minExpirience.value;
    vacancy.vacancyFilter.musicGenreIds = this.selectedMusicGenres;
    //vacancy.vacancyFilter.workTypeIds = this.formControls.title.value;
    vacancy.vacancyFilter.musicianRoleIds = this.selectedMusicRoles;

    this.backendService.updateVacancy(vacancy)
      .subscribe(() => this.toastr.success("Вакансия успешно сохранена."));
  }
}
