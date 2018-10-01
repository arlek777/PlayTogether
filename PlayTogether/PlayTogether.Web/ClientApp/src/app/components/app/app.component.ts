import { Component } from '@angular/core';
import { Constants } from '../../constants';
import { Store } from '@ngrx/store';
import { AuthState } from '../../store/auth/reducers';
import { AuthActionTypes } from '../../store/auth/actions';
import { CommonAction } from '../../models/common-action';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Play Together';

  constructor(private readonly store: Store<AuthState>) {
    const jwtToken = window.localStorage.getItem(Constants.accessTokenKey);
    const userName = window.localStorage.getItem(Constants.currentUserKey);

    if (!jwtToken || !userName) {
      window.localStorage.clear();
      window.sessionStorage.clear();
      this.store.dispatch(new CommonAction(AuthActionTypes.Logout));
    } else {
      this.store.dispatch(new CommonAction(AuthActionTypes.Login, userName));
    }
  }
}
