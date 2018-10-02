// Angular/libs
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

// Components
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';

// Pages
import { HomePage } from './pages/home/home.page';
import { LoginPage } from './pages/login/login.page';
import { ProfilePage } from './pages/profile/profile.page';

// Services
import { InterceptService } from './http.interceptor';
import { GlobalErrorHandler } from './services/global-error-handler.service';
import { BackendService } from './services/backend.service';
import { AuthGuard } from './services/auth-guard.service';

// Store
import { appReducers, appEffects } from './store';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomePage,
    LoginPage,
    ProfilePage
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot(appEffects),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomePage, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'profile', component: ProfilePage, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'login', component: LoginPage, pathMatch: 'full' },
    ])
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptService,
      multi: true
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    },
    BackendService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
