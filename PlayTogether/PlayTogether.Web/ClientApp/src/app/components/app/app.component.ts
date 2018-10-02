import { Component } from '@angular/core';
import { Constants } from '../../constants';
import { Store } from '@ngrx/store';
import { AuthState } from '../../store/auth/reducers';
import { AuthActionTypes, AutoLogin } from '../../store/auth/actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Play Together';

  constructor(private readonly store: Store<AuthState>) {
      this.store.dispatch(new AutoLogin());
  }
}
