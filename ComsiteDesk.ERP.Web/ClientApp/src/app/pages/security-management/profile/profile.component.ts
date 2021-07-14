import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NumberValueAccessor, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { Interconnect, IMessageStream } from 'ng-interconnect';

import { AuthenticationService } from '../../../core/services/auth.service';
import Swal from 'sweetalert2';
import { User } from 'src/app/core/models/auth.models';

import { environment } from '../../../../environments/environment';
import { HttpEventType } from '@angular/common/http';

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

  imageUrl: string = "assets/images/users/user.png";
  fileToUpload: File = null;

  url: any = "assets/images/users/user.png";

  public progress: number;

  // Form submition
  submit: boolean;
  //The broadcaster reference
  connectionImage: IMessageStream;

  constructor(
    public formBuilder: FormBuilder, 
    private authenticationService: AuthenticationService,
    private interconnect: Interconnect) { 
      this.connectionImage = this.interconnect.createBroadcaster("ImageProfile");
    }

  name = 'Angular 4';

  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      let files = event.target.files;
      this.uploadFile(files);

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => { // called once readAsDataURL is completed
        let target: any = event.target;
        this.url = target.result;
      }
    }
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    this.submit = true;
    this.loading = true;

    // stop here if form is invalid
    if (this.validationform.invalid) {

      this.loading = false;
      return;
    }

    let fileToUpload = <File>files[0];    

    this.authenticationService.updateUserAndPic(this.form.Email.value, this.form.FirstName.value,
      this.form.LastName.value, this.form.PhoneNumber.value, fileToUpload)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          let result :any = event.body;

          this.authenticationService.createUpdateUserCookie(result.user);

          this.connectionImage.emit();

          Swal.fire({
            position: 'center',
            type: result.type,
            title: result.message,
            showConfirmButton: false,
            timer: 1500
          });
          this.show = true;
          this.loading = false;          
        }
      });
  }

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
      Email: [{ value: '', disabled: true }, [Validators.required, Validators.email]],
      PhoneNumber: ['', [Validators.required, Validators.pattern('[0-9]+')]]
    });

    this.user = this.authenticationService.currentUser();

    this.validationform.controls.Id.setValue(this.user.id);
    this.validationform.controls.FirstName.setValue(this.user.firstName);
    this.validationform.controls.LastName.setValue(this.user.lastName);
    this.validationform.controls.Email.setValue(this.user.email);
    this.validationform.controls.PhoneNumber.setValue(this.user.phoneNumber);
    this.url = this.user.imageUrl == null ? this.imageUrl : `${environment.apiUrl}\\`+ this.user.imageUrl;

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
        .subscribe(
          data => {
            if (data.type === HttpEventType.Response) {
              this.message = 'Upload success.';
              let result :any = data.body;
              this.authenticationService.createUpdateUserCookie(result.user);
              this.connectionImage.emit();

              Swal.fire({
                position: 'center',
                type: result.type,
                title: result.message,
                showConfirmButton: false,
                timer: 1500
              });
              this.show = true;
              this.loading = false;          
            }
          },
          error => {
            this.error = error;
          });
    }
    this.loading = false;
  }

}