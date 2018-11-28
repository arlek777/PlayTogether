import { IDropdownSettings } from "ng-multiselect-dropdown/multiselect.model";

export class Constants {
  static accessTokenKey = "accessToken";
  static currentUserKey = "user";
  static maxImageSize = 5145729; // about 5mb
  static vacancyFiltersSessionStorageKey = "vacancyFilters";

  static getAutocompleteSettings(): IDropdownSettings {
    return {
      enableCheckAll: false,
      singleSelection: false,
      idField: 'id',
      textField: 'title',
      itemsShowLimit: 10,
      allowSearchFilter: true,
      closeDropDownOnSelection: true,
      noDataAvailablePlaceholderText: 'Загрузка..'
    }
  }
}

export class ValidationMessages {
  static get(validatorName: string, validatorValue?: any): string {
    let config = {
      required: 'Обязательное поле',
      pattern: 'Неверный формат поля',
      minlength: `Минимальная длина ${validatorValue!.requiredLength}`,
      maxlength: `Максимальная длина ${validatorValue!.requiredLength}`,
      min: `Минимальное значение ${validatorValue!.min}`,
      max: `Максимальное значение ${validatorValue!.max}`,
      email: 'Неверный формат email адреса',
      phone: 'Неверный формат телефонного номера',
    };
    return config[validatorName];
  }
}

export class RegExp {
  static phoneMask = ['(', /[0-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, '-', /\d/, /\d/];
}


