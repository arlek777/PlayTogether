import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { StoreModule } from '@ngrx/store';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomePage } from './home/page/home.page';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomePage
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        StoreModule.forRoot({}),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomePage },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
