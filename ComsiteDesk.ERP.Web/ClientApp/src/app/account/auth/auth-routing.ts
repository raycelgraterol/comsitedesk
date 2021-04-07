import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { PasswordresetComponent } from './passwordreset/passwordreset.component';
import { PasswordchangeComponent } from './passwordchange/passwordchange.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'signup',
        component: SignupComponent
    },
    {
        path: 'confirm',
        component: ConfirmComponent
    },
    {
        path: 'reset-password',
        component: PasswordresetComponent
    },
    {
        path: 'profile',
        component: ProfileComponent
    },
    {
        path: 'passwordchange',
        component: PasswordchangeComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule { }
