import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { User } from '../../core/models/auth.models';

import { Interconnect } from "ng-interconnect";


import { AuthenticationService } from '../../core/services/security/auth.service';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})
export class TopbarComponent implements OnInit {

  notificationItems: Array<{}>;
  languages: Array<{
    id: number,
    flag?: string,
    name: string
  }>;
  selectedLanguage: {
    id: number,
    flag?: string,
    name: string
  };

  fullName: string;
  imageUrl: string = "assets/images/users/user.png";
  user: User;
  businessName: string;

  openMobileMenu: boolean;

  @Output() settingsButtonClicked = new EventEmitter();
  @Output() mobileMenuButtonClicked = new EventEmitter();

  constructor(
    private router: Router, 
    private authService: AuthenticationService,
    private interconnect: Interconnect) { }

  ngOnInit() {
    // get the notifications
    this._fetchNotifications();
    this.openMobileMenu = false;
    this.loadInitValues();

    this.interconnect.receiveFrom("ImageProfile", "top-bar", data => {
      this.loadInitValues();
    });
  }

  loadInitValues(){
    this.user = this.authService.currentUser();
    this.fullName = this.authService.currentUser().firstName + " " + this.authService.currentUser().lastName;    
    this.imageUrl = this.user.imageUrl == null ? this.imageUrl : `${environment.apiUrl}\\`+ this.user.imageUrl;

    if(this.user.organization != undefined){
      this.businessName = this.user.organization.businessName;
    }
  }


  /**
   * Change the language
   * @param language language
   */
  changeLanguage(language) {
    this.selectedLanguage = language;
  }

  /**
   * Toggles the right sidebar
   */
  toggleRightSidebar() {
    this.settingsButtonClicked.emit();
  }

  /**
   * Toggle the menu bar when having mobile screen
   */
  toggleMobileMenu(event: any) {
    event.preventDefault();
    this.mobileMenuButtonClicked.emit();
  }

  /**
   * Logout the user
   */
  logout() {
    this.authService.logout();
    this.router.navigate(['/account/login']);
  }

  /**
   * Fetches the notification
   * Note: For now returns the hard coded notifications
   */
  _fetchNotifications() {
    this.notificationItems = [{
      text: 'Caleb Flakelar commented on Admin',
      subText: '1 min ago',
      icon: 'mdi mdi-comment-account-outline',
      bgColor: 'primary',
      redirectTo: '/notification/1'
    },
    {
      text: 'New user registered.',
      subText: '5 min ago',
      icon: 'mdi mdi-account-plus',
      bgColor: 'info',
      redirectTo: '/notification/2'
    },
    {
      text: 'Cristina Pride',
      subText: 'Hi, How are you? What about our next meeting',
      icon: 'mdi mdi-comment-account-outline',
      bgColor: 'success',
      redirectTo: '/notification/3'
    },
    {
      text: 'Caleb Flakelar commented on Admin',
      subText: '2 days ago',
      icon: 'mdi mdi-comment-account-outline',
      bgColor: 'danger',
      redirectTo: '/notification/4'
    },
    {
      text: 'Caleb Flakelar commented on Admin',
      subText: '1 min ago',
      icon: 'mdi mdi-comment-account-outline',
      bgColor: 'primary',
      redirectTo: '/notification/5'
    },
    {
      text: 'New user registered.',
      subText: '5 min ago',
      icon: 'mdi mdi-account-plus',
      bgColor: 'info',
      redirectTo: '/notification/6'
    },
    {
      text: 'Cristina Pride',
      subText: 'Hi, How are you? What about our next meeting',
      icon: 'mdi mdi-comment-account-outline',
      bgColor: 'success',
      redirectTo: '/notification/7'
    },
    {
      text: 'Caleb Flakelar commented on Admin',
      subText: '2 days ago',
      icon: 'mdi mdi-comment-account-outline',
      bgColor: 'danger',
      redirectTo: '/notification/8'
    }];
  }
}
