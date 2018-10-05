// Angular/libs
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ToastrModule } from 'ngx-toastr';

// Components
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';

// Pages
import { HomePage } from './pages/home/home.page';
import { LoginPage } from './pages/login/login.page';
import { ProfilePage } from './pages/profile/profile.page';
import { MainPage } from './pages/profile/main/main.page';
import { SkillsPage } from './pages/profile/skills/skills.page';

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
    ProfilePage,
    MainPage,
    SkillsPage
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot(appEffects),
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-center'
    }),
    HttpClientModule,
    FormsModule,
    NgMultiSelectDropDownModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomePage, pathMatch: 'full', canActivate: [AuthGuard] },
      {
        path: 'profile', component: ProfilePage, canActivate: [AuthGuard],
        children: [
          { path: 'main', component: MainPage },
          { path: 'skills', component: SkillsPage },
          { path: "**", redirectTo: 'main' }
        ]
      },
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
