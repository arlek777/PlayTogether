import { Component, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ValidationMessages } from '../../constants';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { AppState } from '../../store';

@Component({
  selector: 'new-contact-request-counter',
  template: `<span class="badge badge-secondary">{{newContactRequestCount}}</span>`
})
export class NewContactRequestCounterComponent {
  public newContactRequestCount = 0;

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>) {
  }

  ngOnInit() {
    this.store.select(s => s.user.isLoggedIn).subscribe((isLoggedIn) => {
      this._updateContactRequestCounter();
      setInterval(() => this._updateContactRequestCounter(), 60000); // update each 1 minute
    });
  }

  private _updateContactRequestCounter() {
    this.backendService.getUserNewContactRequestCount().subscribe((value: number) => {
      this.newContactRequestCount = value;
    });
  }
}