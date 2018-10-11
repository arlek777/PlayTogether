export class Constants {
    static accessTokenKey = "accessToken";
    static currentUserKey = "user";
}

export class ValidationMessages {
  static get(validatorName: string, validatorValue?: any): string {
    let config = {
      required: 'Обязательное поле',
      minlength: `Минимальная длина ${validatorValue.requiredLength}`,
      email: 'Неверный формат email адреса',
      phone: 'Неверный формат телефонного номера',
    };
    return config[validatorName];
  }
}
