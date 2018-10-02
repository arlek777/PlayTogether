import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { AppState } from '../../store';
import { Logout } from '../../store/auth/actions';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  userName$: Observable<string>;
  isLoggedIn$: Observable<boolean>;

  constructor(private store: Store<AppState>) {
    this.userName$ = this.store.pipe(select(s => s.auth.userName));
    this.isLoggedIn$ = this.store.pipe(select(s => s.auth.isLoggedIn));
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.store.dispatch(new Logout());
  }
}
