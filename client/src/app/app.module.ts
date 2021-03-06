import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';

import { SharedModule } from '@shared/shared.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';

const NG_MODULES = [BrowserAnimationsModule, BrowserModule, HttpClientModule];

const THIRD_PARTY_MODULES = [
  FontAwesomeModule,
  NgbModule,
  ToastrModule.forRoot({
    timeOut: 10000,
    positionClass: 'toast-bottom-right',
    preventDuplicates: false,
  }),
];

const APP_MODULES = [AppRoutingModule, CoreModule, SharedModule];

@NgModule({
  declarations: [AppComponent],
  imports: [NG_MODULES, THIRD_PARTY_MODULES, APP_MODULES],
  bootstrap: [AppComponent],
})
export class AppModule {}
