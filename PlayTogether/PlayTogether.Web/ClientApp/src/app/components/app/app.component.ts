import { Component } from '@angular/core';
import { Constants } from '../../constants';
import { Store } from '@ngrx/store';
import { UserState } from '../../store/user/reducers';
import { UserActionTypes, AutoLogin } from '../../store/user/actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Play Together';

  constructor(private readonly store: Store<UserState>) {
      this.store.dispatch(new AutoLogin());
  }
}
