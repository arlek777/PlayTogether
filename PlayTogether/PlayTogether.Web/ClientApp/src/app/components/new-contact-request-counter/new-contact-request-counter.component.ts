import { Component, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ValidationMessages } from '../../constants';
import { BackendService } from '../../services/backend.service';

@Component({
  selector: 'new-contact-request-counter',
  template: `<span class="badge badge-light">{{newContactRequestCount}}</span>`
})
export class NewContactRequestCounterComponent {
  public newContactRequestCount = 0;

  constructor(
    private readonly backendService: BackendService) {
  }

  ngOnInit() {
    this._updateContactRequestCounter();
    setInterval(this._updateContactRequestCounter, 60000); // update each 1 minute
  }

  private _updateContactRequestCounter() {
    this.backendService.getUserNewContactRequestCount().subscribe((value: number) => {
      this.newContactRequestCount = value;
    });
  }
}
