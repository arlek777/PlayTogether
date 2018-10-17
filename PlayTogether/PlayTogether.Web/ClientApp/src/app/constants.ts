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
      email: 'Неверный формат email адреса',
      phone: 'Неверный формат телефонного номера',
    };
    return config[validatorName];
  }
}

export class RegExp {
  static phoneMask = ['(', /[0-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, '-', /\d/, /\d/];
  static emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
}
