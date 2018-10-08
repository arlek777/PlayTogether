import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { Response } from '@angular/http';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private readonly injector: Injector, private readonly toastrService: ToastrService) { }

  handleError(error) {
    console.log(error);
    if (error && error.error && error.status === 400) {
      this.toastrService.error(error.error, "Ошибка");
    } else {
      this.toastrService.error(error.message, "Ошибка");
    }
  }
}
