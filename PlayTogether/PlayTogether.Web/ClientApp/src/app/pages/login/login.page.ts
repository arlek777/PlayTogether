import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { AuthState } from '../../store/auth/reducers';
import { JwtTokens } from '../../models/jwt-tokens';
import { Constants } from '../../constants';
import { CommonAction } from 'src/app/models/common-action';
import { AuthActionTypes } from '../../store/auth/actions';
import { Router } from '@angular/router';

@Component({
  templateUrl: './login.page.html',
})
export class LoginPage {
  userName: string;
  password: string;

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AuthState>,
    private readonly router: Router) {

    this.store.subscribe(state => {
      if (state.isLoggedIn) {
        this.router.navigate(['/home']);
      }
    });
  }

  async login() {
    //todo add validation

    const jwtTokens: JwtTokens = await this.backendService.login(this.userName, this.password);
    window.localStorage.setItem(Constants.accessTokenKey, jwtTokens.accessToken);
    window.localStorage.setItem(Constants.currentUserKey, jwtTokens.userName);

    this.store.dispatch(new CommonAction(AuthActionTypes.Login, jwtTokens.userName));
  }
}
