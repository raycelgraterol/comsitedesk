import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/core/services/auth.service';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-signup-user',
  templateUrl: './signup-user.component.html',
  styleUrls: ['./signup-user.component.scss']
})
export class SignupUserComponent implements OnInit, AfterViewInit {

  userForm: FormGroup;
  submittedUserForm = false;
  error = '';
  loading = false;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.loading = false;

    this.userForm = this.formBuilder.group({
      firstname: ['', Validators.required],
      lastname: [''],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: [''],
      password: ['', Validators.required],
      keyAccess: ['', Validators.required]
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  ngAfterViewInit() {
    document.body.classList.add('authentication-bg');
    document.body.classList.add('authentication-bg-pattern');
  }

  // convenience getter for easy access to form fields
  get f() { return this.userForm.controls; }

  /**
   * On submit user form
   */
  userFormSubmit() {
    this.submittedUserForm = true;
    this.loading = true;

    // stop here if form is invalid
    if (this.userForm.invalid) {
      this.loading = false;
      return;
    }

    this.authenticationService
      .register(
        this.f.firstname.value,
        this.f.lastname.value,
        this.f.password.value,
        this.f.email.value,
        this.f.phoneNumber.value,
        this.f.keyAccess.value)
      .pipe(first())
      .subscribe(
        data => {
          this.loading = false;
          this.router.navigate([this.returnUrl]);
          //TODO: confirmed mail
          //this.router.navigate(['/account/confirm']);
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }
  
}
