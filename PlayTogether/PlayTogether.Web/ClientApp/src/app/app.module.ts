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
import { TextMaskModule } from 'angular2-text-mask';

// Components
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { ControlValidationComponent } from './components/control-validation/control-validation.component';

// Pages
import { HomePage } from './pages/home/home.page';
import { LoginPage } from './pages/login/login.page';
import { SelectUserTypePage } from './pages/select-user-type/select-user-type.page';
import { UserProfilePage } from './pages/user-profile/user-profile.page';
import { MainPage } from './pages//user-profile/main/main.page';
import { SkillsPage } from './pages//user-profile/skills/skills.page';
import { ManageVacanciesPage } from './pages/manage-vacancies/manage-vacancies.page';
import { ManageVacancyPage } from './pages/manage-vacancy/manage-vacancy.page';
import { SearchVacanciesPage } from './pages/search-vacancies/search-vacancies.page';
import { VacancyPage } from './pages/vacancy/vacancy.page';
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
    ControlValidationComponent,
    HomePage,
    LoginPage,
    SelectUserTypePage,
    UserProfilePage,
    MainPage,
    SkillsPage,
    ManageVacanciesPage,
    ManageVacancyPage,
    ProfilePage,
    VacancyPage,
    SearchVacanciesPage
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot(appEffects),
    BrowserAnimationsModule,
    TextMaskModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-center'
    }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgMultiSelectDropDownModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: SearchVacanciesPage, pathMatch: 'full', canActivate: [AuthGuard] },
      {
        path: 'my/profile', component: UserProfilePage, canActivate: [AuthGuard],
        children: [
          { path: 'main', component: MainPage },
          { path: 'skills', component: SkillsPage },
          { path: "**", redirectTo: 'main' }
        ]
      },
      { path: 'login', component: LoginPage },
      { path: 'select-user-type', component: SelectUserTypePage, canActivate: [AuthGuard] },
      { path: 'my/vacancies', component: ManageVacanciesPage, canActivate: [AuthGuard] },
      { path: 'my/vacancy', component: ManageVacancyPage, canActivate: [AuthGuard] },
      { path: 'my/vacancy/:id', component: ManageVacancyPage, canActivate: [AuthGuard] },
      //{ path: 'search-vacancies', component: SearchVacanciesPage, canActivate: [AuthGuard] },
      { path: 'vacancy', component: VacancyPage, canActivate: [AuthGuard] },
      { path: 'vacancy/:id', component: VacancyPage, canActivate: [AuthGuard] },
      { path: 'profile', component: ProfilePage, canActivate: [AuthGuard] },
      { path: 'profile/:id', component: ProfilePage, canActivate: [AuthGuard] },
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
