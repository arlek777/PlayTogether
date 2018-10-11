export class Constants {
    static accessTokenKey = "accessToken";
    static currentUserKey = "user";
}

export class RegExp {
  static phoneMask = ['(', /[0-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, '-', /\d/, /\d/];
  static namePattern = /^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$/;
  static emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
}
