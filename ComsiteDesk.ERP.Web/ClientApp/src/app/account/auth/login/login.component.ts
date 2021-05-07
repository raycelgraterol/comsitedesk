import { Component, OnInit, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../../../core/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from "angularx-social-login";
import { GoogleLoginProvider, SocialUser } from "angularx-social-login";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, AfterViewInit {

  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;
  error = '';
  loading = false;
  isLogging: boolean;
  googleImage: string;

  private user: SocialUser;

  constructor(
    private formBuilder: FormBuilder, 
    private route: ActivatedRoute, 
    private router: Router,
    private authenticationService: AuthenticationService,
    private authService: AuthService) { }

  ngOnInit() {
    this.googleImage = 'assets/images/google_64.jpg';
    this.authService.authState.subscribe((user) => {
      this.user = user;
    });

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required]],
      password: ['', Validators.required],
    });

    // reset login status
    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    // tslint:disable-next-line: no-string-literal
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  ngAfterViewInit() {
    document.body.classList.add('authentication-bg');
    document.body.classList.add('authentication-bg-pattern');
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  signInWithGoogle(): void {
    this.loading = true;

    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID)
    .then(googleUser => {
      this.authenticationService.googleLogin(googleUser)
        .subscribe((data) => {          
          this.router.navigate([this.returnUrl]);
          this.loading = false;
        });
    }).catch(error => {
      console.log(error);
      this.loading = false;
    });
  }

  signOut(): void {
    this.authService.signOut();
  }

  /**
   * On submit form
   */
  onSubmit() {
    this.submitted = true;
    this.loading = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    
    this.authenticationService.login(this.f.email.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
          this.loading = false;
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }  
}
