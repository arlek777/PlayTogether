import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BackendService } from '../../../services/backend.service';
import { MainProfileInfo } from '../../../models/main-profile-info';
import { AppState } from '../../../store';
import { UserType } from '../../../models/user-type';
import { MasterValueItem } from '../../../models/master-value-item';
import { MasterValueTypes } from '../../../models/master-values-types';
import { Constants } from '../../../constants';

@Component({
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css']
})
export class MainPage implements OnInit {
  public mainInfoModel = new MainProfileInfo();
  public mainPageForm: FormGroup;
  public formSubmitted = false;
  public userType: UserType;
  public musicGenres: MasterValueItem[];
  public musicianRoles: MasterValueItem[];
  public workTypes: MasterValueItem[];
  public userName: string;
  public isUserProfileFilled: boolean;
  public isPhotoZoomed = false;
  public isPhotoLoading = false;

  public dropdownSettings = Constants.getAutocompleteSettings();
  private _redirectToContactPage = false;

  constructor(private readonly formBuilder: FormBuilder,
    private readonly toastr: ToastrService,
    private readonly backendService: BackendService,
    private readonly router: Router,
    private readonly store: Store<AppState>) {

    this.store.select(s => s.user).subscribe((user) => {
      this.userType = user.userType;
      this.userName = user.userName;
    });
  }

  ngOnInit() {
    this.setMainPageValidator();

    this.backendService.getMasterValues(MasterValueTypes.MusicGenres)
      .subscribe((values) => this.musicGenres = values);
    this.backendService.getMasterValues(MasterValueTypes.MusicianRoles)
      .subscribe((values) => this.musicianRoles = values);
    this.backendService.getMasterValues(MasterValueTypes.WorkTypes)
      .subscribe((values) => this.workTypes = values);

    this.backendService.getMainProfileInfo().subscribe(profile => {
      if (profile === null) {
        this.mainInfoModel = new MainProfileInfo();
        this._redirectToContactPage = true;
        this.isUserProfileFilled = false;
      } else {
        this.mainInfoModel = profile;
        this.isUserProfileFilled = true;
      }
      this.formControls.name.setValue(this.mainInfoModel.name);
      this.formControls.photo.setValue(this.mainInfoModel.photoBase64);
      if (this.isGroup) {
        this.formControls.groupName.setValue(this.mainInfoModel.groupName);
      } else if (this.isMusician) {
        this.formControls.musicianRoles.setValue(this.mainInfoModel.musicianRoles);
        this.formControls.experience.setValue(this.mainInfoModel.experience);
        this.formControls.age.setValue(this.mainInfoModel.age);
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

  get photoZoomState() {
    return this.isPhotoZoomed ? 'active' : 'inactive';
  }

  onSelect(value) {
    this.formControls.musicianRoles.setValue(value);
  }

  onDeSelect() {
    this.formControls.musicianRoles.setValue(null);
  }

  onPhotoUploaded(event) {
    if (!event.target.files || event.target.files.length === 0) {
      return;
    }
    const file = event.target.files[0];
    if (file.size > Constants.maxImageSize) {
      this.toastr.error("Файл больше допустимого размера в 5 МБ.");
      return;
    }

    this.isPhotoLoading = true;
    var fileReader = new FileReader();
    fileReader.onloadend = (e) => {
      const photoBase64 = (<any>fileReader.result).split(',')[1];
      this.backendService.proccessPhoto(photoBase64).subscribe((response) => {
        this.mainInfoModel.photoBase64 = response.photoBase64;
        this.formControls.photo.setValue(this.mainInfoModel.photoBase64);
        this.isPhotoLoading = false;
      });
      
    }
    fileReader.readAsDataURL(file);
  }

  public submit() {
    this.formSubmitted = true;
    if (this.mainPageForm.invalid) return;

    this.mainInfoModel.isActivated = true;
    this.mainInfoModel.name = this.formControls.name.value;
    if (this.isGroup) {
      this.mainInfoModel.groupName = this.formControls.groupName.value;
    } else if (this.isMusician) {
      this.mainInfoModel.experience = this.formControls.experience.value;
      this.mainInfoModel.age = this.formControls.age.value;
    }

    this.backendService.updateMainProfileInfo(this.mainInfoModel)
      .subscribe(() => {
        this.formSubmitted = false;
        this.toastr.success("Ваш профиль сохранен.");
        if (this._redirectToContactPage) {
          this._redirectToContactPage = false;
          this.router.navigate(['/my/profile/contact']);
        }
      });
  }

  private setMainPageValidator() {
    this.mainPageForm = this.formBuilder.group({
      name: ['', [
        Validators.required,
        Validators.minLength(2)
      ]],
      photo: ['', []]
    });

    if (this.isGroup) {
      this.mainPageForm.addControl('groupName', this.formBuilder.control('', [
        Validators.required
      ]));
    } else if (this.isMusician) {
      this.mainPageForm.addControl('age', this.formBuilder.control('', [
        Validators.maxLength(2),
        Validators.min(0),
        Validators.max(95)
      ]));

      this.mainPageForm.addControl('musicianRoles', this.formBuilder.control('', [
        Validators.required
      ]));

      this.mainPageForm.addControl('experience', this.formBuilder.control('', [
        Validators.min(0),
        Validators.max(95)
      ]));
    }
  }
}
