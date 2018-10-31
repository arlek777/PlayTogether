import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { BackendService } from '../../../services/backend.service';
import { ContactInfo } from '../../../models/contact-profile-info';
import { AppState } from '../../../store';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserType } from '../../../models/user-type';
import { RegExp, Constants } from '../../../constants';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { MasterValueTypes } from '../../../models/master-values-types';
import { MasterValueItem } from '../../../models/master-value-item';

@Component({
  templateUrl: './contact.page.html',
})
export class ContactPage {
  public contactInfoModel: ContactInfo = new ContactInfo();
  public formSubmitted = false;
  public userType: UserType;
  public form: FormGroup;
  public phoneMask = RegExp.phoneMask;
  public cities: MasterValueItem[];
  public contactTypes: MasterValueItem[];
  public selectedCity: MasterValueItem[];

  public cityDropdownSettings: IDropdownSettings = {
    enableCheckAll: false,
    singleSelection: true,
    idField: 'id',
    textField: 'title',
    itemsShowLimit: 10,
    allowSearchFilter: true,
    closeDropDownOnSelection: true,
    noDataAvailablePlaceholderText: 'Загрузка..'
  };
  public contactTypesDropdownSettings: IDropdownSettings = Constants.getAutocompleteSettings();

  constructor(private readonly backendService: BackendService,
    private readonly formBuilder: FormBuilder,
    private readonly store: Store<AppState>,
    private readonly toastr: ToastrService) {

    this.store.select(s => s.user.userType).subscribe((type) => this.userType = type);
  }

  ngOnInit() {
    this.setPageValidator();

    this.backendService.getMasterValues(MasterValueTypes.Cities)
      .subscribe((values) => this.cities = values);
    this.backendService.getMasterValues(MasterValueTypes.ContactTypes)
      .subscribe((values) => this.contactTypes = values);

    this.backendService.getContactProfileInfo()
      .subscribe((profile) => {
        if (profile === null) {
          this.contactInfoModel = new ContactInfo();
        } else {
          this.contactInfoModel = profile;
        }
        this.selectedCity = [this.contactInfoModel.city];

        this.formControls.email.setValue(this.contactInfoModel.contactEmail);
        this.formControls.city.setValue(this.contactInfoModel.city);
        this.formControls.phone1.setValue(this.contactInfoModel.phone1);
        this.formControls.url1.setValue(this.contactInfoModel.url1);
        this.formControls.url2.setValue(this.contactInfoModel.url2);
      });
  }

  get formControls() {
    return this.form.controls;
  }

  public submit() {
    this.formSubmitted = true;
    if (this.form.invalid) return;

    this.contactInfoModel.contactEmail = this.formControls.email.value;
    this.contactInfoModel.phone1 = this.formControls.phone1.value;
    this.contactInfoModel.url1 = this.formControls.url1.value;
    this.contactInfoModel.url2 = this.formControls.url2.value;
    this.contactInfoModel.city = this.selectedCity[0];

    this.backendService.updateProfileSkills(this.contactInfoModel)
      .subscribe(() => this.toastr.success("Ваш профиль сохранен."));
  }

  public onCitySelect(city) {
    this.formControls.city.setValue(city);
  }

  private setPageValidator() {
    this.form = this.formBuilder.group({
      email: [
        '', [
          Validators.required,
          Validators.email
        ]
      ],
      phone1: [
        '', [
          Validators.required
        ]
      ],
      url1: [
        '', [
          Validators.maxLength(256),
          //Validators.pattern(RegExp.urlMask)
        ]
      ],
      city: [
        '', [
          Validators.required
        ]
      ],
      url2: [
        '', [
          Validators.maxLength(256),
          //Validators.pattern(RegExp.urlMask)
        ]
      ]});
  }
}
