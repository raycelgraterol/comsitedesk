import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ErrorInterceptor } from './core/helpers/error.interceptor';
import { JwtInterceptor } from './core/helpers/jwt.interceptor';

import { LayoutsModule } from './layouts/layouts.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserProfileService } from './core/services/security/user.service';
import { DatePipe, DecimalPipe } from '@angular/common';
import { RolesService } from './core/services/security/roles.service';

import { SocialLoginModule, AuthServiceConfig } from "angularx-social-login";
import { environment } from '../environments/environment'
import { GoogleLoginProvider, FacebookLoginProvider } from "angularx-social-login";
import { TicketTypesService } from './core/services/ticket-types.service';
import { TicketService } from './core/services/ticket.service';
import { TicketCategoriesService } from './core/services/ticket-categories.service';
import { TicketProcessesService } from './core/services/ticket-processes.service';
import { TicketStatusService } from './core/services/ticket-status.service';

import { HeadquarterService } from './core/services/headquarter.service';
import { DepartmentService } from './core/services/department.service';
import { EquipmentUserService } from './core/services/equipment-user.service';
import { EquipmentService } from './core/services/equipment.service';

import { FormViewsService } from './core/services/security/form-views.service';
import { FormActionsService } from './core/services/security/form-actions.service';
import { SecurityModulesService } from './core/services/security/security-modules.service';

let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider(`${environment.googleClientId}`)
  }
]);

export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    LayoutsModule,
    AppRoutingModule,
    SocialLoginModule
  ],
  providers: [
    { provide: AuthServiceConfig, useFactory: provideConfig },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    DecimalPipe,
    DatePipe,
    UserProfileService,
    RolesService,
    FormViewsService,
    FormActionsService,
    SecurityModulesService,
    RolesService,
    TicketTypesService,
    TicketService,
    TicketCategoriesService,
    TicketProcessesService,
    TicketStatusService,
    HeadquarterService,
    DepartmentService,
    EquipmentUserService,
    EquipmentService
    // provider used to create fake backend
    //FakeBackendProvider    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
