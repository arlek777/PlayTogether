import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { JwtTokens } from '../../models/jwt-tokens';
import { Constants } from '../../constants';
import { AuthActionTypes, Login } from '../../store/auth/actions';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/login';
import { AppState } from '../../store';

@Component({
  templateUrl: './profile.page.html',
})
export class ProfilePage {
  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router) {
  }
}
