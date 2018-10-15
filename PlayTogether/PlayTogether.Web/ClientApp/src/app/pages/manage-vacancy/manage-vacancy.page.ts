import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Constants } from '../../constants';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterValueItem } from '../../models/master-value-item';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { MasterValueTypes } from '../../models/master-values-types';
import { VacancyDetail } from '../../models/vacancy';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './manage-vacancy.page.html',
})
export class ManageVacancyPage {
  public vacancyFormGroup: FormGroup;
  public selectedMusicRoles: MasterValueItem[];
  public selectedMusicGenres: MasterValueItem[];
  public musicGenres: MasterValueItem[];
  public musicianRoles: MasterValueItem[];
  public formSubmitted = false;

  public dropdownSettings: IDropdownSettings = {
    enableCheckAll: false,
    singleSelection: false,
    idField: 'id',
    textField: 'title',
    itemsShowLimit: 10,
    allowSearchFilter: true,
    closeDropDownOnSelection: true
  };

  private vacancyId: string;

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
      description: [''],
      minExpirience: [0],
      cities: ['']
    });

    this.backendService.getMasterValues(MasterValueTypes.MusicGenres)
      .subscribe((values) => this.musicGenres = values);

    this.backendService.getMasterValues(MasterValueTypes.MusicianRoles)
      .subscribe((values) => this.musicianRoles = values);

    // TODO add work types

    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.backendService.getVacancy(params["id"])
          .subscribe((vacancy: VacancyDetail) => {
            this.vacancyId = vacancy.id;
            this.formControls.description.setValue(vacancy.description);
            this.formControls.title.setValue(vacancy.title);
            this.formControls.minExpirience.setValue(vacancy.vacancyFilter.minExpirience);
            this.formControls.cities.setValue(vacancy.vacancyFilter.cities);
            this.selectedMusicGenres = vacancy.vacancyFilter.musicGenres;
            this.selectedMusicRoles = vacancy.vacancyFilter.musicianRoles;
          });
      }
    });
  }

  public get formControls() {
    return this.vacancyFormGroup.controls;
  }

  public submit() {
    this.formSubmitted = true;
    if (this.vacancyFormGroup.invalid) return;

    const vacancy = new VacancyDetail();
    vacancy.id = this.vacancyId;
    vacancy.title = this.formControls.title.value;
    vacancy.description = this.formControls.description.value;
    vacancy.vacancyFilter.cities = [];
    vacancy.vacancyFilter.minExpirience = this.formControls.minExpirience.value;
    vacancy.vacancyFilter.musicGenres = this.selectedMusicGenres;
    //vacancy.vacancyFilter.workTypeIds = this.formControls.title.value;
    vacancy.vacancyFilter.musicianRoles = this.selectedMusicRoles;

    this.backendService.updateOrCreateVacancy(vacancy)
      .subscribe((vacancy) => {
        this.vacancyId = vacancy.id;
        this.toastr.success("Вакансия успешно сохранена.");
        this.formSubmitted = false;
        this.router.navigate(['/vacancy', this.vacancyId]);
      });
  }
}
