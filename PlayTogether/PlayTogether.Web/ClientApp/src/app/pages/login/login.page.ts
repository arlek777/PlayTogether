import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { JwtTokens } from '../../models/jwt-tokens';
import { Constants } from '../../constants';
import { UserActionTypes, Login } from '../../store/user/actions';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/login';
import { AppState } from '../../store';

@Component({
  templateUrl: './login.page.html',
})
export class LoginPage {
  userName: string;
  password: string;

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router) {

    this.store.subscribe(state => {
      if (state.user.isLoggedIn) {
        this.router.navigate(['/']);
      }
    });
  }

  login() {
    this.store.dispatch(new Login(new LoginModel(this.userName, this.password)));
  }
}
