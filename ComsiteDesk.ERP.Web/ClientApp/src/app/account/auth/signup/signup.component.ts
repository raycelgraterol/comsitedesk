import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/core/services/auth.service';

import { WizardComponent as BaseWizardComponent } from 'angular-archwizard';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit, AfterViewInit {

  userForm: FormGroup;
  organizationForm: FormGroup;
  submittedUserForm = false;
  submittedOrganization = false;
  error = '';
  loading = false;
  returnUrl: string;


  @ViewChild('wizardForm', { static: false }) wizard: BaseWizardComponent;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {

    this.userForm = this.formBuilder.group({
      firstname: ['', Validators.required],
      lastname: [''],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: [''],
      password: ['', Validators.required],
      organizationId: [1, Validators.required]
    });

    this.organizationForm = this.formBuilder.group({
      id: [0],
      businessName: ['', Validators.required],
      RIF: [''],
      businessEmail: ['', [Validators.required, Validators.email]],
      businessPhoneNumber: [''],
      address: ['', Validators.required],
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
        this.f.organizationId.value,
        this.orf.keyAccess.value)
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

  // convenience getter for easy access to form fields
  get orf() { return this.organizationForm.controls; }

  /**
   * On submit Organization form
   */
  organizationFormSubmit() {
    this.submittedOrganization = true;
    this.loading = true;

    // stop here if form is invalid
    if (this.organizationForm.invalid) {
      this.loading = false;
      return;
    }

    this.authenticationService
      .registerOrganization(this.orf.businessName.value,
        this.orf.RIF.value,
        this.orf.businessEmail.value,
        this.orf.businessPhoneNumber.value,
        this.orf.address.value,
        this.orf.keyAccess.value)
      .pipe(first())
      .subscribe(
        data => {
          this.userForm.controls.organizationId.setValue(data.id);
          this.organizationForm.controls.id.setValue(data.id);
          this.wizard.navigation.goToNextStep();
          
          Swal.fire({
            position: 'center',
            type: "success",
            title: "Organizacion creada con exito!",
            showConfirmButton: false,
            timer: 1500
          });

          this.loading = false;
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }
}
