export class Constants {
    static accessTokenKey = "accessToken";
    static currentUserKey = "user";
}

export class ValidationMessages {
  static get(validatorName: string, validatorValue?: any): string {
    let config = {
      required: 'Обязательное поле',
      pattern: 'Неверный формат поля',
      minlength: `Минимальная длина ${validatorValue.requiredLength}`,
      maxlength: `Максимальная длина ${validatorValue.requiredLength}`,
      min: `Минимальное значение ${validatorValue.min}`,
      max: `Максимальное значение ${validatorValue.max}`,
      email: 'Неверный формат email адреса',
      phone: 'Неверный формат телефонного номера',
    };
    return config[validatorName];
  }
}

export class RegExp {
  static phoneMask = ['(', /[0-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, '-', /\d/, /\d/];
  static urlMask = '/^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.​\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[​6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1​,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00​a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u​00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/[^\s]*)?$/i;';
}
