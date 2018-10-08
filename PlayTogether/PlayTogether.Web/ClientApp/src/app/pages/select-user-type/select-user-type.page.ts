import { Component } from '@angular/core';
import { BackendService } from '../../services/backend.service';
import { Store } from '@ngrx/store';
import { Constants } from '../../constants';
import { UserActionTypes, Login, UpdateUserType } from '../../store/user/actions';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/login';
import { AppState } from '../../store';
import { UserType } from '../../models/user-type';
import { SelectUserType } from '../../models/select-user-type';

@Component({
  templateUrl: './select-user-type.page.html',
})
export class SelectUserTypePage {
  public selectUserType: SelectUserType;
  public UserType = UserType;

  constructor(
    private readonly backendService: BackendService,
    private readonly store: Store<AppState>,
    private readonly router: Router) {

    this.selectUserType = new SelectUserType();
    this.selectUserType.userType = UserType.Musician;
    this.store.select(s => s.user.id)
      .subscribe((value) => this.selectUserType.userId = value);
  }

  public submit() {
    this.store.dispatch(new UpdateUserType(this.selectUserType));
  }
}
