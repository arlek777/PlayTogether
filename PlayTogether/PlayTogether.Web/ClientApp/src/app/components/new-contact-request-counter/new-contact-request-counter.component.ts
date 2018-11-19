import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { AppState } from '../../store';

@Component({
  selector: 'new-contact-request-counter',
  template: `<span class="badge badge-secondary">{{newContactRequestCount}}</span>`
})
export class NewContactRequestCounterComponent {
  public newContactRequestCount = 0;

  private _isLoggedIn = false;
  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>) {
  }

  ngOnInit() {
    this.store.select(s => s.user.isLoggedIn).subscribe((isLoggedIn) => {
      this._isLoggedIn = isLoggedIn;
    });
    
    setInterval(() => this._updateContactRequestCounter(), 10000);
  }

  private _updateContactRequestCounter() {
    if (!this._isLoggedIn) return;

    this.backendService.getUserNewContactRequestCount().subscribe((value: number) => {
      this.newContactRequestCount = value;
    });
  }
}
