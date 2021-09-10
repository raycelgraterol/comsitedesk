import { Directive, ElementRef, Input } from '@angular/core';
import { take } from 'rxjs/operators';

import { AuthenticationService } from 'src/app/core/services/security/auth.service';
import { RoleFormActionsService } from 'src/app/core/services/security/role-form-actions.service';

@Directive({
    selector: '[appModuleURI]'
})
export class ModuleURIDirective {

    @Input() appHighlight = '';
    @Input() moduleURI = '';

    constructor(private el: ElementRef,
        private authService: AuthenticationService,
        public roleFormActionsService: RoleFormActionsService) {
            this.el.nativeElement.style.display = 'none';
    }

    ngOnInit() {
        this.CheckModules(this.moduleURI)
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

    CheckModules(uri: string) {
        let role = this.authService.getMainRole();
        return this.roleFormActionsService
            .checkRoleModule(role.id, uri)
            .pipe(take(1));
    }
}