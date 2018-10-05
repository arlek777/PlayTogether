import { Component } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { BackendService } from '../../../services/backend.service';
import { MasterValueItem } from '../../../models/master-value-item';
import { ProfileSkills } from '../../../models/profile-skills';
import { MasterValueTypes } from '../../../models/master-values-types';
import { AppState } from '../../../store';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './skills.page.html',
})
export class SkillsPage {
  profileSkillsModel: ProfileSkills = new ProfileSkills();
  musicGenres: MasterValueItem[];
  musicianRoles: MasterValueItem[];
  userId: string;

  dropdownSettings: IDropdownSettings = {
    enableCheckAll: false,
    singleSelection: false,
    idField: 'id',
    textField: 'title',
    itemsShowLimit: 10,
    allowSearchFilter: true,
    closeDropDownOnSelection: true
  };

  constructor(private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly toastr: ToastrService) {

    this.store.select(s => s.user.id)
      .subscribe((value) => this.userId = value);
  }

  ngOnInit() {
    this.backendService.getMasterValues(MasterValueTypes.MusicGenres)
      .subscribe((values) => this.musicGenres = values);

    this.backendService.getMasterValues(MasterValueTypes.MusicianRoles)
      .subscribe((values) => this.musicianRoles = values);

    this.backendService.getProfileSkills(this.userId)
      .subscribe((value) => this.profileSkillsModel = value);
  }

  async updateSkills() {
    this.backendService.updateProfileSkills(this.profileSkillsModel)
      .subscribe(() => this.toastr.success("Ваш профиль сохранен."));
  }
}