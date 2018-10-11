import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { AppState } from '../../store';
import { Logout } from '../../store/user/actions';
import { UserState } from '../../store/user/reducers';
import { UserType } from '../../models/user-type';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html'
})
export class NavMenuComponent {
  public isExpanded = false;
  public user: UserState;

  constructor(private store: Store<AppState>) {
    this.store.select(s => s.user).subscribe(user => {
      this.user = user;
    });
  }

  public collapse() {
    this.isExpanded = false;
  }

  public toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public logout() {
    this.store.dispatch(new Logout());
  }

  public get isGroupUser() {
    return this.user.userType === UserType.Group;
  }
}
