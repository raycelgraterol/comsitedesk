import { Directive, ElementRef, Input } from '@angular/core';
import { take } from 'rxjs/operators';

import { AuthenticationService } from 'src/app/core/services/security/auth.service';
import { RoleFormActionsService } from 'src/app/core/services/security/role-form-actions.service';

@Directive({
  selector: '[appFormURI]'
})
export class FormURIDirective {

  @Input() appHighlight = '';
  @Input() formURI = '';
  @Input() actionName = '';

  constructor(private el: ElementRef,
    private authService: AuthenticationService,
    public roleFormActionsService: RoleFormActionsService) {
      this.el.nativeElement.style.display = 'none';
  }

  ngOnInit() {
    this.CheckForms()
      .subscribe(
        result => {
          if (!result) {
            this.el.nativeElement.remove();
          } else {
            this.el.nativeElement.style.display = 'list-item';
          }
        },
        error => { console.error(error) }
      );
  }

  CheckForms() {
    let role = this.authService.getMainRole();
    return this.roleFormActionsService
      .checkRoleCanViewForm(role.id, this.formURI, this.actionName)
      .pipe(take(1));
  }
}