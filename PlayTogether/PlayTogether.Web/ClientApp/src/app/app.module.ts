// Angular/libs
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ToastrModule } from 'ngx-toastr';
import { MatSliderModule } from '@angular/material/slider';
import { TextMaskModule } from 'angular2-text-mask';

// Components
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { ControlValidationComponent } from './components/control-validation/control-validation.component';

// Pages
import { HomePage } from './pages/home/home.page';
import { LoginPage } from './pages/login/login.page';
import { SelectUserTypePage } from './pages/select-user-type/select-user-type.page';
import { ProfilePage } from './pages/profile/profile.page';
import { MainPage } from './pages/profile/main/main.page';
import { SkillsPage } from './pages/profile/skills/skills.page';
import { VacanciesPage } from './pages/vacancies/vacancies.page';
import { VacancyPage } from './pages/vacancy/vacancy.page';

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
    ControlValidationComponent,
    HomePage,
    LoginPage,
    SelectUserTypePage,
    ProfilePage,
    MainPage,
    SkillsPage,
    VacanciesPage,
    VacancyPage
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot(appEffects),
    BrowserAnimationsModule,
    MatSliderModule,
    TextMaskModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-center'
    }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
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
      { path: 'select-user-type', component: SelectUserTypePage, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'vacancies', component: VacanciesPage, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'vacancy', component: VacancyPage, pathMatch: 'full', canActivate: [AuthGuard] },
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
