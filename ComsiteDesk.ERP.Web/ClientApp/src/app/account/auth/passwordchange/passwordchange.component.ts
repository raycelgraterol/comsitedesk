import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../../../core/services/security/auth.service';
import Swal from 'sweetalert2';

import { MustMatch } from './validation.mustmatch';
import { User } from 'src/app/core/models/auth.models';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-passwordchange',
  templateUrl: './passwordchange.component.html',
  styleUrls: ['./passwordchange.component.scss']
})
export class PasswordchangeComponent implements OnInit, AfterViewInit {
  // bread crum data
  breadCrumbItems: Array<{}>;

  user: User;

  error = '';
  message = '';
  token = '';
  email = '';
  loading = false;
  show = false;

  
  // bootstrap validation form
  typeValidationForm: FormGroup;
  typesubmit: boolean;

  constructor(
    public formBuilder: FormBuilder, 
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {

     /**
      * Bootstrap validation form data
      */
     this.typeValidationForm = this.formBuilder.group({
       username: ['', Validators.required],
       token: ['', Validators.required],
       password: ['', [Validators.required, Validators.minLength(8)]],
       confirmpwd: ['', Validators.required]
     }, 
     {
       validator: MustMatch('password', 'confirmpwd'),
     });

     this.token = this.route.snapshot.queryParams.token;
     this.email = this.route.snapshot.queryParams.email;
     
     this.typeValidationForm.controls.token.setValue(this.token);
     this.typeValidationForm.controls.username.setValue(this.email);
  }

  ngAfterViewInit() {
    document.body.classList.add('authentication-bg');
    document.body.classList.add('authentication-bg-pattern');
  }

  
  /**
   * Returns the type validation form
   */
  get type() {
    return this.typeValidationForm.controls;
  }

  /**
   * Type validation form submit data
   */
  typeSubmit() {
    this.typesubmit = true;
    this.loading = true;

    // stop here if form is invalid
    if (this.typeValidationForm.invalid) {

      this.loading = false;
      return;
    }

    if (this.type.username.value != "" || this.type.username.value != null) {
      this.authenticationService.changePassword(this.type.username.value, this.type.password.value, this.type.token.value)
        .pipe(first())
        .subscribe(
          data => {
            if(data.type == "success"){
              Swal.fire({
                position: 'center',
                type: data.type,
                title: data.message,
                showConfirmButton: false,
                timer: 2000
              });
              
              this.router.navigate(['/account/login']);

            }else{
              Swal.fire({
                position: 'center',
                type: data.type,
                title: data.message,
                showConfirmButton: true
              });
            }
            
            this.show = true;
          },
          error => {
            this.error = error;
          });
    } 
    this.loading = false;
  }

}
