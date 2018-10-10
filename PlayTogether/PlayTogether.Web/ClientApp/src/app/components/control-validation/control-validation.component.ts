import { Component, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ValidationMessages } from '../../constants';

@Component({
  selector: 'control-validation',
  template: `<div class="error-msg messages text-left text-danger" *ngIf="errorMessage !== null">{{errorMessage}}</div>`
})
export class ControlValidationComponent {
  @Input() control: FormControl;

  get errorMessage(): string {
    for (let propertyName in this.control.errors) {
      if (this.control.errors.hasOwnProperty(propertyName) && this.control.touched) {
        return ValidationMessages.get(propertyName, this.control.errors[propertyName]);
      }
    }
    
    return null;
  }
}
