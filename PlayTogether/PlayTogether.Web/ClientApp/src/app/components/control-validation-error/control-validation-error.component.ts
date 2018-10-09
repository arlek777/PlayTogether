import { Component, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'control-validation-error',
  template: `<div class="error-msg messages text-left text-danger" *ngIf="errorMessage !== null">{{errorMessage}}</div>`
})
export class ControlMessagesComponent {
  @Input() control: FormControl;
  @Input() errorMessages: any;

  get errorMessage() {
    for (let errorName in this.control.errors) {
      if (this.control.errors.hasOwnProperty(errorName) && this.control.touched) {
        return this.errorMessages[errorName];
      }
    }
    
    return null;
  }
}
