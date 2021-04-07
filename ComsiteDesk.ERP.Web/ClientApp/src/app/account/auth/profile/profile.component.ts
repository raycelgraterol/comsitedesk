import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../../../core/services/auth.service';
import Swal from 'sweetalert2';
import { User } from 'src/app/core/models/auth.models';

import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
// bread crum data
breadCrumbItems: Array<{}>;

//Current User
user: User;

// bootstrap validation form
validationform: FormGroup; 

error = '';
message = '';
loading = false;
show = false;

imageUrl: string = "assets/images/users/user-icon-default.png";
fileToUpload: File = null;

// Form submition
submit: boolean;
constructor(public formBuilder: FormBuilder, private authenticationService: AuthenticationService) { }

ngOnInit() {
   // tslint:disable-next-line: max-line-length
   this.breadCrumbItems = [{ label: 'Inicio', path: '/' }, { label: 'Perfil', path: '/auth/profile', active: true }];

   /**
    * Bootstrap validation form data
    */
   this.validationform = this.formBuilder.group({
     Id: [0],
     FirstName: ['', [Validators.required, Validators.pattern('[a-zA-Z0-9 ]+')]],
     LastName: ['', [Validators.required, Validators.pattern('[a-zA-Z0-9 ]+')]],
     Email: [{value: '', disabled: true}, [Validators.required, Validators.email]],
     PhoneNumber: ['', [Validators.required, Validators.pattern('[0-9]+')]]
   });

   this.user = this.authenticationService.currentUser();

   this.validationform.controls.Id.setValue(this.user.id);
   this.validationform.controls.FirstName.setValue(this.user.firstName);
   this.validationform.controls.LastName.setValue(this.user.lastName);
   this.validationform.controls.Email.setValue(this.user.email);
   this.validationform.controls.PhoneNumber.setValue(this.user.phoneNumber);
}

/**
 * Returns form
 */
get form() {
  return this.validationform.controls;
}

/**
 * Bootsrap validation form submit method
 */
validSubmit() {
  this.submit = true;
  this.loading = true;

  // stop here if form is invalid
  if (this.validationform.invalid) {

    this.loading = false;
    return;
  }

  if (this.form.Id.value != 0 || this.form.Id.value != null) {
    this.authenticationService.updateProfile(this.form.Email.value, this.form.FirstName.value,
       this.form.LastName.value, this.form.PhoneNumber.value)
      .pipe(first())
      .subscribe(
        data => {
          Swal.fire({
            position: 'center',
            type: data.type,
            title: data.message,
            showConfirmButton: false,
            timer: 1500
          });
          this.show = true;
        },
        error => {
          this.error = error;
        });
  } 
  this.loading = false;
}

}
