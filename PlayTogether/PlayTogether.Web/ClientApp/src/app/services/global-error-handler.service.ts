import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { Response } from '@angular/http';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private readonly ngZone: NgZone,
    private readonly toastrService: ToastrService) { }

  handleError(error) {
    console.log(error);
    this.ngZone.run(() => {
      if (this.toastrService) {
        if (error && error.message) {
          this.toastrService.error(error.message);
        } else if (error && error.error) {
          this.toastrService.error(error.error);
        } else {
          this.toastrService.error("Произошла ошибка.");
        }
      }
    });
  }
}
