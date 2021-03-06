import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { Constants } from '../../constants';
import { UserActionTypes, Login } from '../../store/user/actions';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/login';
import { AppState } from '../../store';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  templateUrl: './login.page.html',
})
export class LoginPage {
  public loginForm: FormGroup;
  public formSubmitted = false;

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router,
    private readonly formBuilder: FormBuilder) {

    this.store.subscribe(state => {
      if (state.user.isLoggedIn) {
        this.router.navigate(['/']);
      }
    });
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  public login() {
    this.formSubmitted = true;
    if (this.loginForm.invalid) return;
    const loginModel = new LoginModel(this.formControls.userName.value, this.formControls.password.value);
    this.store.dispatch(new Login(loginModel));
    this.formSubmitted = false;
  }

  public get formControls() {
    return this.loginForm.controls;
  }
}
