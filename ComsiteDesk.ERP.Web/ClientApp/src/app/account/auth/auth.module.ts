import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { ArchwizardModule } from 'angular-archwizard';

import { UIModule } from '../../shared/ui/ui.module';
import { NgxMaskModule } from 'ngx-mask';

import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { AuthRoutingModule } from './auth-routing';
import { ConfirmComponent } from './confirm/confirm.component';
import { PasswordresetComponent } from './passwordreset/passwordreset.component';

import { PasswordchangeComponent } from './passwordchange/passwordchange.component';
import { SignupUserComponent } from './signup-user/signup-user.component';

@NgModule({
  declarations: [
    LoginComponent,
    SignupComponent,
    ConfirmComponent,
    PasswordresetComponent,
    PasswordchangeComponent,    
    SignupUserComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbAlertModule,
    UIModule,
    AuthRoutingModule,
    ArchwizardModule,
    NgxMaskModule.forRoot()
  ]
})
export class AuthModule { }
